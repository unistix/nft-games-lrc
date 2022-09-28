using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    public Text getOutput;
    public Text postOutput;
    private void Start()
    {
        //StartCoroutine(MakeRequests());
        //StartCoroutine(SimplePostRequest());
        StartCoroutine(GetRequest());
        StartCoroutine(PostRequest());
    }

    

    private IEnumerator PostRequest()
    {
        string url = "https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/addOne";
        WWWForm form = new WWWForm();
        form.AddField("myField", "WEBGL");
        form.AddField("Game Name", "WORKS");
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

    private IEnumerator GetRequest()
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






    private IEnumerator SimplePostRequest()
    {
        //List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        //wwwForm.Add(new MultipartFormDataSection("score", "test"));
        var request = new UnityWebRequest("https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/insertone", RequestType.POST.ToString());
        var dataToPost = new PostData() { Hero = "John Wick", PowerLevel = 9001 };
        request.chunkedTransfer = false;

        var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(dataToPost));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        Debug.Log(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        //UnityWebRequest www = UnityWebRequest.Put("https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/insertone", request);

        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    
    private IEnumerator MakeRequests()
    {
        // GET
        var getRequest = CreateRequest("https://eu-west-1.aws.data.mongodb-api.com/app/space-shooter-svqqq/endpoint/getall");
        yield return getRequest.SendWebRequest();
        //var deserializedGetData = JsonUtility.FromJson<Todo>(getRequest.downloadHandler.text);
        Debug.Log(getRequest.downloadHandler.text);
        getOutput.text = getRequest.downloadHandler.text;

        /*
        // POST
        var dataToPost = new PostData() { Hero = "John Wick", PowerLevel = 9001 };
        var postRequest = CreateRequest("https://reqbin.com/echo/post/json", RequestType.POST, dataToPost);
        yield return postRequest.SendWebRequest();
        //var deserializedPostData = JsonUtility.FromJson<PostResult>(postRequest.downloadHandler.text);
        Debug.Log(postRequest.downloadHandler.text);*/
    }


    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, PostData data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            Debug.Log(data);
            Debug.Log(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }
}

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}


public class Todo
{
    // Ensure no getters / setters
    // Typecase has to match exactly
    public int userId;
    public int id;
    public string title;
    public bool completed;
}

[Serializable]
public class PostData
{
    public string Hero;
    public int PowerLevel;
}

public class PostResult
{
    public string success { get; set; }
}
