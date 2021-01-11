using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the class that contains aim information
class AimClass {
    public RaycastHit2D hit;
	public RaycastHit2D reflectHit;
	public Vector2 hitAimCords;
	public bool timedHit;
	
	public AimClass (RaycastHit2D hit1, RaycastHit2D reflectHit1, Vector2 hitAimCords1, bool timedHit1) {
		hit = hit1;
		reflectHit = reflectHit1;
		hitAimCords = hitAimCords1;
		timedHit = timedHit1;
    	}
    }

public class BOTWeapon : MonoBehaviour
{
	public Transform firePoint;
	public GameObject bulletPrefab;
    Vector3 MousePos;
    //public GameObject player;
    
    //shooting settings
    public int randomWeight;
    public int reflectWeight;
    public int straightWeight;
    public int timedWeight;
    public int burstMax;
    public float burstSpeed;

	//timer details
	public float waitTime = 2.0f;
	float currentWaitTime;
	float timer = 0.0f;
	int countBurst;
	
	void Start () {
		countBurst = burstMax - 1;
		currentWaitTime = waitTime;
	}
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
        if (timer > currentWaitTime) {
        	//burst logic
        	if (countBurst > 0) {
        		currentWaitTime = burstSpeed;
        		countBurst = countBurst - 1;
        	}
        	else {
        		countBurst = burstMax - 1;
        		currentWaitTime = waitTime;
        	}
        	
        	MousePos = aim();
        	timer = 0.0f;
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
    	
    	Rigidbody2D playerRB = GameObject.FindWithTag("Player").GetComponent <Rigidbody2D> ();
    	    	
    	for (float i = 0.0f; i < 2*Mathf.PI; i+=.05f) {
    		
    		//raycast whole circle for hits
    		Vector2 unitCirclePos = new Vector2 (Mathf.Cos(i), Mathf.Sin(i));
    		Vector2 circleOutsideTank = new Vector2(GameObject.Find("Bot").transform.position.x + (unitCirclePos.x * 3), GameObject.Find("Bot").transform.position.y + (unitCirclePos.y * 3));
    		//straight hit
    		RaycastHit2D hitTemp = Physics2D.Raycast(circleOutsideTank, unitCirclePos);
    		//reflection hit
    		RaycastHit2D reflectHitTemp = Physics2D.Raycast(hitTemp.point, Vector2.Reflect(unitCirclePos, hitTemp.normal));
    		
    		//timed hit
    		bool tempBoolTimedHit = false;
    		for (float t = 0.0f; t < 5.0f; t+=.1f) {
    			//position at time t for tank
    			Vector2 positionTank = (playerRB.velocity * t) + playerRB.position;
    			//position at time t for bullet
    			Vector2 firePointPosition = circleOutsideTank;//GameObject.Find("/Bot/Rotator/Turret/FirePoint").transform.position;
    			Vector2 bulletVelocity = unitCirclePos * 20;
    			Vector2 positionBullet = (bulletVelocity * t) + firePointPosition;
    			//if they're close to each other
    			Vector2 distance = positionTank - positionBullet;
    			
    			if (distance.magnitude < 1.0f) {
    				tempBoolTimedHit = true;
    			}
    		}
    		
    		//add to class
    		listOfAims.Add(new AimClass(hitTemp, reflectHitTemp, circleOutsideTank, tempBoolTimedHit));
    		}

    		
    	List <AimClass> listOfHits = new List <AimClass> ();
    	//adds straight hits to list
    	int count = 0;
    	for (int j = 0; count < straightWeight; j+=1) {
    		int num = j % listOfAims.Count;
    		if (listOfAims[num].hit.collider.tag == "Player") {
    			listOfHits.Add(listOfAims[num]);
    			count +=1;
    			}
    			
    		if (j > listOfAims.Count & count == 0) {
    			count = straightWeight;
    			}
    		}
    	
    	//adds reflected hits to list
    	count = 0;
    	for (int j = 0; count < reflectWeight; j+=1) {
    		int num = j % listOfAims.Count;		
    		if (listOfAims[num].reflectHit.collider.tag == "Player" & listOfAims[num].hit.collider.tag != "Player") {
    			listOfHits.Add(listOfAims[num]);
    			count +=1;
    			}
    			
    		if (j > listOfAims.Count & count == 0) {
    			count = reflectWeight;
    			}
    		}
    	
    	//adds timed shots to list
    	count = 0;
    	for (int j = 0; count < timedWeight; j+=1) {
    		int num = j % listOfAims.Count;		
    		if (listOfAims[num].timedHit) {
    			listOfHits.Add(listOfAims[num]);
    			count +=1;
    			}
    			
    		if (j > listOfAims.Count & count == 0) {
    			count = timedWeight;
    			}
    		}
    	
    	
    	//adds random shots to list	
    	for (int j = 0; j < randomWeight; j+=1) {
    		int num = Random.Range(0, listOfAims.Count);
    			listOfHits.Add(listOfAims[num]);
    	}
    	
    	if (listOfHits.Count > 0) {
    		return listOfHits[(int) Random.Range(0, listOfHits.Count)].hitAimCords;
    		}
    	
    	Debug.Log("List Empty");	
    	return new Vector3 (GameObject.Find("Bot").transform.position.x + Random.Range (-10, 10), GameObject.Find("Bot").transform.position.y + Random.Range (-10, 10), 0);

    	//return listOfAims[j].hitAimCords;
    	
    	}
    }