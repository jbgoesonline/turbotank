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
    void FixedUpdate() {
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
    
    //aiming including raycasting
    Vector3 aim () {
    	for (float i = 0.0f; i < 2*Mathf.PI; i+=.01f) {
    		//raycast whole circle for hits
    		Vector2 unitCirclePos = new Vector2 (Mathf.Cos(i), Mathf.Sin(i));
    		Vector2 circleOutsideTank = new Vector2(GameObject.Find("Bot").transform.position.x + (unitCirclePos.x * 2), GameObject.Find("Bot").transform.position.y + (unitCirclePos.y * 2));
    		Vector2 biggerCircleOutsideTank = new Vector2(GameObject.Find("Bot").transform.position.x + (unitCirclePos.x * 3), GameObject.Find("Bot").transform.position.y + (unitCirclePos.y * 3));
    		RaycastHit2D hit = Physics2D.Raycast(circleOutsideTank, biggerCircleOutsideTank);
    		//if I get a hit on the player tank, fire bullet
    		//Debug.Log(circleOutsideTank);
    		Debug.Log(hit.collider.tag);
    		if (hit.collider.tag == "Player") {
    			Vector3 returnValue = new Vector3 (unitCirclePos.x, unitCirclePos.y, 0);
    			return returnValue;
    		}
    	}
    	Vector3 returnValue1 = new Vector3 (1, 10, 0);
    	return returnValue1;
    }
    
}

