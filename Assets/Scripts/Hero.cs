using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] int life = 5;
    [SerializeField] float jumpForce = 15f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    private bool isGrounded;
    [SerializeField] Animator animmator;
    public bool isGrounded;
    public float checkRadius;
    ContactFilter2D s;

    States State
    {
        get { return (States)animmator.GetInteger("State"); }
        set { animmator.SetInteger("State", (int)value); }
    }

    void Update()
    {
        if (isGrounded)
            State = States.idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButton("Jump") && isGrounded)
            Jump();
    }


    public void Run()
    {
        if (isGrounded)
            State = States.run;
        var dir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dir * speed * Time.deltaTime, rb.velocity.y); 
        sprite.flipX = dir < 0.0f;
    }

    void Jump()
    {
        State = States.jump;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        
    }

    void Grounded()
    {
        
       
    }

    void FixedUpdate()
    {
        Grounded();
    }
}
public enum States
{
    idle,
    run,
    jump,
    attack
}
