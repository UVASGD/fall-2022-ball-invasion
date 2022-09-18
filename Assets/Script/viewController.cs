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
        // use WASD to control the movement of camera
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // use mouse to control the scale of the view
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h, mouse * mouseSpeedAmplify, v) * Time.deltaTime * speed, Space.World);
    }
}
