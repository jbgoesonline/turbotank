using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the class that contains aim information
class AimClass {
    public RaycastHit2D hit;
	public RaycastHit2D reflectHit;
	public Vector2 hitAimCords;
	
	public AimClass (RaycastHit2D hit1, RaycastHit2D reflectHit1, Vector2 hitAimCords1) {
		hit = hit1;
		reflectHit = reflectHit1; //RIGHT NOTATION?? do I need new operator
		hitAimCords = hitAimCords1;
    	}
    }

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
        	//call aim and shoot script
        rotateCannon();
    }
    
    void ifShoot () {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < 5) {
        	Shoot();
        }
    }
    
    void rotateCannon () {
    	
    	timer += Time.deltaTime;
    	
        
        //recycles timer
        if (timer > waitTime) {
        	MousePos = aim();
        	timer = timer - waitTime;
        	//rotate cannon
        	Vector2 lookDir = MousePos-transform.position;
        	float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg-90 ;
        	transform.rotation =  Quaternion.Euler (0f, 0f, angle);
        	ifShoot();
        }
    }
    
    //shoot function
    void Shoot () {
    	//shooting logic
 
    	Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
    	
    }
    
    
    //change this function to create an array of every correct answer (then choose one).
    //Add to this function parameters that I can change. Randomness, only reflect, near hits, only direct, shooting speed, etc. can choose values near in the array for 'wobble' or near hits
    //aiming including raycasting
    Vector3 aim () {
    	
    	List <AimClass> listOfAims = new List <AimClass> ();
    	
    	
    	for (float i = 0.0f; i < 2*Mathf.PI; i+=.1f) {
    		
    		//raycast whole circle for hits
    		Vector2 unitCirclePos = new Vector2 (Mathf.Cos(i), Mathf.Sin(i));
    		Vector2 circleOutsideTank = new Vector2(GameObject.Find("Bot").transform.position.x + (unitCirclePos.x * 3), GameObject.Find("Bot").transform.position.y + (unitCirclePos.y * 3));
    		RaycastHit2D hitTemp = Physics2D.Raycast(circleOutsideTank, unitCirclePos);
    		//if I get a hit on the player tank, fire bullet
    		
    		RaycastHit2D reflectHitTemp = Physics2D.Raycast(hitTemp.point, Vector2.Reflect(unitCirclePos, hitTemp.normal));
    		listOfAims.Add(new AimClass(hitTemp, reflectHitTemp, circleOutsideTank));
    		}

    		
    	for (int j = 0; j < listOfAims.Count; j+=1) {
    		if (listOfAims[j].hit.collider.tag == "Player") {
    			//Vector3 returnValue = new Vector3 (circleOutsideTank.x, circleOutsideTank.y, 0);
    			//Vector3 returnValue = new Vector3 (hitsX[j], hitsY[j], 0);
    			return listOfAims[j].hitAimCords;
    			}
    		if (listOfAims[j].reflectHit.collider.tag == "Player") {
    			//Vector3 returnValue = new Vector3 (circleOutsideTank.x, circleOutsideTank.y, 0);
    			//Vector3 returnValue = new Vector3 (hitsX[j], hitsY[j], 0);
    			return listOfAims[j].hitAimCords;
    			}
    		}
    	
    	Vector3 returnValue1 = new Vector3 (1, -10, 0);
    	return returnValue1;
    	}
    }
    

