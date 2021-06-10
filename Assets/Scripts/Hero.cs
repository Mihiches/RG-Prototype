
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public static Hero Instanse { get; set; }

    [SerializeField] float speed = 3f;
    public int herolives = 5;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite;
    bool isGrounded;
    [SerializeField] Animator animmator;
    [SerializeField] Collider2D legCollider;
    
    [SerializeField] int jumpCount = 2;
    int jumpCountValue;
    [SerializeField] int brakeForce = 1;
    float dir;
    float moveDirection;
    float currentVelocityX;
    void Start()
    {
        jumpCountValue = jumpCount;
        Instanse = this;
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
        if (Input.GetButton("Horizontal") && Input.GetButton("Jump") && jumpCount > 0)
            State = States.jump;
        if (isGrounded && rb.velocity.y ==0)
              State = States.idle;
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
            Jump();
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonUp("Horizontal"))
            StartCoroutine(Brake());
        if (Input.GetButtonDown("Horizontal"))
            StopCoroutine(Brake());
    }
    public void Run()
    {
        if (isGrounded)
            State = States.run;
        dir = Input.GetAxis("Horizontal");
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
        if (!isGrounded && rb.velocity.y < -0.1) State = States.fall;
    }
    void Grounded()
    {
        if (legCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            isGrounded = true;
            jumpCount = jumpCountValue;
        }
        else
            isGrounded = false;
    }
    IEnumerator Brake()
    {
        moveDirection = Mathf.Sign(rb.velocity.x);
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
  
    public void GetDamage(int damage)
    {
        rb.AddForce(new Vector2(brakeForce * dir * -1, rb.velocity.y));
        State = States.hit;

        herolives -= damage;
        Debug.Log(herolives);
        if (herolives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
public enum States
{
    idle,
    run,
    jump,
    attack,
    fall,
    hit
}
