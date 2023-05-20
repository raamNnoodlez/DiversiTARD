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

    bool isJumping = false;
    bool activateJump = false;
    bool startTimer = false;
    bool earlyRelease = false;
    float timer;

    Vector2 moveInput;

    public bool IsMoving { get; private set; }

    Rigidbody2D playerBody;
    public Animator animator;

    private bool facingLeft = true; 

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
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                earlyRelease = true;
            }
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(moveInput.x * walkSpeed, playerBody.velocity.y);

        if(CompareTag("Ghost") || CompareTag("WoodenMan"))
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        }
        

        if(moveInput.x > 0 && facingLeft)
        {
            Flip();
        }

        if(moveInput.x < 0 && !facingLeft)
        {
            Flip();
        }

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


    //checking if grounded
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;

            if (CompareTag("Ghost") || CompareTag("WoodenMan"))
                animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isJumping = true;

            if (CompareTag("Ghost") || CompareTag("WoodenMan"))
                animator.SetBool("isJumping", true);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }
}
