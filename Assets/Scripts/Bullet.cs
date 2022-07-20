using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 20f;
    public float playerLocalScaleX;
    void Start()
    {
        GetRigidBody2D();
    }

    private void GetRigidBody2D()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        myRigidbody.velocity = new Vector2(bulletSpeed * playerLocalScaleX, 0f);
    }
}
