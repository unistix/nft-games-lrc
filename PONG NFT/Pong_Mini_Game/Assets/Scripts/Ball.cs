using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 200.0f; //large arbitrary value adjust as necessary or have the speed increase with each level 
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        AddStartingForce(); //recenter on start to avoid glitches
        ResetPosition();
        
    }

    public void AddStartingForce()
    {
        // starts ball moving
        //pick a random direction in x  to determin what direction it moves in first
        float x = Random.value < 0.5f ? -1.0f : 1.0f;   //if less than half go one direction if greater go the other + coinflip logic in unity

        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : //up is negative while down is postive - if you hard coded 0 it will go totally horizontal
                                        Random.Range(0.5f, 1.0f); //sets an angle that isn't 0 but constantly changing

        //create a direction vector for clarity instead of a super long function
        Vector2 dir = new Vector2(x, y);

        //using these values we can apply a force to the ball - vector2 constructed using x and y
        rb.AddForce(dir*this.speed);

        //fix wall sticking through unity physics engine
        //create physics material
        //apply the material to the 2D rigid bodty component in the material param


    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void ResetPosition()
    {
        rb.position = Vector2.zero; //reset position to center
        rb.velocity = Vector2.zero; // reset velocity to zero

        //AddStartingForce(); // re add starting force
    }

}
