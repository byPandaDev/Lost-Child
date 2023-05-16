using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    public float speed = 50f;
    [Header("Sneaking")]
    public float sneakSpeed = 25f;
    public float sneekAnimationSpeed = 0.5f;
    [Header("Sprinting")]
    public float sprintSpeed = 75f;
    public float sprintAnimationSpeed = 1.5f;
    [Header("Endurance")]
    [Tooltip("The Endurance the Player has (Ausdauer)")]
    public float startEndurance = 100f;
    [Tooltip("The Endurance in % the Player lose while Running")]
    public float losingEndurance = 1.0f;
    [Tooltip("The Endurance in % the Player get while Walking")]
    public float healingEndurance = 1.0f;
    [Tooltip("The Endurance the Player has (Ausdauer)")]
    public float currentEndurance = 100f;
    [Tooltip("Losing Time")]
    public float sendRateLosing = 0.1f;
    [Tooltip("Healing Time")]
    public float sendRateHealing = 0.5f;
    [Tooltip("Healing Time Standing")]
    public float sendRateStanding = 0.3f;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;
    private Vector3 moveDir;
    private bool lookedLeft = false;
    private bool isSprinting = false;
    private bool isSneaking = false;

    private float tempTime;

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

        if (currentEndurance < 100 && (!isSprinting || (isSprinting && movement.x == 0 && movement.y == 0)))
        {
            if(movement.x == 0 && movement.y == 0)
            {
                tempTime += Time.deltaTime;
                if (tempTime > sendRateStanding)
                {
                    tempTime -= sendRateStanding;
                    if (currentEndurance < 100) currentEndurance += startEndurance * (healingEndurance / 100);
                }
            } 
            else
            {
                tempTime += Time.deltaTime;
                if (tempTime > sendRateHealing)
                {
                    tempTime -= sendRateHealing;
                    if (currentEndurance < 100) currentEndurance += startEndurance * (healingEndurance / 100);
                }
            }
        }

        if (movement.x != 0 || movement.y != 0)
        {
            
            if(currentEndurance > 0)
            {
                if (isSprinting)
                {
                    animator.speed = sprintAnimationSpeed;
                    tempTime += Time.deltaTime;
                    if (tempTime > sendRateLosing)
                    {
                        tempTime -= sendRateLosing;
                        currentEndurance -= startEndurance * (losingEndurance / 100);
                    }
                }
                else animator.speed = 1.0f;
            }
            if(movement.x != 0)
            {
                if (isSneaking) animator.Play("Player-Sneak-Sideways");
               /* else if (isSprinting) animator.speed = 1.5f;*//*animator.Play("Player-Sprint-Sideways");*/
                else animator.Play("Player-Walk-Sideways");

                // Go Right
                if (movement.x > 0)
                {
                    sr.flipX = false;
                    lookedLeft = false;
                }
                // Go Left
                else if (movement.x < 0)
                {
                    sr.flipX = true;
                    lookedLeft = true;
                }
            }
            else if(movement.y != 0)
            {
                // Go Up
                if(movement.y > 0)
                {
                    if (isSneaking) animator.Play("Player-Sneak-Up");
                    /*else if (isSprinting) animator.speed = 1.5f;*//*animator.Play("Player-Sprint-Up");*/
                    else animator.Play("Player-Walk-Up");
                } 
                // Go Down
                else if (movement.y < 0)
                {
                    if (isSneaking) animator.Play("Player-Sneak-Down");
                    /*else if (isSprinting) animator.speed = 1.5f;*//*animator.Play("Player-Sprint-Down");*/
                    else animator.Play("Player-Walk-Down");
                }
            }
            else
            {
                animator.Play("Player-Idle");
                if (lookedLeft) sr.flipX = true;
                else sr.flipX = false;
            }
        }
        else
        {
            animator.Play("Player-Idle");
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
        else if (Input.GetKey(KeyCode.LeftShift) && currentEndurance > 0)
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
