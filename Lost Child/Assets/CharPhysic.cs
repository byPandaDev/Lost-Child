using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharPhysics : MonoBehaviour
{


    public bool isFacingRight;

    public float speed = 1f;
    public float accelRate = 2f;
    public float TopSpeed = 5f;

    public Animator animator;

    private Vector2 _movementInput;


    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {


        if (_movementInput.x != 0)
        {
            CheckDirectionToFace(rb2d.velocity.x > 0);
        }


    }

    void FixedUpdate()
    {
        movement();
    }

    private void movement()
    {
        _movementInput.x = Input.GetAxis("Horizontal");
        _movementInput.y = Input.GetAxis("Vertical");

        float speedDif = (speed * 2f) - (rb2d.velocity.x + rb2d.velocity.y);
        float movement = speedDif * accelRate;

        if (movement >= TopSpeed )
        {
            movement = TopSpeed;
        }

        Vector2 walking = new Vector2(_movementInput.x, _movementInput.y);
        rb2d.velocity = movement * walking;
    }


    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        isFacingRight = !isFacingRight;
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != isFacingRight)
            Flip();
    }



}
