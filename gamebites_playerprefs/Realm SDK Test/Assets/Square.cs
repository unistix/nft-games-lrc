using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class Square : MonoBehaviour
{
    private ColorEntity squareColor;
    private Realm realm;
    // Start is called before the first frame update
    private void Awake()
    {
        realm = Realm.GetInstance();//acess DB

        //CRUD OPERATIONS - Read/Query
            //check if color entityexists and if yes we don't need to create it again

        squareColor = new ColorEntity();
        //CRUD OPERATIONS - Write
        realm.Write(() =>
        {
            realm.Add(squareColor);
            //write ensures no other pocesses running similatanoesly - atomic action
        });


        SetColor();
       

    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        Debug.Log("mse clkd");
        squareColor.Red = Random.Range(0f, 1f);
        squareColor.Green = Random.Range(0f, 1f);
        squareColor.Blue = Random.Range(0f, 1f);
        SetColor();
    }

    private void  SetColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(squareColor.Red, squareColor.Green, squareColor.Blue);
    }
}
