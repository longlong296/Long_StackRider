using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;        //Public variable to store a reference to the player game object

    private Vector3 offset;
    [Range(1, 10)] public int smoothMotion;      //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 targetPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothMotion * Time.deltaTime);
        transform.position = smoothPos;
    }
}
