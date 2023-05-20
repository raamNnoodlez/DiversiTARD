using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wooden_Man_Attack : MonoBehaviour
{
    public Animator animator;
    public float timeToAttack = 0.25f;

    float timer = 0;
    bool isAttacking = false;
    GameObject attackArea = default;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        animator.SetBool("isAttacking", true);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Attack();
    }
}
