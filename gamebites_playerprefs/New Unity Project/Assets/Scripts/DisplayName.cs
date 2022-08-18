using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayName : MonoBehaviour
{
    

    public Text NameBox;
    void Start()
    {
        NameBox.text = PlayerPrefs.GetString("name");
    }

    public void ResetSavedGame()
    {
        PlayerPrefs.DeleteKey("name");
        SceneManager.LoadScene("SampleScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
