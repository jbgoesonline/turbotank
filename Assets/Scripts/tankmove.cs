using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class tankmove : MonoBehaviour
{
    public float angularSpeed;
    public float moveSpeed2;
    public Rigidbody2D rb;
    public GameObject player;
    //public Camera cam;
    
    //health bar stuff
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth = 100;
    public int damageValue = 20;

    float dx;
    float dy;


    // Start is called before the first frame update
       void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb = GetComponent<Rigidbody2D> ();
        
        //heatlh bar init
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame



    private void Update()
    {
        dx = Input.GetAxisRaw("Horizontal");
        dy = Input.GetAxisRaw("Vertical");
        //MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void FixedUpdate(){
        float angleV =-dx*angularSpeed;
        float velCom = dy*moveSpeed2;
        float xv = (float) Math.Cos(rb.rotation*(Math.PI/180));
        float yv = (float) Math.Sin(rb.rotation*(Math.PI/180));
        Vector2 movement = new Vector2(xv*dy*moveSpeed2,yv*dy*moveSpeed2);

        rb.angularVelocity = angleV;
        rb.velocity = movement;

       
        //Debug.Log("Angle: " + rb.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision) {
    	if (collision.gameObject.tag == "Bullet") {
    		takeDamage(damageValue);
    		if (currentHealth <= 0) {
    			Destroy(player);
    		}
    	}
    }
    
    void takeDamage (int damageValue) {
    	currentHealth -= damageValue;
    	healthBar.SetHealth(currentHealth);
    }
}

