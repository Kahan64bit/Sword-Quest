using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float collisionoffset = 0.02f;
    public ContactFilter2D contactFilter;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private List<RaycastHit2D> collisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool isMoving = TryMove(movementInput);

            if(!isMoving)
            {
                // Check if movement along X-axis is possible during collision
                isMoving = TryMove(new Vector2(movementInput.x, 0));

                if (!isMoving)
                {
                    // Check if movement along Y-axis is possible during collision if X-axis is false
                    isMoving = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
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

    void OnMove(InputValue movementVal)
    {
        movementInput = movementVal.Get<Vector2>(); // Get movement via Vector 2
    }
}