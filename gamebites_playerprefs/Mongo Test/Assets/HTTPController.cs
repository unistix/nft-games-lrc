using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;

public class HTTPController : MonoBehaviour
{

    private static readonly HttpClient client = new HttpClient();
    // Start is called before the first frame update
    void  Start()
    {
       
    }

    public void async Test()
    {

        var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");
    }


}
