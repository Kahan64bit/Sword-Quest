using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 1.5f;
   
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void attackRight()
    {
        print("Attack Right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void attackLeft()
    {
        print("Attack Left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);

    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.SendMessage("OnHit", damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("OnHit", damage);
    }
}
