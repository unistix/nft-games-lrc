using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //public Text score;
    //public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        //get highscore from file already sorted in nav UI
    }

    // This is the GAME it should just return a final score 
    public static int RollDice()
    {
        int score = Random.Range(1, 300);
        

        return score;
    }

    //Handle the score and see it it's a high score

    //Update playerdata according it it's an improvement
}
