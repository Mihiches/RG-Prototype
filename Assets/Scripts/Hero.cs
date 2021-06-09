
using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] int life = 5;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    bool isGrounded;
    [SerializeField] Animator animmator;
    [SerializeField] BoxCollider2D legsCollider;
    [SerializeField] int jumpCount = 2;
    int jumpCountValue;
    [SerializeField] int brakeForce = 1;
    void Start()
    {
        jumpCountValue = jumpCount;
    }
    [SerializeField]
    States State
    {
        get { return (States)animmator.GetInteger("State"); }
        set { animmator.SetInteger("State", (int)value); }
    }
    void Update()
    {
        Grounded();
        Fall();
        if (isGrounded && rb.velocity.y ==0)
              State = States.idle;
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
            Jump();
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonUp("Horizontal"))
            StartCoroutine(Brake());
        if (Input.GetButtonDown("Horizontal"))
            StopAllCoroutines();
    }
    public void Run()
    {
        if (isGrounded)
            State = States.run;
        var dir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dir * speed, rb.velocity.y); 
        sprite.flipX = dir < 0.0f;
    }
    void Jump()
    {
        jumpCount--; 
         State = States.jump;
        rb.velocity = new Vector2(rb.velocity.x , jumpForce);
    }
    void Fall()
    {
        if (!isGrounded && rb.velocity.y < 0)
        {
            State = States.fall;
        }
    }
    void Grounded()
    {
        if (legsCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            isGrounded = true;
            jumpCount = jumpCountValue;
        }
        else
            isGrounded = false;
    }
    IEnumerator Brake()
    {
        float moveDirection = Mathf.Sign(rb.velocity.x);
        float currentVelocityX;
        while (rb.velocity.x != 0)
        {
            if (moveDirection > 0 )
                currentVelocityX = Mathf.Max(0, rb.velocity.x - brakeForce * Time.deltaTime);
            else
                currentVelocityX = Mathf.Min(0, rb.velocity.x + brakeForce * Time.deltaTime);
            rb.velocity = new Vector2(currentVelocityX, rb.velocity.y);
            yield return null;
        }
    }
}
public enum States
{
    idle,
    run,
    jump,
    attack,
    fall
}
