using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;

public class WebController : MonoBehaviour
{
    //public static NetworkManager Instance;
    public Text getOutput;
    public Text postOutput;

    public void PostRequest(string wallet)
    {
        StartCoroutine(_PostRequest(wallet));
    }

    public void GetRequest()
    {
        StartCoroutine(_GetRequest());
    }
    private  IEnumerator _PostRequest(string wallet)
    {
        string url = "https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/addOne";
        WWWForm form = new WWWForm();
        form.AddField("myField", "Metamask");
        form.AddField("Game Name", "WORKS");
        form.AddField("Game Name", wallet);
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            postOutput.text = uwr.downloadHandler.text;
        }
    }

    private IEnumerator _GetRequest()
    {
        string url = "https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/getall";
        /*WWWForm form = new WWWForm();
        form.AddField("myField", "myData");
        form.AddField("Game Name", "Mario Kart");*/

        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            getOutput.text = uwr.downloadHandler.text;
        }
    }
}
