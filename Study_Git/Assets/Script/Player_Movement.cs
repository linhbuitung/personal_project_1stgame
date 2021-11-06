using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]    private float speed;
    [SerializeField]    private float jumpForce;
    [SerializeField]    private LayerMask groundLayer;
    [SerializeField]    private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private CircleCollider2D circleCollider;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleCollider = GetComponent < CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {       
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        anim.SetBool("is_Running", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        //wall jump logic
         if(wallJumpCooldown > 0.2f)
         {
             body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

             if(onWall() && !isGrounded())
             {
                 body.gravityScale = 0;
                 body.velocity = Vector2.zero;
             } else
             {
                 body.gravityScale = 3;
             }

             if (Input.GetKey(KeyCode.Space))
             {
                 jump();
             } 
         }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

     private void jump()
     {

         if (isGrounded())
         {
             body.velocity = new Vector2(body.velocity.x, jumpForce);
             anim.SetTrigger("jump");
         } else if (onWall() && !isGrounded())
         {
             if(horizontalInput == 0)
             {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, -1);
                 transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
             }
             else
             {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, jumpForce/2);
             }
             wallJumpCooldown = 0;

             //Mathf.Sign(x) return 1 when x > 0 and return -1 if x <0;

         }
     }
       

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
