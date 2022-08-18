using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaddle : Paddle
{

    //Simple AI computer paddle tracks postion of ball

    public Rigidbody2D ball;
    //ball and paddle are separate so make ball public and drop in from editor
    //rather then declareing the whole game object or declare the component then assign the object in editor




    private void FixedUpdate()
    {
        //always use fixed update when working with physics
        //when the ball comes toward the paddle
        //paddle moves up if ball is above
        //paddle moves down if ball is below

       
            

        if (this.ball.velocity.x > 0.0f)  //if ball velocity x is greater than zero ball is moving right towards paddle
        {
            //paddle moves up or down based on y position of ball
            if (this.ball.position.y > this.transform.position.y) //ball is above padd
            {
                // add a force to move up
                rb.AddForce(Vector2.up * this.speed); //Paddle speed is established at baseclass but override is possible for increased speed and leveling.
            } else if  (this.ball.position.y < this.transform.position.y) //if ball is below paddle
            {
                // add downward force
                rb.AddForce(Vector2.down * this.speed);
            }
            //if theyre the same do nothing, could just have first if statement but then it would move constantly
            // no force applied the paddle is idle
        }
        else
        {
            //slowly move to wards the center of the field if the ball is moving away from the paddle rather than chasing it

            //variablity gives player oppurtinity to score
            // by having the computer idle mid playing field it the Computer doesn't know where the ball will be going next adding human like quality

            //slowly move to wards the center of the field if the ball is moving away from the paddle rather than chasing it 
            if (this.transform.position.y > 0.0f)
            {
                //if current position is greaer than 0 (center point), move down
                rb.AddForce(Vector2.down * this.speed);
            } else if (this.transform.position.y < 0.0f)
            {
                rb.AddForce(Vector2.up * this.speed);
            }

        }

        





    }
}
