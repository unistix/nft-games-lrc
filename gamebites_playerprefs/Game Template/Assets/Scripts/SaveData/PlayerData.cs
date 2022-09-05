using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int highscore;
    public int rank;

    public PlayerData (Player player)
    {
        name = player._name;
        highscore = player.highscore;
        rank = player.rank;
        Debug.Log("constructor1 working");

    }

    //testing constructor - keep for quick data objects without player duplication
    public PlayerData(string _name, int _highscore, int _rank)
    {
        name = _name;
        highscore = _highscore;
        rank = _rank;
        Debug.Log("constructor no new player working");

    }

    public PlayerData(string _name)
    {
        name = _name;
        highscore = 0;
        rank = 0;
        Debug.Log("constructor no new player working");

    }



}
