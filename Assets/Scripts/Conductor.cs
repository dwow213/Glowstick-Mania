using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Conductor : MonoBehaviour
{
    //currently, it has some desync issues due to how the note the player is supposed to hit updates
    
    [Header("References")]
    public ChartHolder chartHolder;
    public AudioSource music; //the music
    public AudioSource metronome;
    public NoteDisplay[] noteDisplays2d; //temp 2d, for testing only
    public NoteDisplay[] noteDisplays3d; //the crowds and coordinator
    public PlayerScript player;
    EventCore eventCore;

    [Header("Conductor")]
    public float bpm; //bpm of song
    public List<List<float>> chart = new List<List<float>>(); //list that holds the notes with its measures, beat and type

    [Header("Conductor Stats")]
    public float secPerBeat; //how many seconds each beat takes
    [SerializeField] float songPosition; //current song position in seconds
    public int totalBeats; //the total amount of beats in a song
    public int currentMeasure; //current measure of the song
    [SerializeField] float currentBeat; //current beat of the song
    [SerializeField] float dspSongTime; //how many seconds passed since the song started
    [SerializeField] float noteInMilliseconds; //convert the current note's position in time to milliseconds
    [SerializeField] float ms; //how many milliseconds away from finishing
    [SerializeField] public float playerRow; //the row the player is in
    bool rowChanged;
    bool alreadyMoved;

    bool onPlayerNote;
    float targetMovement;
    float startTimer;
    bool startSong;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.provideInput.AddListener(ProcessInput);

        //music = GetComponent<AudioSource>();
        secPerBeat = 60f / bpm; //get the amount of seconds per beat based on bpm

        chart = chartHolder.getChart("RubySunset"); //get the test chart
        //music.Play(); //play the music

        playerRow = 4; //start at the very back

        noteInMilliseconds = secPerBeat; //start from the first note
        alreadyMoved = true;
        onPlayerNote = false;
        startTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print("player row: " + playerRow);
        
        if (!startSong)
        {
            startTimer += Time.deltaTime;

            if (startTimer > 3)
            {
                startSong = true;
                //AudioSettings.Reset();
                dspSongTime = (float)AudioSettings.dspTime; //get the amount of time since song started
                music.Play();
            }

            return;
        }

        //determine the song position by checking how many seconds have passed since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //print("song position: " + songPosition);

        //get the total amount of beats in the song
        totalBeats = Mathf.FloorToInt(songPosition / secPerBeat);

        //metronome that plays every beat
        if ((totalBeats % 4) + 1 != currentBeat)
        {
            metronome.Play();
            print(chart.Count);
            print(chart[0][0] + ", " + chart[0][1] + ", " + chart[0][2]);
        }

        //get the current measure and beat of the song
        currentMeasure = (int)totalBeats / 4 + 1;
        currentBeat = (totalBeats % 4) + 1;
        
        ms = (noteInMilliseconds - songPosition) * 1000;
        //print("ms: " + (ms));

        //normal note
        if ((ms < -secPerBeat && !onPlayerNote))
        //if (ms < -700)
        {
            noteInMilliseconds = ((currentMeasure - 1) * 4 + currentBeat) * secPerBeat;
            noteDisplays2d[(int)playerRow - 1].image.color = Color.white;

            //print("new ms: " + ((noteInMilliseconds - songPosition) * 1000));

            alreadyMoved = false;
            rowChanged = false;
        }

        //note for player
        if (ms < -700 && onPlayerNote)
        {
            noteInMilliseconds = ((currentMeasure - 1) * 4 + currentBeat) * secPerBeat;
            noteDisplays2d[(int)playerRow - 1].image.color = Color.white;

            //print("new ms: " + ((noteInMilliseconds - songPosition) * 1000));

            if (!alreadyMoved)
            {
                noteDisplays2d[(int)playerRow - 1].image.color = Color.red;
                player.PointChange(-2f);
            }

            onPlayerNote = false;
            alreadyMoved = false;
        }

        //every two measures, change the row
        if (currentMeasure % 2 != 0 && currentBeat == 1 && !(currentMeasure < 4) && !rowChanged)
        {
            changeRow();
        }

        //if there is still more notes in the chart
        if (chart.Count > 0)
        {
            //check if the note should be displayed
            while (chart[0][0] == currentMeasure && chart[0][1] == currentBeat)
            {
                print(chart[0][0] + ", " + chart[0][1] + ", " + chart[0][2]);

                //change the crowd's pose
                noteDisplays3d[(int)chart[0][3] - 1].setNote(chart[0][2]);

                //change the note as long as it's the player's row (2d only)
                if (chart[0][3] != playerRow)
                    noteDisplays2d[(int)chart[0][3] - 1].setNote(chart[0][2]);
                
                if (chart[0][3] == playerRow - 1)
                {
                    print("on player note");
                    noteInMilliseconds = ((currentMeasure - 1) * 4 + currentBeat) * secPerBeat;
                    targetMovement = chart[1][2]; //save the player's note
                    onPlayerNote = true;
                    print("new ms: " + ((noteInMilliseconds - songPosition) * 1000));
                }

                chart.Remove(chart[0]);
            }
        }
        //if 2 seconds have passed since the music ended
        else if (songPosition > music.clip.length + 2)
        {
            //if player does really well, they get win screen
            if (playerRow == 2 && player.points > 0)
                SceneManager.LoadScene(2);
            else //otherwise, they get pass screen
                SceneManager.LoadScene(4);
        }

    }

    //check player's input and see if it is on beat and correctly pressed
    void ProcessInput(string input)
    {
        //change player's arrow to the movement they did
        noteDisplays2d[(int)playerRow - 1].setNote(chartHolder.GetMoveId(input));

        if (alreadyMoved)
        {
            print("already moved:" + songPosition);
            return;
        }

        //stop player from doing input if they're too far
        if (!onPlayerNote)
        {
            print("not time for player input: " + songPosition);
            //player.PointChange(-0.1f);
            return;
        }

        //float specificNote = GetSpecificNote();
        float specificNote = targetMovement;

        alreadyMoved = true;

        //if player did the wrong movement
        if (input != chartHolder.GetMoveName(specificNote))
        {
            print("wrong movement dummy\nms:" + (ms));
            print("should be " + chartHolder.GetMoveName(chart[0][2]) + ", but you pressed " + input);
            noteDisplays2d[(int)playerRow - 1].image.color = Color.red; //might be temp
            player.PointChange(-2f * Time.deltaTime);
            alreadyMoved = false;
            eventCore.wrongMovement.Invoke();
            return;
        }

        //okay judgement
        if (ms > 92 || ms < -92)
        {
            print("judgement: okay \nms:" + (ms));
            noteDisplays2d[(int)playerRow - 1].image.color = Color.orange;
            player.PointChange(0.3f);
            eventCore.processJudgement.Invoke(1);
            return;
        }

        //good judgement
        if (ms > 33 || ms < -33)
        {
            print("judgement: good \nms:" + (ms));
            noteDisplays2d[(int)playerRow - 1].image.color = Color.green;
            player.PointChange(0.6f);
            eventCore.processJudgement.Invoke(2);
            return;
        }

        //perfect judgement
        print("judgement: perfect \nms:" + (ms));
        noteDisplays2d[(int)playerRow - 1].image.color = Color.lightBlue;
        player.PointChange(1f);
        eventCore.processJudgement.Invoke(3);
        return;
    }

    //changing player's row based on performance
    void changeRow()
    {
        print("changeRow");

        rowChanged = true;

        //player is doing well; make them go closer
        if (player.points > 0)
        {
            if (playerRow > 2)
            {
                playerRow--;
                eventCore.processMovement.Invoke(true);
            }


        }
        else //player is doing worse; make them go farther
        {
            if (playerRow < 4)
            {
                playerRow++;
                eventCore.processMovement.Invoke(false);
            }

        }
    }

    //get the note that should be pressed
    float GetSpecificNote()
    {
        //if there is only one note
        if (chart.Count < 2)
        {
            return chart[0][2];
        }
        
        //if there are not two notes being shown in the same measure or beat
        if (chart[0][0] != chart[1][0] || chart[0][1] != chart[1][1])
        {
            return chart[0][2];
        }

        //check how far the player's row is from these notes
        float firstNoteDifference = playerRow - chart[0][3];
        float secondNoteDifference = playerRow - chart[1][3];

        //if this note has already passed the player or the note is closer to the player than the other one
        if (firstNoteDifference < 0 || secondNoteDifference < firstNoteDifference)
        {
            return chart[1][2];
        }

        if (secondNoteDifference < 0 || firstNoteDifference < secondNoteDifference)
        {
            return chart[0][2];
        }

        return -1;
    }
}
