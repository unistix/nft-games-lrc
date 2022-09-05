using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text score;
    public Text highScore;
    public void RollDice()
    {
        int number = Random.Range(1, 7);
        score.text = number.ToString();
        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        
    }

    // Update is called once per frame
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore",0).ToString();
    }
}
