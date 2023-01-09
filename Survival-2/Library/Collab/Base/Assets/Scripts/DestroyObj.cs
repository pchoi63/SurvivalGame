using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public GameObject other;
    //I have removed the "public gameobject other" since I'm assuming that this script is 
    //attached to the player.
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collider hits a gameobject with tag "Laser"
        //then the gameobject the script is attached to will be destroyed

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
           
    }
}