using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewController : MonoBehaviour
{
    public float speed = 10;
    public float mouseSpeedAmplify = 10;
    // Update is called once per frame
    void Update()
    {
        float h, v, mouse;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        mouse = Input.GetAxis("Mouse ScrollWheel");
        // use WASD to control the movement of camera
        if (transform.position.x > 100)
        {
            if(h > 0)
            {
                h = 0;
            }
        }

        if (transform.position.x < -60)
        {
            if (h < 0)
            {
                h = 0;
            }
        }

        if (transform.position.z > 0)
        {
            if(v > 0)
            {
                v = 0;
            }
        }

        if (transform.position.z < -150)
        {
            if (v < 0)
            {
                v = 0;
            }
        }


        // use mouse to control the scale of the view
        if (transform.position.y < 20)
        {
            if(mouse < 0)
            {
                mouse = 0;
            }
        }

        if (transform.position.y > 80)
        {
            if (mouse > 0)
            {
                mouse = 0;
            }
        }
        transform.Translate(new Vector3(h, mouse * mouseSpeedAmplify, v) * Time.deltaTime * speed, Space.World);
    }
}
