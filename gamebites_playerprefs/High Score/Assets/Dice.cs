using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Text score;
    public Text highScore;
    public Text congrats;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore",0).ToString(); //0 is default value
        //congrats.gameObject.SetActive(false);
        Deactivate();
    }

    public void RollDice()
    {
        int number = Random.Range(1, 7);
        score.text = number.ToString();

        if (number > PlayerPrefs.GetInt("HighScore",0))
        {
            congrats.gameObject.SetActive(true);
            Invoke("Deactivate", 2);

            PlayerPrefs.SetInt("HighScore", number);
            highScore.text = number.ToString();
            

            //You beat the high score
        }


        
    }

    public void Deactivate()
    {
        congrats.gameObject.SetActive(false);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
        //PlayerPrefs.DeleteAll() remove all player prefs 
    }
}
