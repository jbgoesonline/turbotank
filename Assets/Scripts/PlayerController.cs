using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    public float dragNumber;                //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (movement == Vector2.zero) {
        	rb2d.drag = dragNumber;
        }
        
        else {
        rb2d.drag = 0;
        rb2d.AddForce (movement * acceleration);
        }
    }
    void OnCollisionEnter2D(Collision2D collision) {
    	if (collision.gameObject.tag == "Bullet") {
    		Destroy(player);
    	}
    }
}