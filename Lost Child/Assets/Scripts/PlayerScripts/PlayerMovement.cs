using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    public float speed;
    [Header("Sneaking")]
    public float sneakSpeed;
    public float sneekAnimationSpeed;
    [Header("Sprinting")]
    public float sprintSpeed;
    public float sprintAnimationSpeed;

    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;
    private Vector3 moveDir;
    private bool lookedLeft = false;
    private bool isSprinting = false;
    private bool isSneaking = false;

    public Animator timeReflectionAnimator;
    public AnimatorControllerParameter[] parameters;
    [SerializeField] private Rigidbody2D rb2d;


    private void Awake()
    {
        // Get Components from Player
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        foreach (AnimatorControllerParameter parameter in timeReflectionAnimator.parameters)
        {
            timeReflectionAnimator.SetBool(parameter.name, false);
        }

        if (movement.x != 0 || movement.y != 0)
        {
            if(movement.x != 0)
            {
                if (isSneaking) animator.SetBool("SneakingSideways", true);
                else if (isSprinting) animator.SetBool("SprintingSideways", true);
                else animator.SetBool("WalkingSideways", true);

                if (movement.x > 0)
                {
                    sr.flipX = false;
                    lookedLeft = false;
                }
                else if (movement.x < 0)
                {
                    sr.flipX = true;
                    lookedLeft = true;
                }
                else
                {
                    animator.SetBool("SneakingSideways", false);
                    animator.SetBool("SprintingSideways", false);
                    animator.SetBool("WalkingSideways", false);
                }
            }
        }
        else
        {
            animator.SetBool("WalkingSideways", false);
            if (lookedLeft) sr.flipX = true;
            else sr.flipX = false;
        }

        moveDir = new Vector3(movement.x, movement.y).normalized;

    }

    private void FixedUpdate()
    {
        // Sneaking
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.velocity = moveDir * sneakSpeed * Time.fixedDeltaTime;
            isSneaking = true;
            isSprinting = false;

        }
        // Sprinting
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = moveDir * sprintSpeed * Time.fixedDeltaTime;
            isSneaking = false;
            isSprinting = true;
        }
        // Walking
        else
        {
            rb.velocity = moveDir * speed * Time.fixedDeltaTime;
            isSneaking = false;
            isSprinting = false;
        }
    }
}
