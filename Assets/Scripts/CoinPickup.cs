using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsForCoinPickup=100;
    bool wasPickup=false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player"&&!wasPickup)
        {
            wasPickup=true;
            FindObjectOfType<GameSession>().AddScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
