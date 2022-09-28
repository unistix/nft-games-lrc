using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Threading.Tasks;
using UnityEngine.UI;
public class RealmController : MonoBehaviour
{

    public static RealmController Instance;

    public string RealmAppId = "space-shooter-svqqq";
    public string realUserID;
    

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;
    //public Button TestButton;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(base.gameObject);
        }
        else
        {
            Destroy(base.gameObject);
        }

        //TestButton = GameObject.Find("Increase").GetComponent<Button>();
        //TestButton.onClick.AddListener(IncreaseScore);
    }
    
    void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }
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
                try
                {
                    //if user is not null create login in existing and update text > Loggin in
                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = Realm.GetInstance(new PartitionSyncConfiguration(email, _realmUser));
                    Debug.Log(_realm.SyncSession.ConnectionState);
                    return _realmUser.Id;
                }
                catch
                {
                    Debug.Log("User does not exist");
                    await _realmApp.EmailPasswordAuth.RegisterUserAsync(email, password);
                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(email, _realmUser));



                    _realm.Write(() => {
                        PlayerProfile _playerProfile = _realm.Add(new PlayerProfile(_realmUser.Id));
                    });
                    Debug.Log("User added");

                    Debug.Log(_realm.SyncSession.ConnectionState);
                    //return _realmUser.Id;

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
        //Debug.Log(realUserID);
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
            _realm.Write(() => {
                _playerProfile.Score++;
                Debug.Log(_playerProfile.Score);
            });
        }
    }

    

   public void ResetScore() {
        PlayerProfile _playerProfile = GetPlayerProfile();
        if(_playerProfile != null) {
            _realm.Write(() => {
                if(_playerProfile.Score > _playerProfile.HighScore) {
                    _playerProfile.HighScore = _playerProfile.Score;
                }
                _playerProfile.Score = 0;
            });
        }
    }

  

    public bool IsSparkBlasterEnabled()
    {
        PlayerProfile _playerProfile = GetPlayerProfile();
        return _playerProfile != null ? _playerProfile.SparkBlasterEnabled : false;
    }

    public bool IsCrossBlasterEnabled()
    {
        PlayerProfile _playerProfile = GetPlayerProfile();
        return _playerProfile != null ? _playerProfile.CrossBlasterEnabled : false;
    }

}