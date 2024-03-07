using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Dylan Janssen
 * September 19th, 2021 (date of adding this comment block)
 * This file contains code that syncs the camera with the player sprite, allowing the player sprite to stay centered on screen.
 * this code is unchanged from the lecture
 */
public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;


    void Start()
    {
        offset = transform.position - player.transform.position;  // offset vector from initial config
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
