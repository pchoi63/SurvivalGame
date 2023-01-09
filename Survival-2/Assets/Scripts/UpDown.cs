using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{

    float vel = 3.0f;
    float upLimit = 8.5f;
    float downLimit = 8.5f;
    float direction = 1.0f;

    void Start()
    {
        upLimit = transform.position.y - 8.5f;
        downLimit = transform.position.y + 8.5f;
    }


    void Update()
    {
        if (transform.position.y >= downLimit)
        {
            direction = -1.0f;
        }
        else if (transform.position.y <= upLimit)
        {
            direction = 1.0f;
        }
        GetComponent<Rigidbody2D>().MovePosition(transform.position + (vel * direction * transform.up * Time.deltaTime));
    }
}
