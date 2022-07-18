using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bouncing : MonoBehaviour
{
    PhysicsMaterial2D myPhysicsMaterial2D;
    [SerializeField] float jumpSpeedLimit=10f;
    void Start()
    {
        GetPhysics2D();
    }
    private void GetPhysics2D()
    {
        myPhysicsMaterial2D = transform.GetComponent<TilemapCollider2D>().sharedMaterial;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Player")) return;
        Rigidbody2D collisionRB2D=collision.rigidbody;
        if(Mathf.Abs(collisionRB2D.velocity.y)>jumpSpeedLimit)
            collisionRB2D.velocity=new Vector2(collisionRB2D.velocity.x,jumpSpeedLimit);
    }
}
