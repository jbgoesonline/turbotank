﻿using System.Collections;
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
    GameObject[] bullets;
    GameObject bclose;
    //public Camera cam;
    
    //health bar stuff
    public int maxHealth;
    public HealthBar healthBar;
    public int currentHealth = 100;
    public int damageValue = 20;
     Vector3 MousePos;
     int wp = 0;
     List <GameObject> waypoints = new List<GameObject>();

    // Start is called before the first frame update
       void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb = GetComponent<Rigidbody2D> ();
        
        for(int i = 0; i<4; i++){
            if(GameObject.Find("Waypoint"+i))
            {
                //Debug.Log("Found");
                waypoints.Add(GameObject.Find("Waypoint"+i));
            }
            //Debug.Log("Waypoint" + i);
        }
        
        //heatlh bar init
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame

 

    private void Update(){
        //rotate bot towards mouse
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        bool dodge=  false;
        
        if(bullets.Length!=0){
            for(int i = 0 ;i<bullets.Length;i++){
                if((bullets[i].transform.position-transform.position).magnitude <10){
                    dodge = true;
                    bclose = bullets[i];
                    break;
                }
            }
        }
        if(dodge){
            
            Vector3 bvec = new Vector3(bclose.transform.position.x+rb.velocity.x,bclose.transform.position.y+rb.velocity.y,0);
            float x1 = bvec.x*(float)(Math.Cos(90*(Math.PI/180)))-bvec.y*(float)(Math.Sin(90*(Math.PI/180)));
            float y1 = bvec.x*(float)(Math.Sin(90*(Math.PI/180)))+bvec.y*(float)(Math.Cos(90*(Math.PI/180)));
            Vector3 bvecp = new Vector3( x1,y1,0);
            MousePos = bvecp;
            Vector2 lookDir = MousePos-transform.position;
            if(lookDir.magnitude>1){
                float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
                Quaternion angle_q =  Quaternion.Euler (0f, 0f, angle);
                Quaternion init = transform.rotation;
                //while()
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle_q, Time.deltaTime * 100);
                //move forwards
                float xv = (float) Math.Cos(rb.rotation*(Math.PI/180));
                float yv = (float) Math.Sin(rb.rotation*(Math.PI/180));
                Vector2 movement = new Vector2(xv*3,yv*3);

                rb.velocity = movement;
                }
            
        }else{
            if(waypoints.Count == 0){
                MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }else{
        
                MousePos = waypoints[wp].transform.position;
            }
            Vector2 lookDir = MousePos-transform.position;
            if(lookDir.magnitude>1){
                float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg;
                Quaternion angle_q =  Quaternion.Euler (0f, 0f, angle);
                Quaternion init = transform.rotation;
                //while()
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle_q, Time.deltaTime * 100);
                //move forwards
                float xv = (float) Math.Cos(rb.rotation*(Math.PI/180));
                float yv = (float) Math.Sin(rb.rotation*(Math.PI/180));
                Vector2 movement = new Vector2(xv*3,yv*3);

                rb.velocity = movement;
            }else{
                rb.velocity = new Vector2 (0, 0);
                wp +=1;
                if(wp==waypoints.Count){
                    wp = 0;
                }
            }
        }
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

