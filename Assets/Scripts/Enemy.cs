using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{ 
    private Animator animator;
    public PlayerHealth playerHealth;
    private Rigidbody2D rb;
    public float health = 10f;
    public float damage = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public float Health
    {
        set
        {
            print(value);
            health = value;
            
            if(health <= 1)
            {
                dying();
            }
        }
        get
        {
            return health;  
        }
    }

    private void dying()
    {
        animator.SetBool("Dead", true);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage)
    {
        Debug.Log("Slime hit for " + damage);
        Health -= damage;
        animator.SetTrigger("Damage");
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
        animator.SetTrigger("Damage");
        Debug.Log("Slime hit for " + damage);
        Debug.Log("Force: " + knockback);
    }


    // ADD DAMAGE TO PLAYER 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.takeDamage(damage);
            Debug.Log("Damge taken: " + damage);
        }
    }


}