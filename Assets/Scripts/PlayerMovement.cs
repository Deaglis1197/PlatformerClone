using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Animator characterAnimator;
    Animation climbingAnimation;
    Vector2 moveInput;
    Rigidbody2D theRb2d;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float gravityScaleAtStart;
    bool isAlive = true;
    void Start()
    {
        GetRigidBody();
        GetAnimator();
        GetGravityScaleAtStart();
        GetColliderForFeet();
        GetColliderForBody();
    }

    private void GetColliderForBody()
    {
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void GetColliderForFeet()
    {
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    private void GetGravityScaleAtStart()
    {
        gravityScaleAtStart = theRb2d.gravityScale;
    }

    private void GetAnimator()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void GetRigidBody()
    {
        theRb2d = GetComponent<Rigidbody2D>();
    }
    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation).GetComponent<Bullet>().playerLocalScaleX = transform.localScale.x;
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            theRb2d.velocity += new Vector2(0, jumpPower);
        }
    }
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    private void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            theRb2d.gravityScale = 4;
            characterAnimator.SetBool("isClimbing", false);
            characterAnimator.speed = 1;
            return;
        }
        Vector2 climbVelocity = new Vector2(theRb2d.velocity.x, moveInput.y * climbSpeed);
        theRb2d.velocity = climbVelocity;
        theRb2d.gravityScale = 0;
        //this part is different from course
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            characterAnimator.SetBool("isClimbing", true);
            characterAnimator.speed = Mathf.Abs(moveInput.y);
        }
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
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard", "Water")))
        {
            isAlive = false;
            characterAnimator.SetTrigger("Dying");
            if (!myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Water")))
                theRb2d.velocity = deathKick;
            else
            {
                theRb2d.gravityScale=0.25f;
                theRb2d.velocity = new Vector2(Mathf.Sign(theRb2d.velocity.x),1);
            }
        }
    }
}
