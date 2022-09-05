using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighscoreTable : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform rowContainer;
    private Transform rowTemplate;
    private AllPlayerData data;
    private List<PlayerData> sortedPlayerList;
    void Awake()
    {


        ReadandSortTableData();
        DisplayTableData();
        //SaveSystem.DeletePlayerFromAllBin("jim");
        //SaveSystem.DeletePlayerFromAllUnsafe("jim");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadandSortTableData()
    {
        data = SaveSystem.ReadAllPlayersFileBin();
        List<PlayerData> playerList = data.players;
        sortedPlayerList = playerList.OrderByDescending(p => p.highscore).ToList();

        //List <PlayerData> sortedplayersList = playerList.Sort((player1,player2) => player1.highscore.CompareTo() );
    }

    void DisplayTableData()
    {
        rowContainer = transform.Find("HighScoreContainer");
        rowTemplate = rowContainer.Find("HighScoreTemplate");
        //Debug.Log(entryTemplate);

        rowTemplate.gameObject.SetActive(false);

        float templateHeight = 25f; //define float for template height and
        int i = 0;



        //for (int i = 0; i < sortedPlayerList.; i++) //change 10 to var.length

        foreach (PlayerData p in sortedPlayerList)
        {
                
            
            Transform rowTransform = Instantiate(rowTemplate, rowContainer); //indivual row entry

            RectTransform rowRectTransform = rowTransform.GetComponent<RectTransform>(); //selects the rect transform component
            rowRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);  //postion based on template height float  
            rowTransform.gameObject.SetActive(true);

            //position is defined by rank > this is where rank exists - and is read into the data 
            int _rank = i + 1; // one ahead to start with rank 1 instead of zero
            string rank = (_rank == 1) ? "1st" :
                (_rank == 2) ? "2nd" :
                (_rank == 3) ? "3rd" :
                _rank + "th";






            rowTransform.Find("Pos").GetComponent<Text>().text = rank;

            int score = p.highscore;

            rowTransform.Find("Score").GetComponent<Text>().text = score.ToString();

            string name = p.name;
            rowTransform.Find("Name").GetComponent<Text>().text = name;
            i++;
            if (i==10)
            {
                break; // get out of the loop.
            }

        }
    }
}
