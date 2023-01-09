using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject snowball;
    public Transform playerTransform;

    GameObject objGame;   
    bool canLaunch;

    void Start()
    {
        canLaunch = true;
        playerTransform = GetComponent <Transform>();
    }

    void Update()
    {
        if (canLaunch)
        {
            Vector3 player = (playerTransform.transform.position - transform.position).normalized;
            Vector3 ball = new Vector3(-0.75f,0,0);

            objGame = (GameObject)Instantiate(snowball, transform.position + ball, transform.rotation);
            
            Rigidbody2D snow = objGame.GetComponent<Rigidbody2D>();
            snow.velocity = new Vector2(1500f, 0);
            StartCoroutine(Throw(objGame));
        }
    }
    
    IEnumerator Throw(GameObject objGame)
    {
        canLaunch = false;
        if (objGame != null)
        {
            yield return new WaitForSeconds(3f);
            Destroy(objGame);
            Debug.Log("enemy");
            yield return new WaitForSeconds(2f);
        }
        canLaunch = true;
    }

    void OnDestroy()
    {
        if (objGame != null) { 
            Destroy(objGame);
        }
    }
}
