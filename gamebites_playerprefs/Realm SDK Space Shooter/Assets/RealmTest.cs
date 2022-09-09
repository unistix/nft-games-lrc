using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;

public class RealmTest : MonoBehaviour
{
    private Realm realm;
    private string username;
    private string password;
    private string appID;
    private App realmApp;
    async void OnEnable()
    {
        
        var env = new Env();
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

        Debug.Log("Sync Session State");
        Debug.Log(realm.SyncSession.ConnectionState);

       
        






    }
}
