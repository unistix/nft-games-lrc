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
        squareColor = realm.Find<ColorEntity>("square1");
            if (squareColor == null)
            {
                //Only create the object if it can't be found.
                squareColor = new ColorEntity("sqaure1");
                //CRUD OPERATIONS - Write
                realm.Write(() =>
                {
                    realm.Add(squareColor);
                    //write ensures no other pocesses running similatanoesly - atomic action
                });
            }
        

       


        //SetColor();
       

    }

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
        gameObject.GetComponent<SpriteRenderer>().color = new Color(squareColor.Red, squareColor.Green, squareColor.Blue);
    }
}
