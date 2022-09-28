using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System.Threading.Tasks;
using UnityEngine.UI;
public class _RealmController : MonoBehaviour
{

    public static _RealmController Instance;

    public string RealmAppId = "gamebites-asteroids-npfgs";
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
    public async void Login(string email, string password)
    {
       
            _realmApp = App.Create(new AppConfiguration(RealmAppId)
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            
                
                    //if user is not null create login in existing and update text > Loggin in
                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = Realm.GetInstance(new PartitionSyncConfiguration(email, _realmUser));
                    Debug.Log(_realm.SyncSession.ConnectionState);
                    Debug.Log(_realmUser.Id);
                
                





    }
    public void Start()
    {
        Login("c.dafinone@gmail.com", "Cupcake1234");
    }
   

    

}