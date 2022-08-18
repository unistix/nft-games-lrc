using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer"); //searches parent rather than whole scene
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");


        //HIDE GAME OBJECT TO START
        entryTemplate.gameObject.SetActive(false);

        //AddHighscoreEntry(1000, "CMK");

        //GENERATE STATIC VALUES HIGH SCORE TABLE - [Eventually this will be a singleton list entry]

        /*highscoreEntryList = new List<HighscoreEntry>() - Hardcore method for populating data
        {
            new HighscoreEntry{ score=521854, name="AAA" },
            new HighscoreEntry{ score=35824562, name="TXT"},
            new HighscoreEntry{ score=234234, name="BBB"},
            new HighscoreEntry{ score=234114, name="DHI"}
        }; */


        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Debug.Log(jsonString);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //sorting will be moved to save and load later on new entry creation */

        //find faster sorting ALG

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
        }


        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        /*
        ///instatiate class to see all objects
         Highscores highscores = new Highscores{ highscoreEntryList = highscoreEntryList };

         //Basic saving and loading - hardcore method pfor populating data
         string json = JsonUtility.ToJson(highscores); //pulling in hardcoded list 
         PlayerPrefs.SetString("highscoreTable", json);
         PlayerPrefs.Save();
         Debug.Log(PlayerPrefs.GetString("highscoreTable")); */





    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        //Generate table valuess
        int rank = transformList.Count + 1; //
        string rankString;


        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        //Populate table with new row 
        entryTransform.Find("_posText").GetComponent<Text>().text = rankString;


        int score = highscoreEntry.score;

        entryTransform.Find("_scoreText").GetComponent<Text>().text = score.ToString();


        string name = highscoreEntry.name;
        entryTransform.Find("_nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    //ADD new high score 

    private void AddHighscoreEntry(int score, string name){
        // Creaate High Score Entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load Saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updated Highscore List 
        string json = JsonUtility.ToJson(highscores); //pulling in hardcoded list 
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

    }
   
    /*creare highscore class to see all objects*/
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Create high score entry
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }



}
