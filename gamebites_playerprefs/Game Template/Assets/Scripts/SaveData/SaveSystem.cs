using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;


public class SaveSystem : MonoBehaviour
{
    
    // ---------CREATE------------------------
    //CREATE DB ALL PLAYERS
    public static void CreateAllPlayersFileBin (PlayerData data)
    {
        //creates the file which stores all the players only use once
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/allplayers.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(player); 
        AllPlayerData alldata = new AllPlayerData(data);

        formatter.Serialize(stream, alldata);
        stream.Close();

        Debug.Log("creating all player file safe");
        Debug.Log("dataPath : " + path);


    }

    public static void CreateAllPlayersFileUnsafe(PlayerData data)
    {
        //creates the file which stores all the players only use once


        string path = Application.persistentDataPath + "/allplayers.json";
        //PlayerData data = new PlayerData(player);
        AllPlayerData alldata = new AllPlayerData(data);

        string json = JsonUtility.ToJson(alldata);
        Debug.Log(alldata);
        //create two entries wrapped together 
        

        File.WriteAllText(path, json);

        Debug.Log("creating all player file unsafe");
        Debug.Log("dataPath : " + path);


    }

    //CREATE DB CURRENT PLAYER
    
    public static void CreateCurrentPlayerFileBin(PlayerData data)
    {

        //At first this is manual but eventually this should be read from all player db not passed in  - Things got interesting now we have both
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/currentplayer.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(player);
        

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("creating current player file safe");
        Debug.Log("dataPath : " + path);


    }

    public static void CreateCurrentPlayerFileUnsafe(PlayerData data)
    {      
        string path = Application.persistentDataPath + "/currentplayer.json";

        //PlayerData data = new PlayerData(player);
   

        string json = JsonUtility.ToJson(data);
     
 

        File.WriteAllText(path, json);

 
        Debug.Log("creating current player file unsafe");
        Debug.Log("dataPath : " + path);


    }


    // ---------READ------------------------
    //READ ALL PLAYER DATA
    public static AllPlayerData ReadAllPlayersFileBin()
    {
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;

            stream.Close();

            Debug.Log(data);
            Debug.Log("reading all playerfile safe");
            return data;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }

    public static AllPlayerData ReadAllPlayersFileUnsafe()
    {
        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(path, FileMode.Open);
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);


            Debug.Log(data);
            Debug.Log("reading all playerfile safe");
            return data;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }

    public static AllPlayerData ReadAllPlayersFileBin_Sorted()
    {
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            data.players.OrderByDescending(p => p.highscore).ToList();

            stream.Close();

            Debug.Log(data);
            Debug.Log("reading all playerfile safe");
            return data;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }

    public static AllPlayerData ReadAllPlayersFileUnsafe_Sorted()
    {
        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(path, FileMode.Open);
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);
            data.players.OrderByDescending(p => p.highscore).ToList();


            Debug.Log(data);
            Debug.Log("reading all playerfile safe");
            return data;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }



    //READ CURRENT PLAYER DATA

    public static PlayerData ReadCurrentPlayerFileBin()
    {
        string path = Application.persistentDataPath + "/currentplayer.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            //Debug.Log(data.name);
            Debug.Log("reading current playerfile safe");
            return data;
            
        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }
    
    public static PlayerData ReadCurrentPlayerFileUnsafe()
    {
        string path = Application.persistentDataPath + "/currentplayer.json";
        if (File.Exists(path))
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            //FileStream stream = new FileStream(path, FileMode.Open);
            string stringdata = File.ReadAllText(path);

            PlayerData data = JsonUtility.FromJson<PlayerData>(stringdata);


            
            Debug.Log("reading current playerfile safe");
            return data;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }

    //READ CURRENT PLAYER DATA FROM ALL DATA

    public static PlayerData ReadCurrentPlayerByNameFromAllPLayerFileUnsafe(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately
   
         string path = Application.persistentDataPath + "/allplayers.json";
         if (File.Exists(path))
         {
             string stringdata = File.ReadAllText(path);
             AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);
             PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());

           
   
             string json = JsonUtility.ToJson(data);
            /* Debug.Log(playerdata.name);
             Debug.Log(playerdata.highscore);
             Debug.Log(playerdata.rank);*/
        
            Debug.Log("reading current player from all player data unsafe");
            return playerdata;

         }
         else
         {
             Debug.LogError("File not found in " + path);
            return null;

         }
    }

    public static PlayerData ReadCurrentPlayerByNameFromAllPLayerFileBin(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately


        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;

            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            stream.Close();

            string json = JsonUtility.ToJson(data);
            /*Debug.Log(playerdata.name);
            Debug.Log(playerdata.highscore);
            Debug.Log(playerdata.rank);*/
     
            Debug.Log("reading current player from all player data unsafe");
            return playerdata;


        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;

        }
    }


    public static PlayerData ReadCurrentPlayerByNameFromAllPLayerFileUnsafe_Sorted(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately

        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            string stringdata = File.ReadAllText(path);
            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);
            data.players.OrderByDescending(p => p.highscore).ToList();
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());




            string json = JsonUtility.ToJson(data);
            /* Debug.Log(playerdata.name);
             Debug.Log(playerdata.highscore);
             Debug.Log(playerdata.rank);*/

            Debug.Log("reading current player from all player data unsafe");
            return playerdata;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;

        }
    }

    public static PlayerData ReadCurrentPlayerByNameFromAllPLayerFileBin_Sorted(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately


        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            stream.Close();
            data.players.OrderByDescending(p => p.highscore).ToList();
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());

            string json = JsonUtility.ToJson(data);
            /*Debug.Log(playerdata.name);
            Debug.Log(playerdata.highscore);
            Debug.Log(playerdata.rank);*/

            Debug.Log("reading current player from all player data unsafe");
            return playerdata;


        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;

        }
    }

    public static int ReadCurrentRankFromAllUnsafe(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately

        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            string stringdata = File.ReadAllText(path);
            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);
            List<PlayerData> datadesc = data.players.OrderByDescending(p => p.highscore).ToList();

            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            int index = datadesc.FindIndex(i => i.name.ToLower() == _name.ToLower());



            string json = JsonUtility.ToJson(data);
            /* Debug.Log(playerdata.name);
             Debug.Log(playerdata.highscore);
             Debug.Log(playerdata.rank);*/

            Debug.Log("reading current player from all player data unsafe");
            return index;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return -1;

        }
    }

    public static int ReadCurrentRankFromAllBin(string _name)
    {
        //Add By Name because we might need to create one for wallet addres separately


        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            stream.Close();
            List <PlayerData> datadesc = data.players.OrderByDescending(p => p.highscore).ToList();
           
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            int index = datadesc.FindIndex(i => i.name.ToLower() == _name.ToLower());

            string json = JsonUtility.ToJson(data);
            /*Debug.Log(playerdata.name);
            Debug.Log(playerdata.highscore);
            Debug.Log(playerdata.rank);*/

            Debug.Log("reading current player from all player data unsafe");
            return index;


        }
        else
        {
            Debug.LogError("File not found in " + path);
            return -1;

        }
    }


    // ------------UPDATE-------------

    //Update CURRENT PLAYER DATA
    //read 
    //edit json
    //write to file
    public static void UpdateCurrentPlayerFileBin(PlayerData updatedata)
    {
        
        //read current player data
        string path = Application.persistentDataPath + "/currentplayer.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            //stream.Close();
           
           



            formatter.Serialize(stream, updatedata);
            stream.Close();

            Debug.Log(" updating current player safe");

        }
        else
        {
            Debug.LogError("File not found in " + path);
        }
    }

    public static void UpdateCurrentPlayerFileUnsafe(PlayerData updatedata)
    {

        //read current player data
        string path = Application.persistentDataPath + "/currentplayer.json";
        if (File.Exists(path))
        {
            string stringdata = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(stringdata);

            string json = JsonUtility.ToJson(updatedata);
            File.WriteAllText(path, json);

            Debug.Log(" updating current player unsafe");

        }
        else
        {
            Debug.LogError("File not found in " + path);

        }
    }

    

    //Update All Players with new Player
    public static void UpdateAllPlayerFileUnsafe_AddPlayer(PlayerData newplayer) //Not Unique
    {

        //read current player data
        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);

            
            data.players.Add(newplayer);
 

            string json = JsonUtility.ToJson(data);
            Debug.Log(path);
            Debug.Log(" updating adding new player to all players unsafe");


            File.WriteAllText(path, json);

        }
        else
        {
            Debug.LogError("File not found in " + path);

        }
    }
    public static void UpdateAllPlayerFileBin_AddPlayer(PlayerData newplayer) //Not Unique
    {

        //read current player data
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            //read
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);



            //update
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData; 
            stream.Close();
            data.players.Add(newplayer); 
       
            //write
            BinaryFormatter cformatter = new BinaryFormatter();
            FileStream cstream = new FileStream(path, FileMode.Create);

            cformatter.Serialize(cstream, data);
            stream.Close();
            Debug.Log(" updating adding new player to all players safe");


        }
        else
        {
            Debug.LogError("File not found in " + path);

        }
    }

    public static bool UpdateAllPlayerFileUnsafe_AddPlayerUnique(PlayerData newplayer) 
    {

        //read current player data
        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);


            string _name = newplayer.name;
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            if (playerdata != null)
            {
                Debug.Log("Username Exists");
                
                return false;

            }
            else
            {
                data.players.Add(newplayer);
                
            }

            // AllPlayerData alldata = new AllPlayerData(data);

            string json = JsonUtility.ToJson(data);
            Debug.Log(path);
            Debug.Log(" updating adding new unique player to all players unsafe");
            //create two entries wrapped together 


            File.WriteAllText(path, json);
            return true;


        }
        else
        {
            Debug.LogError("File not found in " + path);
            return false;

        }

        //return if user exists or not
    }
    public static bool UpdateAllPlayerFileBin_AddPlayerUnique(PlayerData newplayer) 
    {

        //read current player data
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            //read
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            //update
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            stream.Close();
            string _name = newplayer.name;
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            if (playerdata != null)
            {
                Debug.Log("Username Exists");
                return false;
            }
            else
            {
                data.players.Add(newplayer);
            }

            //write
            BinaryFormatter cformatter = new BinaryFormatter();
            FileStream cstream = new FileStream(path, FileMode.Create);

            cformatter.Serialize(cstream, data);
            stream.Close();


            Debug.Log(" updating adding new unique player to all players safe");
            return true;

        }
        else
        {
            Debug.LogError("File not found in " + path);
            return false;

        }
        //return if user exists or not
    }


    public static void UpdateAllPlayerFileBin_UpdatePlayer(string _name, string property, string value)
    {
        //read current player data
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            //read
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);



            
            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            stream.Close();
            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());


            string json = JsonUtility.ToJson(data);

            //Update player with values

         
            if (playerdata != null)
            {
                playerdata.name = _name;
                if (property == "highscore")
                {
                    playerdata.highscore = int.Parse(value);
                }
                else if (property == "rank")
                {
                    playerdata.highscore = int.Parse(value);
                }
            }

            PlayerData playerdata2 = data.players.Find(i => i.name.ToLower() == _name.ToLower());


            //write
            BinaryFormatter cformatter = new BinaryFormatter();
            FileStream cstream = new FileStream(path, FileMode.Create);

            cformatter.Serialize(cstream, data);
            stream.Close();

            Debug.Log(path);
            Debug.Log(" updating all players with new player values unsafe");




        }
        else
        {
            Debug.LogError("File not found in " + path);

        }
    }
    public static void UpdateAllPlayerFileUnsafe_UpdatePlayer(string _name, string property, string value)
    {

        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            //Read player data
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);

            PlayerData playerdata = data.players.Find(i => i.name.ToLower() == _name.ToLower());
            

            string json = JsonUtility.ToJson(data);

           //Update player with values

            Debug.Log(playerdata.name);
            Debug.Log(playerdata.highscore);
            Debug.Log(playerdata.rank);
            if (playerdata != null)
            {
                playerdata.name = _name;
                if (property == "highscore")
                {
                    playerdata.highscore = int.Parse(value);
                }
                else if (property == "rank")
                {
                    playerdata.highscore = int.Parse(value);
                }
            }

            PlayerData playerdata2 = data.players.Find(i => i.name.ToLower() == _name.ToLower());


            //list.Find(i => i.Property == value);                 <--- Filter on specific values eg. name 
            //list.Find(delegate (Item i) { return i.Property == value; }); // C# 2.0+
            //List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList(); sorting the list 

          

            //write players list back to file





            json = JsonUtility.ToJson(data);
            Debug.Log(path);
            //create two entries wrapped together 


            File.WriteAllText(path, json);
            Debug.Log(" updating all players with new player values unsafe");
            //create two entries wrapped together 


            //File.WriteAllText(path, json);







        }
        else
        {
            Debug.LogError("File not found in " + path);
            

        }
    }







    // ------------DELETE-------------
    //DELETE DB CURRENT PLAYER (Clear data at the end of session)

    public static void DeleteCurrentPlayerFile()
    {


        Debug.Log("working not bin");
        string pathjson = Application.persistentDataPath + "/currentplayer.json"; //make dynamic with key and time?
        string pathbin = Application.persistentDataPath + "/currentplayer.bin";




        File.Delete(pathjson);
        File.Delete(pathbin);


        Debug.Log("current player file deleted");



    }

    public static void DeletePlayerFromAllBin(string _name)
    {
        //read current player data
        string path = Application.persistentDataPath + "/allplayers.bin";
        if (File.Exists(path))
        {
            //read
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);




            AllPlayerData data = formatter.Deserialize(stream) as AllPlayerData;
            stream.Close();
            data.players.RemoveAll(i => i.name.ToLower() == _name.ToLower());


            

            //write
            BinaryFormatter cformatter = new BinaryFormatter();
            FileStream cstream = new FileStream(path, FileMode.Create);

            cformatter.Serialize(cstream, data);
            stream.Close();

            Debug.Log(path);
            Debug.Log(" updating all players with new player values unsafe");




        }
        else
        {
            Debug.LogError("File not found in " + path);

        }
    }
    public static void DeletePlayerFromAllUnsafe(string _name)
    {

        string path = Application.persistentDataPath + "/allplayers.json";
        if (File.Exists(path))
        {
            //Read player data
            string stringdata = File.ReadAllText(path);

            AllPlayerData data = JsonUtility.FromJson<AllPlayerData>(stringdata);

            data.players.RemoveAll(i => i.name.ToLower() == _name.ToLower());


            string json = JsonUtility.ToJson(data);

            

            File.WriteAllText(path, json);
            Debug.Log(" updating all players with new player values unsafe");
            //create two entries wrapped together 


            //File.WriteAllText(path, json);







        }
        else
        {
            Debug.LogError("File not found in " + path);


        }
    }















    public static void SavePlayerBin(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/allplayers.bin";
        FileStream stream = new FileStream(path, FileMode.Append);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("player added");
        Debug.Log("dataPath : " + path);


    }

  

    public static void SavePlayerUnSafe(Player player)
    {
        string path = Application.persistentDataPath + "/allplayers.json";


        PlayerData data = new PlayerData(player);
        string json = JsonUtility.ToJson(data);
        //File.Replace();
        File.AppendAllText(path, ",");
        File.AppendAllText(path, json);
        Debug.Log("player added");
        Debug.Log("dataPath : " + path);


    }
    

    public static void UpdateGameState(PlayerData updatedata)
    {
        UpdateCurrentPlayerFileBin(updatedata);
        UpdateCurrentPlayerFileUnsafe(updatedata);
        //UpdateAllPlayerFileBin_UpdatePlayer(updatedata.)


    }
    /*
    public static void LoadPlayerBin(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/allplayers.bin";
        FileStream stream = new FileStream(path, FileMode.Append);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("player added");
        Debug.Log("dataPath : " + path);


    } */
}
