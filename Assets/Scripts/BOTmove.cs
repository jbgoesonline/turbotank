using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BOTmove : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeed2;
    public Rigidbody2D rb;
    public GameObject BOT;
    public GameObject PlayerTank;
    //public Camera cam;
    
    //health bar stuff
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth = 100;
    public int damageValue = 20;
     Vector3 MousePos;

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

    IEnumerator Rotate(){
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = MousePos-transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
        Quaternion angle_q =  Quaternion.Euler (0f, 0f, angle);
        Quaternion init = transform.rotation;
        //while()
        transform.rotation = Quaternion.Lerp(transform.rotation, angle_q, Time.deltaTime * 1);
        yield return null;
    }

    private void Update(){

        //StartCoroutine("Rotate");
    
    }

    private void FixedUpdate(){
        
    }
    

    void OnCollisionEnter2D(Collision2D collision) {
    	if (collision.gameObject.tag == "Bullet") {
    		takeDamage(damageValue);
    		if (currentHealth <= 0) {
    			Destroy(BOT);
    		}
    	}
    }
    
    void takeDamage (int damageValue) {
    	currentHealth -= damageValue;
    	healthBar.SetHealth(currentHealth);
    }
}

