using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float sneakSpeed;
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 moveDir;

    private bool isDashButtonDown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            if(movement.x > 0)
            {
                animator.SetBool("WalkingRight", true);
                animator.SetBool("WalkingLeft", true);
            }
            else if(movement.x < 0)
            {
                animator.SetBool("WalkingRight", false);
                animator.SetBool("WalkingLeft", true);
            }
        }
        else
        {
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
        }

        moveDir = new Vector3(movement.x, movement.y).normalized;

    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            rb.velocity = moveDir * sneakSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = moveDir * speed * Time.fixedDeltaTime;
        }
    }
}
