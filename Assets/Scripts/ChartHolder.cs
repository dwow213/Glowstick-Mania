using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChartHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gets the chart
    //charts are stored in a 2d float array, where each index holds another float list which holds the values that represent a note
    //in the embedded list, first index is the measure, second index is the beat, third index is the movement type and fourth index is row number
    public List<List<float>> getChart(string chart)
    {
        switch (chart)
        {
            case "test":
                return GetTestChart();

            case "test2":
                return GetTest2Chart();

            case "RubySunset":
                return GetRubySunsetChart();

            default:
                print("invalid chart");
                return null;
        }
    }
    
    //getting the test chart
    //reference:
    //new List<float> {measure, beat, movementType, noteRowNum}
    List<List<float>> GetTestChart()
    {
        List<List<float>> chart = new List<List<float>>
        {
            new List<float> {1, 1, GetMoveId("up"), 1},

            new List<float> {1, 2, GetMoveId("up"), 2},
            
            new List<float> {1, 3, GetMoveId("up"), 3},
            
            new List<float> {1, 4, GetMoveId("up"), 4},
            
            new List<float> {2, 1, GetMoveId("down"), 1},
            
            new List<float> {2, 2, GetMoveId("down"), 2},
            
            new List<float> {2, 3, GetMoveId("down"), 3},
            
            new List<float> {2, 4, GetMoveId("down"), 4},
            
            new List<float> {3, 1, GetMoveId("left"), 1},
            
            new List<float> {3, 2, GetMoveId("left"), 2},
            
            new List<float> {3, 3, GetMoveId("left"), 3},
            
            new List<float> {3, 4, GetMoveId("left"), 4},
            
            new List<float> {4, 1, GetMoveId("right"), 1},
            
            new List<float> {4, 2, GetMoveId("right"), 2},
            
            new List<float> {4, 3, GetMoveId("right"), 3},
            
            new List<float> {4, 4, GetMoveId("right"), 4},
        };

        return chart;
    }

    List<List<float>> GetTest2Chart()
    {
        List<List<float>> chart = new List<List<float>>
        {
            new List<float> {1, 1, GetMoveId("up"), 1},

            new List<float> {1, 2, GetMoveId("up"), 2},

            new List<float> {1, 3, GetMoveId("left"), 1},
            new List<float> {1, 3, GetMoveId("up"), 3},

            new List<float> {1, 4, GetMoveId("left"), 2},
            new List<float> {1, 4, GetMoveId("up"), 4},

            
            new List<float> {2, 1, GetMoveId("down"), 1},
            new List<float> {2, 1, GetMoveId("left"), 3},

            new List<float> {2, 2, GetMoveId("down"), 2},
            new List<float> {2, 2, GetMoveId("left"), 4},

            new List<float> {2, 3, GetMoveId("right"), 1},
            new List<float> {2, 3, GetMoveId("down"), 3},

            new List<float> {2, 4, GetMoveId("right"), 2},
            new List<float> {2, 4, GetMoveId("down"), 4},

            new List<float> {3, 1, GetMoveId("right"), 3},

            new List<float> {3, 2, GetMoveId("right"), 4},

            new List<float> {3, 3, GetMoveId("right"), 1},

            new List<float> {3, 4, GetMoveId("right"), 1},

            new List<float> {4, 1, GetMoveId("up"), 1},

            new List<float> {4, 2, GetMoveId("up"), 2},

            new List<float> {4, 3, GetMoveId("up"), 3},

            new List<float> {4, 4, GetMoveId("up"), 4},

            new List<float> {5, 1, GetMoveId("down"), 1},

            new List<float> {5, 2, GetMoveId("down"), 2},

            new List<float> {5, 3, GetMoveId("down"), 3},

            new List<float> {5, 4, GetMoveId("down"), 4},

            new List<float> {6, 1, GetMoveId("left"), 1},

            new List<float> {6, 2, GetMoveId("left"), 2},

            new List<float> {6, 3, GetMoveId("left"), 3},

            new List<float> {6, 4, GetMoveId("left"), 4},

            new List<float> {7, 1, GetMoveId("right"), 1},

            new List<float> {7, 2, GetMoveId("right"), 2},

            new List<float> {7, 3, GetMoveId("right"), 3},

            new List<float> {7, 4, GetMoveId("right"), 4},

            new List<float> {8, 1, GetMoveId("up"), 1},

            new List<float> {8, 2, GetMoveId("up"), 2},

            new List<float> {8, 3, GetMoveId("left"), 1},
            new List<float> {8, 3, GetMoveId("up"), 3},

            new List<float> {8, 4, GetMoveId("left"), 2},
            new List<float> {8, 4, GetMoveId("up"), 4},

            new List<float> {9, 1, GetMoveId("down"), 1},
            new List<float> {9, 1, GetMoveId("left"), 3},

            new List<float> {9, 2, GetMoveId("down"), 2},
            new List<float> {9, 2, GetMoveId("left"), 4},

            new List<float> {9, 3, GetMoveId("right"), 1},
            new List<float> {9, 3, GetMoveId("down"), 3},

            new List<float> {9, 4, GetMoveId("right"), 2},
            new List<float> {9, 4, GetMoveId("down"), 4},

            new List<float> {10, 1, GetMoveId("right"), 3},

            new List<float> {10, 2, GetMoveId("right"), 4},

            new List<float> {10, 3, GetMoveId("right"), 1},

            new List<float> {10, 4, GetMoveId("right"), 1},

        };

        return chart;
    }

    List<List<float>> GetRubySunsetChart()
    {
        List<List<float>> chart = new List<List<float>>
        {
            new List<float> {3, 1, GetMoveId("up"), 1},
            new List<float> {3, 2, GetMoveId("up"), 2},
            new List<float> {3, 3, GetMoveId("up"), 3},
            new List<float> {3, 4, GetMoveId("up"), 4},

            new List<float> {4, 1, GetMoveId("right"), 1},
            new List<float> {4, 2, GetMoveId("right"), 2},
            new List<float> {4, 3, GetMoveId("right"), 3},
            new List<float> {4, 4, GetMoveId("right"), 4},

            new List<float> {5, 1, GetMoveId("left"), 1},
            new List<float> {5, 2, GetMoveId("left"), 2},
            new List<float> {5, 3, GetMoveId("left"), 3},
            new List<float> {5, 4, GetMoveId("left"), 4},

            new List<float> {6, 1, GetMoveId("down"), 1},
            new List<float> {6, 2, GetMoveId("down"), 2},
            new List<float> {6, 3, GetMoveId("down"), 3},
            new List<float> {6, 4, GetMoveId("down"), 4},

            new List<float> {7, 1, GetMoveId("left"), 1},
            new List<float> {7, 2, GetMoveId("left"), 2},
            new List<float> {7, 3, GetMoveId("left"), 3},
            new List<float> {7, 4, GetMoveId("left"), 4},

            new List<float> {8, 1, GetMoveId("right"), 1},
            new List<float> {8, 2, GetMoveId("right"), 2},
            new List<float> {8, 3, GetMoveId("right"), 3},
            new List<float> {8, 4, GetMoveId("right"), 4},

            new List<float> {9, 1, GetMoveId("left"), 1},
            new List<float> {9, 2, GetMoveId("left"), 2},
            new List<float> {9, 3, GetMoveId("left"), 3},
            new List<float> {9, 4, GetMoveId("left"), 4},

            new List<float> {10, 1, GetMoveId("up"), 1},
            new List<float> {10, 2, GetMoveId("up"), 2},
            new List<float> {10, 3, GetMoveId("up"), 3},
            new List<float> {10, 4, GetMoveId("up"), 4},

            new List<float> {11, 1, GetMoveId("down"), 1},
            new List<float> {11, 2, GetMoveId("down"), 2},
            new List<float> {11, 3, GetMoveId("down"), 3},
            new List<float> {11, 4, GetMoveId("down"), 4},

            new List<float> {12, 1, GetMoveId("up"), 1},
            new List<float> {12, 2, GetMoveId("up"), 2},
            new List<float> {12, 3, GetMoveId("up"), 3},
            new List<float> {12, 4, GetMoveId("up"), 4},

            new List<float> {13, 1, GetMoveId("left"), 1},
            new List<float> {13, 2, GetMoveId("left"), 2},
            new List<float> {13, 3, GetMoveId("left"), 3},
            new List<float> {13, 4, GetMoveId("left"), 4},

            new List<float> {14, 1, GetMoveId("down"), 1},
            new List<float> {14, 2, GetMoveId("down"), 2},
            new List<float> {14, 3, GetMoveId("down"), 3}, 
            new List<float> {14, 4, GetMoveId("down"), 4},

            new List<float> {15, 1, GetMoveId("right"), 1},
            new List<float> {15, 2, GetMoveId("right"), 2}, 
            new List<float> {15, 3, GetMoveId("right"), 3},
            new List<float> {15, 4, GetMoveId("right"), 4},

            new List<float> {16, 1, GetMoveId("left"), 1},
            new List<float> {16, 2, GetMoveId("left"), 2},
            new List<float> {16, 3, GetMoveId("left"), 3},
            new List<float> {16, 4, GetMoveId("left"), 4},

            new List<float> {17, 1, GetMoveId("down"), 1},
            new List<float> {17, 2, GetMoveId("down"), 2},
            new List<float> {17, 3, GetMoveId("down"), 3},
            new List<float> {17, 4, GetMoveId("down"), 4},


        };

        return chart;
    }

    //converts the move type from a string to a float so it can be read by conductor
    public float GetMoveId(string moveType)
    {
        switch (moveType)
        {
            case "up":
                return 1;

            case "down":
                return 2;

            case "left":
                return 3;

            case "right":
                return 4;
            
            default:
                return -1;
        }
    }

    //converts the move id from a float to a string 
    public string GetMoveName(float moveId)
    {
        switch (moveId)
        {
            case 1:
                return "up";

            case 2:
                return "down";

            case 3:
                return "left";

            case 4:
                return "right";

            default:
                return null;
        }
    }
}
