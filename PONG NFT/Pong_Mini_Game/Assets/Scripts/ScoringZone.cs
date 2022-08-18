using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//events systems allow us to distinguish between left and right wall - tags might have issues

public class ScoringZone : MonoBehaviour
{
    //trigger an event when the ball collides
    public EventTrigger.TriggerEvent scoreTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            //object collides with ball
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.scoreTrigger.Invoke(eventData);
            //call functions from Unity when events happen much faster than tags and if statements 
            //Scoring function added in editor

        }
    }

}
