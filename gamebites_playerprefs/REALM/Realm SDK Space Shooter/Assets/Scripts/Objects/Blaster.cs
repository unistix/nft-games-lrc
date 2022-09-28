using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 5.0f;
    public float decayRate = 5.0f; //how quickly the blaster should disappear if it hasn’t hit anything.

    private float timeToDecay = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDecay -= Time.deltaTime;
        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        if (transform.position.x > 8.0f || timeToDecay <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }

}
