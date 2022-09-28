using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;




class ColorEntity : RealmObject //tells realm the data can be stored
{
    [PrimaryKey] //set primary ket as the object name quick simple unique player IDS
    [MapTo("_id")]
    public string ObjectName { get; set; }
    // Start is called before the first frame update
    public float Red { get; set; } = 0f;
    public float Green { get; set; } = 0f;

    public float Blue { get; set; } = 0f;

    public ColorEntity()
    {
        //default initialiser
    }

    public ColorEntity(string objectName)
    {
        //convenience initialiser set object name
        ObjectName = objectName;
    }

    



}

