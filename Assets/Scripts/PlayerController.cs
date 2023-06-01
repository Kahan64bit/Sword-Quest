using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float collisionoffset = 0.05f;
    public ContactFilter2D contactFilter;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private List<RaycastHit2D> collisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementInput != Vector2.zero)
        {
            int count = rb.Cast(
                movementInput,
                contactFilter,
                collisions,
                movementSpeed * Time.fixedDeltaTime + collisionoffset);

            if(count == 0)
            {
                rb.MovePosition(rb.position + movementInput * movementSpeed * Time.fixedDeltaTime);

            }
        }
    }

    void OnMove(InputValue movementVal)
    {
        movementInput = movementVal.Get<Vector2>();
    }
}
