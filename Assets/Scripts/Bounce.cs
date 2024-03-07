using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Dylan Janssen
 * September 19th, 2021 (date of adding this comment block)
 * This file contains code that allows the asteroids (PickUps) to bounce off (reflect their vector) objects they collide with.
 */
public class Bounce : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 preVelocity;


    //set rigidbody2d variable, awake function so that it is called first, separated initialization into two steps.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //get the velocity every update before collision
    void Update()
    {
        preVelocity = rb.velocity;
    }


    //transfer the velocity and change the direction based on the trajectory
    private void OnCollisionEnter2D(Collision2D col)
    {
        //get the previous speed before collision
        var speed = preVelocity.magnitude;

        //col.GetContact(0) may be better for memory garbage, but since this is a small game I felt it was okay to use this:
        var contacts = col.contacts[0].normal;
        
        //calculate the direction in which the ball will be redirected
        var direction = Vector2.Reflect(preVelocity.normalized, contacts);
        rb.velocity = direction * Mathf.Max(speed, 5f); 
    }
}
