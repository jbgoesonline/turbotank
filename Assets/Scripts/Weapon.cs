using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;
    Vector3 MousePos;

    // Update is called once per frame
    void Update() {
        //if the fire button is pressed
         MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = MousePos-transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90 ;
        transform.rotation =  Quaternion.Euler (0f, 0f, angle);
        if (Input.GetButtonDown("Fire1")) {
        	if (GameObject.FindGameObjectsWithTag("Bullet").Length < 5) {
        		Shoot();
        	}
        }
    }
    
    //shoot function
    void Shoot () {
    	//shooting logic
 
    	Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
    	
    }
    
}
