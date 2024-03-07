using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Author: Dylan Janssen
 * September 19th, 2021 (date of adding this comment block)
 * This file contains basic controls for the player, the timer, collision handlers, and restart button display.
 */
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Transform tran;
    public float speed;
    public Text countText;
    public Text winText;
    public Button restartButton;
    public int movementMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        //instantiating variables on startup
        rb2d = GetComponent<Rigidbody2D>();
        tran = GetComponent<Transform>();
        countText.text = "Timer: " + timer.ToString();
        winText.text = "";
        restartButton.gameObject.SetActive(false); // hide button
        movementMultiplier = 1;

    }

    // timer for the game
    float timer = 0.0f;
    void Update()
    {
        int seconds = 0;
        // timer given by instructor, will count to 60 then restart back at 0
        // will NOT count if you lose the game by hitting an asteroid.
        // LINE 40: makes the timer go down instead of up. (60-0, 60-1, etc)
        if (winText.text != "You Lose!" && winText.text != "You Win!")
        {
            timer += Time.deltaTime;
            seconds = 60 - (int)timer % 61;
            countText.text = "Timer: " + seconds.ToString();
        }

        // if the seconds is 0, display win text and give the player the option to restart

        if (seconds == 0 && winText.text != "You Lose!")
        {
            winText.text = "You Win!";
            restartButton.gameObject.SetActive(true); //show button
        }

       
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed * movementMultiplier; //movementMultiplier helps for inverted movement (1 = normal, -1 = inverted)
    }

    /*
     * asteroids:
     * Yellow: game-over
     * White: Inverted Movement
     * Black: Increase Size
     * Light Blue/Turquoise: Slowed movement 
    */

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if you hit a pickup object (asteroid), and have not already won the game, will display loss text and show restart button
        if (col.gameObject.CompareTag("PickUp") && winText.text != "You Win!")
        {
            winText.text = "You Lose!";
            restartButton.gameObject.SetActive(true);
        }

        //if you hit a blue ice chunk, your UFO takes damage and slows down PERMANENTLY! (you will be at 70% of base speed)
        if (col.gameObject.CompareTag("PickUp2"))
        {
            movementMultiplier = 1;
            speed = 7;
            col.gameObject.SetActive(false);
        }

        // if you collide with this black asteroid, you will increase in size slightly 
        // this does not impact the game much, but makes it slightly harder to manuever around the asteroids! 
        if (col.gameObject.CompareTag("PickUp3"))
        {
            movementMultiplier = 1;
            tran.localScale += new Vector3(0.1f, 0.1f, 0);
            col.gameObject.SetActive(false);
        }

        // if you hit this game object, your controls will be inverted!
        // fear not, for if you hit another special object your movement will return to normal!

        // movement multiplier makes velocity negative, which will send you in the opposite direction you are wanting to go, so be careful!
        if (col.gameObject.CompareTag("PickUp4"))
        {
            movementMultiplier = -1;
            col.gameObject.SetActive(false);
        }
    }
    

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); //restart the game from sample scene
    }


}
