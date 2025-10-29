using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Conductor : MonoBehaviour
{
    [Header("References")]
    public ChartHolder chartHolder;
    public AudioSource music; //the music
    public AudioSource metronome;
    public NoteDisplay[] noteDisplays;
    EventCore eventCore;

    [Header("Conductor")]
    public float bpm; //bpm of song
    public List<List<float>> chart = new List<List<float>>(); //list that holds the notes with its measures, beat and type

    [Header("Conductor Stats")]
    [SerializeField] float secPerBeat; //how many seconds each beat takes
    [SerializeField] float songPosition; //current song position in seconds
    [SerializeField] int totalBeats; //the total amount of beats in a song
    [SerializeField] int currentMeasure; //current measure of the song
    [SerializeField] float currentBeat; //current beat of the song
    [SerializeField] float dspSongTime; //how many seconds passed since the song started
    [SerializeField] float noteInMilliseconds; //convert the current note's position in time to milliseconds
    [SerializeField] float ms; //how many milliseconds away from finishing
    [SerializeField] float playerRow; //the row the player is in

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.provideInput.AddListener(ProcessInput);

        music = GetComponent<AudioSource>();
        secPerBeat = 60f / bpm; //get the amount of seconds per beat based on bpm
        dspSongTime = (float)AudioSettings.dspTime; //get the amount of time since song started

        chart = chartHolder.getChart("test2"); //get the test chart
        music.Play(); //play the music

        playerRow = 4; //start at the very back

        noteInMilliseconds = secPerBeat; //start from the first note
    }

    // Update is called once per frame
    void Update()
    {
        //determine the song position by checking how many seconds have passed since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
        //print("song position: " + songPosition);

        //get the total amount of beats in the song
        totalBeats = Mathf.FloorToInt(songPosition / secPerBeat);

        //metronome that plays every beat
        if ((totalBeats % 4) + 1 != currentBeat)
            metronome.Play();

        //get the current measure and beat of the song
        currentMeasure = (int)totalBeats / 4 + 1;
        currentBeat = (totalBeats % 4) + 1;
        
        ms = (noteInMilliseconds - songPosition) * 1000;
        print("ms: " + (ms));

        //if the current note has been up for 200 ms, update for the next note
        if (ms < -700)
        {
            noteInMilliseconds = ((currentMeasure - 1) * 4 + currentBeat) * secPerBeat;
            noteDisplays[(int)playerRow - 1].image.color = Color.white;
        }
        
        //check if the note should be displayed
        while (chart[0][0] == currentMeasure && chart[0][1] == currentBeat)
        {
            print(chart[0][0] + ", " + chart[0][1] + ", " + chart[0][2]);

            if (chart[0][3] != playerRow)    
                noteDisplays[(int)chart[0][3] - 1].setNote(chart[0][2]);

            chart.Remove(chart[0]);
        }
    }

    //check player's input and see if it is on beat and correctly pressed
    void ProcessInput(string input)
    {
        //change player's arrow to the movement they did
        noteDisplays[(int)playerRow - 1].setNote(chartHolder.GetMoveId(input));

        float specificNote = GetSpecificNote();

        //if player did the wrong movement
        if (input != chartHolder.GetMoveName(specificNote))
        {
            print("wrong movement dummy\nms:" + (ms));
            print("should be " + chartHolder.GetMoveName(chart[0][2]) + ", but you pressed " + input);
            return;
        }
        //print("note in milliseconds: " + noteInMilliseconds);
        print("ms: " + (ms));

        //okay judgement
        if (ms > 184 || ms < -184)
        {
            print("judgement: okay \nms:" + (ms));
            noteDisplays[(int)playerRow - 1].image.color = Color.orange;
            return;
        }

        //good judgement
        if (ms > 66 || ms < -66)
        {
            print("judgement: good \nms:" + (ms));
            noteDisplays[(int)playerRow - 1].image.color = Color.green;
            return;
        }

        //perfect judgement
        if (ms > 32 || ms < -32)
        {
            print("judgement: perfect \nms:" + (ms));
            noteDisplays[(int)playerRow - 1].image.color = Color.blue;
            return;
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
