using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform player;

    private float speed;
    Control con;
    public Vector3 cam;

    int i = 0;

    void Start()
    {
        cam = transform.position - player.transform.position;
        speed = 2.5f;
        con = player.GetComponent <Control>();
    }

	void Update () {
                
        if (Input.GetKey(KeyCode.UpArrow) && con.isOnGround)
        {
            while (i == 0)
            {
                transform.position += (speed * transform.up);
                i = 1;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && con.isOnGround)
        {
            while (i == 0)
            {
                transform.position -= (speed * transform.up);
                i = 1;
            }
        }
        else
        {
            i = 0;
            transform.position = player.position + cam;
        }
    }
}
