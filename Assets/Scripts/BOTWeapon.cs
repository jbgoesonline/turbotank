using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTWeapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;
    Vector3 MousePos;
    

	//timer details
	public float waitTime = 2.0f;
	float timer = 0.0f;
	

    // Update is called once per frame
    void Update() {
    	//update timer
    	timer += Time.deltaTime;
    	
        
        //recycles timer
        if (timer > waitTime) {
        	timer = timer - waitTime;
        	//call aim and shoot script
        	aimShoot();
        }
    }
    
    void aimShoot () {
    	MousePos = aim();
        Vector2 lookDir = MousePos-transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90 ;
        transform.rotation =  Quaternion.Euler (0f, 0f, angle);
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < 5) {
        	Shoot();
        }
    }
    
    //shoot function
    void Shoot () {
    	//shooting logic
 
    	Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
    	
    }
    
    Vector3 aim () {
    	return GameObject.Find("Player Tank").transform.position;;
    }
    
}

