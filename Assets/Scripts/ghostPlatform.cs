using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostPlatform : MonoBehaviour
{

    public float despawnDelay = 3f;
    float despawnTimer = 0f;
    bool ghostOffPlatform = false;
    public bool platformTimerResests = true;
    public GameObject ghost;


    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost");
    }

    // Update is called once per frame
    void Update()
    {
        Despawn();
    }

    public void Despawn()
    {
        if (ghostOffPlatform)
        {
            despawnTimer += Time.deltaTime;
        }

        if(despawnTimer >= despawnDelay)
        {
            if(gameObject != null)
            {
                ghost.GetComponent<Player_Controller>().ghostPlatformExists = false;
                Destroy(gameObject);
            }  
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            //Debug.Log("Ghost on Platform");
            ghostOffPlatform = false;
            if(platformTimerResests)
                despawnTimer = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            //Debug.Log("Ghost off Platform");
            ghostOffPlatform = true;
        }
    }
}
