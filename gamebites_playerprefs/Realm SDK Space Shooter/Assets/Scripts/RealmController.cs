using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Threading.Tasks;

public class RealmController : MonoBehaviour
{
    public static RealmController Instance;

    private string RealmAppId;

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;
    Task<string> tstrintest;

    void Awake()
    {
        var env = new Env();
        RealmAppId = env.appID;

        DontDestroyOnLoad(gameObject);
        Instance = this;

        tstrintest = Login(env.realm_name, env.realm_pass);//test Login eventually this will be a button 
    }

    void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

    private void Update()
    {
        //Score Button Test

        PlayerProfile _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            
                Debug.Log(_playerProfile.Score);
            
        }
    }

    //Keep functions which effect DB within Realm controller
    //Don't create multiple realm controller scripts in this scene
    //Dont create specific functions for loading data since loading is now built in


    public async Task<string> Login(string email, string password)
    {
        if (email != "" && password != "")
        {
            _realmApp = App.Create(new AppConfiguration(RealmAppId)
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            try
            {
                if (_realmUser == null)
                {

                    //This is where the UI light error handling should take NOT place
                    //if use is null create a new user and update text > Creating New Account 

                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(email, _realmUser));
                    Debug.Log(_realm.SyncSession.ConnectionState);
                }
                else
                {
                    //if user is not null create login in existing and update text > Loggin in
                    _realm = Realm.GetInstance(new PartitionSyncConfiguration(email, _realmUser));
                    Debug.Log(_realm.SyncSession.ConnectionState);
                }
            }
            catch (ClientResetException clientResetEx)
            {
                if (_realm != null)
                {
                    _realm.Dispose();
                }
                clientResetEx.InitiateClientReset();
            }
            return _realmUser.Id;

        }

        return "";

        
        


    }

    public PlayerProfile GetPlayerProfile()
    {
        //This is where the UI light error handling should take NOT place - it will be called regulary
        PlayerProfile _playerProfile = _realm.Find<PlayerProfile>(_realmUser.Id);

        if (_playerProfile == null)
        {

           
            _realm.Write(() => {
                _playerProfile = _realm.Add(new PlayerProfile(_realmUser.Id));
            });
        }

       
        return _playerProfile;


    }



    public void IncreaseScore()
    {
        PlayerProfile _playerProfile = GetPlayerProfile();
        if (_playerProfile != null)
        {
            _realm.Write(() =>
            {
                _playerProfile.Score++;
            });
        }
    }
    
    /*

    public void ResetScore() { }

    public bool IsSparkBlasterEnabled() { }

    public bool IsCrossBlasterEnabled() { }*/

}
