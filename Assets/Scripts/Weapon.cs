using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;

    // Update is called once per frame
    void Update() {
        //if the fire button is pressed
        if (Input.GetButtonDown("Fire1")) {
        	if (GameObject.FindGameObjectsWithTag("Bullet").Length < 5) {
        		Shoot();
        	}
        }
    }
    
    //shoot function
    void Shoot () {
    	//shooting logic
    	Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    	
    }
    
}
