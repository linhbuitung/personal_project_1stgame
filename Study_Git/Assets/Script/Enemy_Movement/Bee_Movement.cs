using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Movement : MonoBehaviour
{
    [SerializeField] private float flyForce;
    [SerializeField] private float detectRange;
    [SerializeField] private float timer;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D myBody;
    public Rigidbody2D player;
  
    Vector3 playerPosRn;
    private float realmoveSpeed;
    //Enemy enemyTracker;


    // Start is called before the first frame update
    void Start()
    {  
        myBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        //enemyTracker = myBody.GetComponent<Enemy>();

    }

    private void Update()
    {
        if (player.position.x > myBody.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        Wait();
        Chase();

        //if (enemyTracker.isDead == true)
        //{
        //   StopCoroutine("Fly");
        //}
    }
    // Update is called once per frame
    void Chase()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, playerPosRn);
     
        
            if (hit.collider.CompareTag("Ground"))
            {
                return;
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                return;
            }
        */
        if (Vector2.Distance(transform.position, playerPosRn) < detectRange ) { 
               transform.position = Vector2.MoveTowards(myBody.position, playerPosRn, flyForce * Time.deltaTime);
        }
    }
    
    void Wait()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            playerPosRn = player.position;
            timer = 4f;
        }
    }

}
