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
        MouseTracker()


    }

    public void ChangeLevel()
    {
        level += 1;
        levelTxt.text = level.ToString();

    }

    public void ChangeHealth()
    {
        health += 1;
        healthTxt.text = health.ToString();

    }

    public void MouseTracker()
    {
        mousePos = Input.mousePosition;
        posX.text = mousePos.x.ToString();
        posY.text = mousePos.y.ToString();
        posZ.text = mousePos.z.ToString();
    }
}
