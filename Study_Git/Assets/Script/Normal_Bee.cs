using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Bee : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public Animator anim;
    public int maxHealth;
    int currentHealth;
    public bool isDead = false;

    //[SerializeField] private LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }
}
