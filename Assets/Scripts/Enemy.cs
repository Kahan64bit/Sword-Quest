using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    private Animator animator;
    public float health = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

    private void dead()
    {
        Destroy(gameObject);
    }

    private void OnHit(float damage)
    {
        Debug.Log("Slime hit for " + damage);
        Health -= damage;
        animator.SetTrigger("Damage");
    }


    
}
