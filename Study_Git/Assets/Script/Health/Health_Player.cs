using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private bool invincible = false;
    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            //ifram
        }
        else
        {
            if (!dead) 
            { 
                anim.SetTrigger("Die");
                GetComponent<Player_Movement>().enabled = false;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                dead = true;
            }
        }
    }

    void resetInvulnerability()
    {
        invincible = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!invincible)
        { 
            if (collision.collider.CompareTag("Enemy") == true)
            {
                invincible = true;
                TakeDamage(collision.collider.GetComponent<Enemy>().damage);
                Invoke("resetInvulnerability", 2);
            }
        }
    }

    

}
