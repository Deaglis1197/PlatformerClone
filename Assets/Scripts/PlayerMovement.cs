using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed=10f;
    Vector2 moveInput;
    Rigidbody2D theRb2d;
    void Start()
    {
        GetRigidBody();
    }

    private void GetRigidBody()
    {
        theRb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }
    private void Run()
    {
        Vector2 playerVelocity=new Vector2(moveInput.x*runSpeed,theRb2d.velocity.y);
        theRb2d.velocity=playerVelocity;
    }

    void OnMove(InputValue value)
    {  
        moveInput=value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
