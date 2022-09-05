using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 3;
    public int health = 40;
    public Vector3 mousePos;

    public Text levelTxt;
    public Text healthTxt;

    public Text posX;
    public Text posY;
    public Text posZ;

    // Start is called before the first frame update
    void Start()
    {
        levelTxt.text = level.ToString();
        healthTxt.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MouseTracker();


    }
    public void SavePlayer()
    {
        Debug.Log(" save playerworking");
        SaveSystem.SavePlayer(this);
      

    }

    public void LoadPlayer()
    {
        Debug.Log(" LOAD playerworking");

        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.level);


        level = data.level;
        levelTxt.text = level.ToString();
        Debug.Log(data.level);
        Debug.Log(data.health);


        health = data.health;
        healthTxt.text = health.ToString();

        mousePos.x = data.position[0];
        mousePos.y = data.position[1];
        mousePos.z = data.position[2];
        posX.text = mousePos.x.ToString();
        posY.text = mousePos.y.ToString();
        posZ.text = mousePos.z.ToString();


    }

    public void ChangeLevel()
    {
        level += 1;
        levelTxt.text = level.ToString();
        Debug.Log("working");

    }

    public void ChangeHealth()
    {
        health += 1;
        healthTxt.text = health.ToString();
        Debug.Log("working");

    }

    public void MouseTracker()
    {
        mousePos = Input.mousePosition;
        posX.text = mousePos.x.ToString();
        posY.text = mousePos.y.ToString();
        posZ.text = mousePos.z.ToString();
    }
}
