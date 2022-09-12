using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{

    public Button LoginButton;
    //public Button TestButton;
    public InputField UsernameInput;
    public InputField PasswordInput;// Start is called before the first frame update
                                    //private string appID;


    // Update is called once per frame
    void Start()
    {
        var env = new Env();

        UsernameInput.text = env.realm_name;
        PasswordInput.text = env.realm_pass;

        LoginButton.onClick.AddListener(Login);
        //TestButton.onClick.AddListener(IncreaseScore);
    }

    async void Login()
    {
        if (await RealmController.Instance.Login(UsernameInput.text, PasswordInput.text) != "")
        {
            SceneManager.LoadScene("MainScene");
          


        }
    }

    /* public void IncreaseScore()
    {
        RealmController.Instance.IncreaseScore();
        Debug.Log("test clicked");
    }*/

}
