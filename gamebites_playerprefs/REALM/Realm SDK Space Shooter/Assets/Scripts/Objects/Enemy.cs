using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float movementSpeed = 10.0f; //Enemy movement speed
  void OnEnable()
    {
        float randomPositionY = Random.Range(-4.0f, 4.0f); //picks a random y-axis position for the game object.
        transform.position = new Vector3(8.0f, randomPositionY, 0); //Assign game object to that position

    }

    void Update()
    {
        transform.position += Vector3.left * movementSpeed * Time.deltaTime; //move object left
        if (transform.position.x < -8.0f) //if past the screen then destory
        {
            gameObject.SetActive(false);
        }


    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Weapon") //if blaster hits the enemy
        {
            gameObject.SetActive(false);//de-active
            RealmController.Instance.IncreaseScore(); //call increase score on DB Ui will auto update with game controller
        }
    }
}
