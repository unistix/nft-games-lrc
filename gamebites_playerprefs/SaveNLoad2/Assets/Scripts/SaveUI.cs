using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    public void SavePlayer()
    {
        Debug.Log("working");
        SaveSystem.SavePlayer(player);
        
    }

    // Update is called once per frame
    public void LoadPlayer(Player player)
    {
       

        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.level);


        player.level = data.level;


        player.levelTxt.text = player.level.ToString();
        Debug.Log(data.level);

        


        player.health = data.health;

        player.mousePos.x = data.position[0];
        player.mousePos.y = data.position[1];
        player.mousePos.z = data.position[2];


    }
}