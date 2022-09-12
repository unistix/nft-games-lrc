using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBlast : Blaster
{
    // No decay time but only destroy one enemy
    void Update()
    {
        
        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        if (transform.position.x > 8.0f )
        {
            gameObject.SetActive(false);
        }
    }
}
