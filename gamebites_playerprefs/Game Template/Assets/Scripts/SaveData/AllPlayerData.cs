using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllPlayerData 
{
    public List<PlayerData> players = new List<PlayerData>();
    public AllPlayerData(PlayerData playerdata)
    {
        
        players.Add(playerdata);
       


    }
}
