using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character_Switch : MonoBehaviour
{
    public GameObject otherCharacter;
    public GameObject thisCharacter;

    public CinemachineVirtualCamera cinemachine;
    private Transform activeCharacter;

    private void Awake()
    {
        if (thisCharacter.CompareTag("WoodenMan"))
        {
            thisCharacter.GetComponent<Player_Controller>().enabled = true;
            otherCharacter.GetComponent<Player_Controller>().enabled = false;
            activeCharacter = thisCharacter.transform;
            cinemachine.LookAt = activeCharacter;
            cinemachine.Follow = activeCharacter;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeCharacter != null)
            {
                if (activeCharacter.CompareTag("WoodenMan"))
                {
                    if (thisCharacter.CompareTag("Ghost"))
                    {
                        thisCharacter.GetComponent<Player_Controller>().enabled = true;
                        otherCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = thisCharacter.transform;
                    }
                    else
                    {
                        otherCharacter.GetComponent<Player_Controller>().enabled = true;
                        thisCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = otherCharacter.transform;
                    }
                }
                else if (activeCharacter.CompareTag("Ghost"))
                {
                    if (thisCharacter.CompareTag("WoodenMan"))
                    {
                        thisCharacter.GetComponent<Player_Controller>().enabled = true;
                        otherCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = thisCharacter.transform;
                    }
                    else
                    {
                        otherCharacter.GetComponent<Player_Controller>().enabled = true;
                        thisCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = otherCharacter.transform;
                    }
                }

                cinemachine.LookAt = activeCharacter;
                cinemachine.Follow = activeCharacter;
            }
        }
    }
}
