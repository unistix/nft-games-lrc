using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    // Start is called before the first frame update
    //allows for flexibility of bouncing on different surfaces
    //? Rather than level based only backwalls increase speed = smoother game?
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float bounceStrength;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when we add a bouncy surface to this script this gets called when the ball collisides with it

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            //object collides with ball
            Vector2 normal = collision.GetContact(0).normal; //gets the contact point of the collision
            ball.AddForce(-normal * this.bounceStrength); //normal is direction and strength is the scalar representation of speed
            //normal is vector pointing away from the surface at that point
        }


        //easier way to do this 
        //check if the object tag is a ball the run the process
        //althought this might be more efficient 

    }
}
