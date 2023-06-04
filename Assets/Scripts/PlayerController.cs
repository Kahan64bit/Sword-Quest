using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float collisionoffset = 0.02f;
    public ContactFilter2D contactFilter;
    public SwordAttack swordAttack;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;
    private SpriteRenderer spriterender;

    private List<RaycastHit2D> collisions = new List<RaycastHit2D>();

    bool canMove = true;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frameomni direction 
    private void FixedUpdate()
    {
        if(canMove)
        {
            // Movement 
            if (movementInput != Vector2.zero)
            {
                bool isMoving = TryMove(movementInput);

                if (!isMoving)
                {
                    // Check if movement along X-axis is possible during collision
                    isMoving = TryMove(new Vector2(movementInput.x, 0));

                    if (!isMoving)
                    {
                        // Check if movement along Y-axis is possible during collision if X-axis is false
                        isMoving = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                // Set animation based off direction of vector
                if (movementInput.x > 0.01)
                {
                    animator.SetFloat("Horizontal", movementInput.x);
                    animator.SetFloat("Speed", movementInput.sqrMagnitude);
                }
                if (movementInput.x < 0.01)
                {
                    animator.SetFloat("Horizontal", movementInput.x);
                    animator.SetFloat("Speed", movementInput.sqrMagnitude);
                }
                if (movementInput.y < 0.01)
                {
                    animator.SetFloat("Vertical", movementInput.y);
                    animator.SetFloat("Speed", movementInput.sqrMagnitude);
                }
                if (movementInput.y > 0.01)
                {
                    animator.SetFloat("Vertical", movementInput.y);
                    animator.SetFloat("Speed", movementInput.sqrMagnitude);
                }
            }

            // Set direction to sprite (Left or Right)
            if (movementInput.x < 0)
            {
                spriterender.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriterender.flipX = false;
            }
        }
       
    }

    // Raycast to check for collision
    private bool TryMove(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction, // X and Y vals between -1,1 to determine which direction player intends to move
                contactFilter, // Collision settings that species certain parameters on specific layers 
                collisions, // Raycast collision detection 
                movementSpeed * Time.fixedDeltaTime + collisionoffset); // Cast amount based off movement and offset

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime); // Apply movement to rb
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
        
    }

    // Gets direction of vector
    void OnMove(InputValue movementVal)
    {
        movementInput = movementVal.Get<Vector2>(); // Get movement via Vector 2
    }

    void OnFire()
    {
        animator.SetTrigger("Attack_S");
    }

    public void SwordAttack()
    {
        lockMovement();

        if(spriterender.flipX == true)
        {
            swordAttack.attackLeft();
        }
        else
        {
            swordAttack.attackRight();
        }
    }

    public void endSwordAttack()
    {
        unlockMovement();
        swordAttack.StopAttack();
    }

    public void lockMovement()
    {
        canMove = false;
    }

    public void unlockMovement()
    {
        canMove = true;
    }
}