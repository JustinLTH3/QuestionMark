using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "enemy")
        { 
            movement.enabled = false;
            Spawner.instance.EndGame();
        }
    }

}
