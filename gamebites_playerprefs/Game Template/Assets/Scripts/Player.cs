using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string _name = "";
    public int highscore = 0;
    public int rank = 0;

    public Player(string username)
    {
        _name = username;
      
        Debug.Log("player constructor working");

    }

    public void PlayerInit(string username)
    {
        _name = username;

        Debug.Log("player constructor-initialiser working");

    }
    /*
    public void CreateAllPlayersFile()
    {
        //run once
        
        SaveSystem.CreateAllPlayersFileUnsafe(this);
        SaveSystem.CreateAllPlayersFileBin(this);
        Debug.Log(" all player file created");


    }*/

    /*
    public void CreatePlayerFile()
    {
        //always run m
       
        SaveSystem.CreateCurrentPlayerFileUnsafe(this);
        SaveSystem.CreateCurrentPlayerFileBin(this);
        Debug.Log(" current player file created");


    }*/

    public void ReadPlayerFile()
    {
        //always run m

        // SaveSystem.CreateCurrentPlayerFileUnsafe(this);
        PlayerData rank = SaveSystem.ReadCurrentPlayerFileBin();
        Debug.Log(rank.name);
        Debug.Log(rank.highscore);
        Debug.Log(rank.rank);

        PlayerData rank2 = SaveSystem.ReadCurrentPlayerFileUnsafe();
       
        Debug.Log(" read current player file working");
        


    }

    public void ReadAllPlayerFile()
    {
        //always run m

       // SaveSystem.CreateCurrentPlayerFileUnsafe(this);
        AllPlayerData rank1 = SaveSystem.ReadAllPlayersFileBin();
        foreach (PlayerData _data in rank1.players)
        {
            Debug.Log(_data.name);
        }

        Debug.Log(rank1.players[1]);

        AllPlayerData rank = SaveSystem.ReadAllPlayersFileUnsafe();
        //Debug.Log(rank.players);
        Debug.Log(" read current player file working");


    }

    public PlayerData ReadSinglePlayerAllPlayerFile()
    {
        //always run m

        // SaveSystem.CreateCurrentPlayerFileUnsafe(this);
        string name = "Kevin";
        PlayerData playerdata = SaveSystem.ReadCurrentPlayerByNameFromAllPLayerFileBin(name);
        PlayerData playerdataU = SaveSystem.ReadCurrentPlayerByNameFromAllPLayerFileUnsafe(name);
        
        Debug.Log(playerdata.name);
        Debug.Log(playerdata.highscore);
        Debug.Log(playerdata.rank);
        return playerdata;


    }


    public void UpdatePlayerFile(string username, int highscore, int rank)
    {

        PlayerData updatedata = new PlayerData(username, highscore, rank);
        SaveSystem.UpdateCurrentPlayerFileBin(updatedata);
        SaveSystem.UpdateCurrentPlayerFileUnsafe(updatedata);
    }
    public void UpdateAllPlayerFile_AddPlayer(string username, int highscore, int rank)
    {
        PlayerData newplayer = new PlayerData(username, highscore, rank);
        SaveSystem.UpdateAllPlayerFileBin_AddPlayer(newplayer);
        SaveSystem.UpdateAllPlayerFileUnsafe_AddPlayer(newplayer);
    }

    public void UpdateAllPlayerFile_AddPlayerUnique(string username, int highscore, int rank)
    {
        PlayerData newplayer = new PlayerData( username, highscore, rank);
        SaveSystem.UpdateAllPlayerFileBin_AddPlayerUnique(newplayer);
        SaveSystem.UpdateAllPlayerFileUnsafe_AddPlayerUnique(newplayer);
    }

    public void UpdateAllPlayerFile_UpdatePlayer()
    {


        string _name = "Kevin";
        string property = "highscore";
        string value = "40";
        SaveSystem.UpdateAllPlayerFileBin_UpdatePlayer( _name, property,  value);
    }
    /*
    public void SavePlayer()
    {
        Debug.Log("working");
        SaveSystem.SavePlayerBin(this);
        SaveSystem.SavePlayerUnSafe(this);
    }
    */

}
