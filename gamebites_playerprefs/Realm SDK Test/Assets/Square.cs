using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;

public class Square : MonoBehaviour
{
    private ColorEntity squareColor;
    private Realm realm;
    private string username;
    private string password;
    private string appID;
    private App realmApp;
    public SpriteRenderer square;
    // Start is called before the first frame update


    async void OnEnable (){

        var env = new Env();
        username = env.realm_name;
        password = env.realm_pass;
        appID = env.appID;

        Debug.Log(env.realm_name);
        Debug.Log(env.realm_pass);



        realmApp = App.Create(new AppConfiguration(appID)
        {
            MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
        });

        var realmUser = realmApp.CurrentUser;

        
            //realmUser = realmApp.CurrentUser;
            if (realmUser == null)
            {
                realmUser = await realmApp.LogInAsync(Credentials.EmailPassword(username, password));
                realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(username, realmUser));
            }
            else
            {
                realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(username, realmUser));
            }
       
        Debug.Log(realm);
        

        squareColor = realm.Find<ColorEntity>("square7");
        if (squareColor == null)
        {
            //Only create the object if it can't be found.
            squareColor = new ColorEntity("sqaure7");
            //CRUD OPERATIONS - Write
            realm.Write(() =>
            {
                realm.Add(squareColor);
                //write ensures no other pocesses running similatanoesly - atomic action
            });
        }

        SetColor();


    }
    private void OnDisable()
    {
      
    }

    /*
    private async void Awake()
    {
        

       

        //await realmUser.LogOutAsync();


        //RealmConnect();


       


    } */

    // Update is called once per frame
    private void OnMouseDown()
    {
        //Whenever click, the object is also updated so we update the DB as well
        
        Debug.Log("mse clkd");
        realm.Write(() =>
        {
            squareColor.Red = Random.Range(0f, 1f);
            squareColor.Green = Random.Range(0f, 1f);
            squareColor.Blue = Random.Range(0f, 1f);
            //write ensures no other pocesses running similatanoesly - atomic action
        });
       
        SetColor();
    }

    private void  SetColor()
    {
        /*var newColor = new Color(squareColor.Red, squareColor.Green, squareColor.Blue);
        Debug.Log(newColor);

        gameObject.GetComponent<SpriteRenderer>().color = newColor;*/
        gameObject.GetComponent<SpriteRenderer>().color = new Color(squareColor.Red, squareColor.Green, squareColor.Blue);
    }

    public void RealmConnect()
    {
        realm = Realm.GetInstance();//acess DB



        //CRUD OPERATIONS - Read/Query
        //check if color entityexists and if yes we don't need to create it again
        squareColor = realm.Find<ColorEntity>("square3");
        if (squareColor == null)
        {
            //Only create the object if it can't be found.
            squareColor = new ColorEntity("sqaure3");
            //CRUD OPERATIONS - Write
            realm.Write(() =>
            {
                realm.Add(squareColor);
                //write ensures no other pocesses running similatanoesly - atomic action
            });
        }
    }

    // - this is how you log users out.
    //Realm.Credentials.custom(token) - Most likely what will be instead of annoymous or email pass used

}
