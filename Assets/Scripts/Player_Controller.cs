using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float jumpForce = 18f;
    public float gravityScale = 25f;
    public float jumpTimer = 0.4f;
    public float platformOffset = 1f;
    public float ghostPlatSpawnDelay = 0f;
    public bool ghostPlatformExists = false;
    //public string testString = "default";
    public GameObject ghostPlatform;


    bool isJumping = false;
    bool activateJump = false;
    bool startTimer = false;
    bool earlyRelease = false;
    float timer;

    Vector2 moveInput;

    public bool IsMoving { get; private set; }

    Rigidbody2D playerBody;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();

        timer = jumpTimer;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            //Debug.Log("timing");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Debug.Log("I'm less");
                earlyRelease = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlatform();
        }

    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(moveInput.x * walkSpeed, playerBody.velocity.y);

        if (activateJump)
        {
           playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);

            if (earlyRelease)
            {
                playerBody.gravityScale = gravityScale;
                timer = jumpTimer;
                startTimer = false;
                earlyRelease = false;
                activateJump = false;
            }
        }
           
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            activateJump = true;
            startTimer = true;
            playerBody.gravityScale = 0;
        }

        if (context.canceled || earlyRelease)
        {
            playerBody.gravityScale = gravityScale;
            timer = jumpTimer;
            startTimer = false;
            earlyRelease = false;
            activateJump = false;
        }
    }

    public bool CanSpawnGhostPlatform()
    {
        //Debug.Log("Checking");
        //Debug.Log("Platform Status:" + ghostPlatformExists);
        //Debug.Log("Jumping Status:" + isJumping);

        if (isJumping && !ghostPlatformExists)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, platformOffset);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Platform"))
                    return false;
            }
            return true;
        }
        return false;
    }

    public void SpawnPlatform()
    {
        if (CanSpawnGhostPlatform())
        {
            Invoke("SpawnDelayedPlatform", ghostPlatSpawnDelay);
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.platfromCreation);
            ghostPlatformExists = true;
        }
    }

    private void SpawnDelayedPlatform()
    {
        Vector3 spawnPosition = playerBody.position - new Vector2(0f, platformOffset);
        Instantiate(ghostPlatform, spawnPosition, Quaternion.identity);
    }

    //checking if grounded
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
        {
            isJumping = true;
        }
    }
}
