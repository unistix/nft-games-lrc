using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    public InputField textBox;
    public InputField pointBox;
    public string playerPrefsName = "name";
    public string playerPrefsPoints = "points";
    public string playerPrefsNamePoints = "namePoints";

    private string loadPlayer;
    private string loadPoints;

    //player prefs stored individual data for the session but not full data. can't track name and points just name or points.


    public void saveButton() //this is actually the create account button 
    {
        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log("Your name is " + PlayerPrefs.GetString("name"));

        /*PlayerPrefs.SetString(playerPrefsPoints, pointBox.text);
        Debug.Log("Your name is " + PlayerPrefs.GetString(playerPrefsPoints));*/
    }

    public void loadButton()
    {
        loadPlayer = PlayerPrefs.GetString(playerPrefsName, textBox.text);
        Debug.Log("Your name is " + loadPlayer);
        loadPoints = PlayerPrefs.GetString(playerPrefsPoints);
        Debug.Log("Your name is " + loadPoints);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
