using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float climbSpeed = 10f;
    Animator characterAnimator;
    Vector2 moveInput;
    Rigidbody2D theRb2d;
    CapsuleCollider2D playerCapsuleCollider2D;
    float gravityScaleAtStart;
    void Start()
    {
        GetRigidBody();
        GetAnimator();
        GetCapsuleCollider2d();
        GetGravityScaleAtStart();
    }

    private void GetGravityScaleAtStart()
    {
        gravityScaleAtStart = theRb2d.gravityScale;
    }

    private void GetCapsuleCollider2d()
    {
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void GetAnimator()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void GetRigidBody()
    {
        theRb2d = GetComponent<Rigidbody2D>();
    }
    void OnJump(InputValue value)
    {
        if (!playerCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            theRb2d.velocity += new Vector2(0, jumpPower);
        }
    }
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    private void ClimbLadder()
    {
        if (!playerCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            theRb2d.gravityScale = 4;
            return;
        }
        Vector2 climbVelocity = new Vector2(theRb2d.velocity.x, moveInput.y * climbSpeed);
        theRb2d.velocity = climbVelocity;
        theRb2d.gravityScale = 0;
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(theRb2d.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(theRb2d.velocity.x), 1f);
    }
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, theRb2d.velocity.y);
        theRb2d.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(theRb2d.velocity.x) > Mathf.Epsilon;
        characterAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
