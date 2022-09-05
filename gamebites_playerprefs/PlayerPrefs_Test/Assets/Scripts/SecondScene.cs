using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondScene : MonoBehaviour
{
    public Text NameBox;
    public string playerPrefsName = "name";



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("name"));
        NameBox.text = PlayerPrefs.GetString("name");
        //Debug.Log(PlayerPrefs.GetString(playerPrefsName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
