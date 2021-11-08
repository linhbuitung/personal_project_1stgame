using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;
    public Animator anim;
    public int maxHealth = 100;
    int currentHealth;
    public bool isDead = false;
    
    //[SerializeField] private LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);
        this.enabled = false;
        Destroy(gameObject, 2);
    }


}
