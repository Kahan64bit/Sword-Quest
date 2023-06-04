using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float Health
    {
        set
        {
            print(value);
            health = value;
            if(health <= 0)
            {
                defeated();
            }
        }
        get
        {
            return health;  
        }
    }

    public float health = 1f;


    public void defeated()
    {
        Destroy(gameObject);
    }
}
