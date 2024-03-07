using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Dylan Janssen
 * September 19th, 2021 (date of adding this comment block)
 * This file contains movement controls for the asteroids/PickUps
 */
public class Rotator : MonoBehaviour
{ 
    private Rigidbody2D rb;
    public float speed;
    public Vector2 movement;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //chooses a random direction on the 2d plane
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //declare initial velocity
        rb.velocity = speed * movement;
    }

    void FixedUpdate()
    {
        
    } 
}
