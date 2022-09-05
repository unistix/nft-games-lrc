using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class NavUI : MonoBehaviour
{
    //------ Login In Scene Buttons IO------
    public InputField textBox;
    public Text errorText;
    public Text highScoreText;
    public Text nameText;
    public Text rankText;
    public Text scoreText;

    public void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Menu" )
        {
            PlayerData player = SaveSystem.ReadCurrentPlayerFileBin();
            PlayerData playerU = SaveSystem.ReadCurrentPlayerFileUnsafe();
            Debug.Log("UI Set");
            nameText.text = player.name;
            highScoreText.text = player.highscore.ToString();
            rankText.text = SaveSystem.ReadCurrentRankFromAllBin(player.name).ToString();
           
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            PlayerData player = SaveSystem.ReadCurrentPlayerFileBin();
            PlayerData playerU = SaveSystem.ReadCurrentPlayerFileUnsafe();
            Debug.Log("UI Set");
            nameText.text = player.name;
            highScoreText.text = player.highscore.ToString();
        }
        
        

        
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            PlayerData player = SaveSystem.ReadCurrentPlayerFileBin();
            rankText.text = SaveSystem.ReadCurrentRankFromAllBin(player.name).ToString();

        };
    }
    //public Text error message;

    //-----------IO------------
    public string GetUsernameInput()
    {
        string username;
        //Debug.Log(textBox.text);

       
        
        try
        {
            if (textBox.text != "")
            {
                username = textBox.text;

                return username;

            }
            else
            {
                Debug.Log("Please enter username - Input field Empty");
                errorText.text = "Please enter username - Input field Error";
                return null;
            }

           
        }
       catch (NullReferenceException e)
       {
             
           Debug.LogException(e, this);
            Debug.Log("Please enter username - Input field Error"); //TEXT ERROR
            errorText.text = "Please enter username - Input field Error";
            return null;

            //this is where the error message gets updated
       }


        
    }

    //-----------Buttons------------
    public void LogIn()
    {
        
        string username = GetUsernameInput();
        //Debug.Log(username);

        if (username != null )
        {
            PlayerData playerdata = SaveSystem.ReadCurrentPlayerByNameFromAllPLayerFileBin(username);
            PlayerData playerdataU = SaveSystem.ReadCurrentPlayerByNameFromAllPLayerFileUnsafe(username);
            try
            {
                
                if (playerdata != null)
                {
                    Debug.Log(playerdata.name);
                    SaveSystem.DeleteCurrentPlayerFile(); 
                    SaveSystem.CreateCurrentPlayerFileUnsafe(playerdataU);
                    SaveSystem.CreateCurrentPlayerFileBin(playerdata); 
                    SceneManager.LoadScene("Menu");

                }
                else
                {
                    Debug.Log("User does not exist please sign up"); //TEXT ERROR 
                    errorText.text = "User does not exist please sign up";
                }
                
                /*SaveSystem.DeleteCurrentPlayerFile(); // Ensure No Rouge Files
                SaveSystem.CreateCurrentPlayerFileUnsafe(playerdataU);
                SaveSystem.CreateCurrentPlayerFileBin(playerdata); */


                
            }

            catch (NullReferenceException e)
            {

                Debug.LogException(e, this);
                Debug.Log("User does not exist please sign up"); //TEXT ERROR 
                errorText.text = "User does not exist please sign up";


                //this is where the error message gets updated
            }
            
            
        }
        else
        {
            Debug.Log("Please enter username - Input Text Error");
            //this is where text error handling goes

        }

        

        
        //no need to return data as it can be read from file now

     
    }

    public void SignUp()
    {
        string username = GetUsernameInput();
        //Debug.Log(username);
        //add a new player to the main file the run log in but with main file included
        PlayerData newplayer = new PlayerData(username);
        bool notexists = SaveSystem.UpdateAllPlayerFileBin_AddPlayerUnique(newplayer);
        bool notexistsU = SaveSystem.UpdateAllPlayerFileUnsafe_AddPlayerUnique(newplayer);

 
        if (username != null)
        {
 
            try
            {

                if (newplayer != null && notexists==true && notexists == true)
                {
                    Debug.Log(newplayer.name);
                    SaveSystem.DeleteCurrentPlayerFile();// Ensure No Rouge Files
                    SaveSystem.CreateCurrentPlayerFileUnsafe(newplayer);
                    SaveSystem.CreateCurrentPlayerFileBin(newplayer);
                    //Settheir rank to last 
                    SceneManager.LoadScene("Menu");

                }
                else
                {
                    Debug.Log("User Exists Please log in"); //TEXT ERROR 
                    errorText.text = "User Exists Please log in";
                }

            }

            catch (NullReferenceException e)
            {

                Debug.LogException(e, this);
                Debug.Log("User Exists Please log in"); //TEXT ERROR 
                errorText.text = "User Exists Please log in";


                //this is where the error message gets updated
            }


        }
        else
        {
            Debug.Log("Please enter username - Input Text Error");
            //this is where text error handling goes

        }
        
    }





    // -----Menu Scene -----

    //--------------IO---------------
    /*public void SetUsernameText(string username)
    {
        

        
    }

    public void SetHighScoreText(string highscore)
    {
    

        scoreText.text = highscore;
    } */

    public void SetRankText(string rank)
    {
        

    }


    //-----------Buttons------------
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        //starts game
    }

    public void Quit()
    {
        //clear existing player pref and return to main menu
        SceneManager.LoadScene("Login");
        SaveSystem.DeleteCurrentPlayerFile();
    }

    // -----GameScene -----

    //--------------IO---------------
    //reused from menu but build others here 

    //-----------Buttons------------
    public void Roll() //GAME ACTION BUTTON - THIS IS WHERE ACTIVITY WILL TAKE PLACE
    {
        Debug.Log("Roll Dice");
        int score = Game.RollDice(); //Game action starts
        scoreText.text = score.ToString();
        PlayerData player = SaveSystem.ReadCurrentPlayerFileBin();
        PlayerData playerU = SaveSystem.ReadCurrentPlayerFileUnsafe();
        if (score > player.highscore)
        {
            player.highscore = score;
            string _highScoreText = score.ToString();
            highScoreText.text = _highScoreText;
            //UPDATE BLOCK
            SaveSystem.UpdateCurrentPlayerFileBin(player);
            SaveSystem.UpdateCurrentPlayerFileUnsafe(player);
            SaveSystem.UpdateAllPlayerFileBin_UpdatePlayer(player.name, "highscore", _highScoreText);
            SaveSystem.UpdateAllPlayerFileUnsafe_UpdatePlayer(player.name, "highscore", _highScoreText);
            //UPDATE BLOCK

            //Get current rank!!
        }







        //Game Action ends
        //Data is updated //in normal game update will be called after die in the game function

    }

    

    // Game Scene Buttons
    public void Menu()
    {
        //go back to menu
        SceneManager.LoadScene("Menu");
    }


    public void HighScore()
    {
        //display highscores

        Debug.Log("loading high score");
        SceneManager.LoadScene("Highscore");
    }

    // -----High Score -----


    //--------------IO---------------
    public void SetRankHighscoreTableText(string rank)
    {

    }

    //-----------Buttons------------
    //reused from menu and game but build others here 
}
