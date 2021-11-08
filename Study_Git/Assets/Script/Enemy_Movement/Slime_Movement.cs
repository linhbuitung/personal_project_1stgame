using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Movement : MonoBehaviour
{
    
    [SerializeField] private float jumpForce;
    [SerializeField] private int timeBetween;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D myBody;
    public Rigidbody2D player;
    private float realmoveSpeed;
    Enemy enemyTracker;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Jump");
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyTracker = myBody.GetComponent<Enemy>();
    }

    private void Update()
    {
        anim.SetBool("Grounded", isGrounded());
        if (player.position.x < myBody.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
        if (enemyTracker.isDead == true) {
            StopCoroutine("Jump");
        }
    }
    // Update is called once per frame
    private IEnumerator Jump()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(timeBetween);
            if (isGrounded())
            {
                anim.SetTrigger("jump");
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
                chase();
            }

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    void chase()
    {
        if(player.position.x > myBody.position.x)
        {
            realmoveSpeed = 10*moveSpeed;
        } else
        {
            realmoveSpeed = -10*moveSpeed;
        }
        myBody.AddForce(new Vector2(realmoveSpeed, 0));
    }
}
