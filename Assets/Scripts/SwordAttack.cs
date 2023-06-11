using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 1.5f;
    public float knockBackForce = 500f;
   
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
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2)(collision.gameObject.transform.position - parentPosition ).normalized;
            Vector2 knockback = direction * knockBackForce;

            // collision.SendMessage("OnHit", damage, knockback);
            damageable.OnHit(damage, knockback);
        }
        else
        {
            Debug.Log("Collider does not implement IDamageable");
        }
       
    }
}
