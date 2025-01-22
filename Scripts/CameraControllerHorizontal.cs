using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerHorizontal : MonoBehaviour
{
    // The object (e.g., the player) around which the camera will rotate.
    public Transform target;

    // Speed of the rotation.
    public float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        // You can set a default target if it's not set in the Inspector
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;  // Change "Player" with your target tag if needed
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera around the target at the specified speed
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Make the camera look at the target while rotating
        transform.LookAt(target);
    }
}
