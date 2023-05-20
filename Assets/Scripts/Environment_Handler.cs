using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Handler : MonoBehaviour
{
    public GameObject ghostFloor;
    public Sprite ghostBackground;
    public GameObject fatherFloor;
    public Sprite fatherBackground;
    bool isFatherEnvironment = true;

    public float ghostFloorX = 4;
    public float ghostFloorY = -11;
    public float fatherFloorX = 4;
    public float fatherFloorY = -8;

    private GameObject currentFatherFloor;
    private GameObject currentGhostFloor;

    public static Environment_Handler evironmentHandlerInstance;

    void Awake()
    {
        if (evironmentHandlerInstance != null && evironmentHandlerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        evironmentHandlerInstance = this;
        DontDestroyOnLoad(this);
    }

    public void spawnGhostEnvironment()
    {
        if (isFatherEnvironment)
        {
            Destroy(currentFatherFloor);
            currentFatherFloor = null;
        }

        if (currentGhostFloor == null)
        {
            Vector3 spawnPosition = new Vector3(ghostFloorX, ghostFloorY, 0f);
            currentGhostFloor = Instantiate(ghostFloor, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned ghost floor");
        }

        isFatherEnvironment = false;
    }

    public void spawnFatherEnvironment()
    {
        if (!isFatherEnvironment)
        {
            Destroy(currentGhostFloor);
            currentGhostFloor = null;
        }

        if (currentFatherFloor == null)
        {
            Vector3 spawnPosition = new Vector3(fatherFloorX, fatherFloorY, 0f);
            currentFatherFloor = Instantiate(fatherFloor, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned father floor");
        }

        isFatherEnvironment = true;
    }
}
