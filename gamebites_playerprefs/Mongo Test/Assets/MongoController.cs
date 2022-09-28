using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MongoDB.Driver;

public class MongoController : MonoBehaviour
{
    public Text data;
    string dbs;
    // Start is called before the first frame update
    void Start()
    {
        MongoClient dbClient = new MongoClient("mongodb+srv://unistix:Cupcake1234@realm-sdk-test.n4uhsvr.mongodb.net/?retryWrites=true&w=majority");

        var dbList = dbClient.ListDatabases().ToList();

        //List<string> dbs = new List<string>();

        Debug.Log("The list of databases on this server is: ");
        foreach (var db in dbList)
        {
            Debug.Log(db);
            dbs = dbs+ " " + db.ToString();
            
        }
        data.text = dbs;
    }


}
