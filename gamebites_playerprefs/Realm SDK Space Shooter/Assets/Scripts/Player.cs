using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float respawnSpeed = 8.0f; //how long it takes for the respawn animation to happen
    public float weaponFireRate = 0.5f; //ow fast you’re allowed to fire blasters

    private float nextBlasterTime = 0.0f;
    private bool isRespawn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRespawn == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-3.0f, -0.25f), respawnSpeed * Time.deltaTime);
            if (transform.position == new Vector3(-3.0f, -0.25f, 0.0f))
            {
                isRespawn = false;
            }
            //dont do anything if respwaning other than reset position 
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 4.0f)
            {
                transform.position += Vector3.up * movementSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -4.0f)
            {
                transform.position += Vector3.down * movementSpeed * Time.deltaTime;
            }


            if (Input.GetKeyDown("space") && Time.time > nextBlasterTime) //shoot blaster and regen bullets
            {
                Debug.Log("shoot");
                nextBlasterTime = Time.time + weaponFireRate;
                GameObject blaster = ObjectPool.SharedInstance.GetPooledBlaster();
                if (blaster != null)
                {
                    blaster.SetActive(true);
                    blaster.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                }
            }
            if (RealmController.Instance.IsCrossBlasterEnabled())
            {
                if (Input.GetKey(KeyCode.B) && Time.time > nextBlasterTime)
                {
                    nextBlasterTime = Time.time + weaponFireRate;
                    GameObject crossBlast = ObjectPool.SharedInstance.GetPooledCrossBlast();
                    if (crossBlast != null)
                    {
                        crossBlast.SetActive(true);
                        crossBlast.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                    }
                }
            }
            if (RealmController.Instance.IsSparkBlasterEnabled())
            {
                if (Input.GetKey(KeyCode.V) && Time.time > nextBlasterTime)
                {
                    nextBlasterTime = Time.time + weaponFireRate;
                    GameObject sparkBlast = ObjectPool.SharedInstance.GetPooledSparkBlast();
                    if (sparkBlast != null)
                    {
                        sparkBlast.SetActive(true);
                        sparkBlast.transform.position = new Vector3(transform.position.x + 1, transform.position.y);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && isRespawn == false)
        {
            RealmController.Instance.ResetScore();
            transform.position = new Vector3(-10.0f, -0.25f, 0.0f);
            isRespawn = true;

        }
    }
}
