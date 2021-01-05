using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	//shooting target
	public GameObject bullet;
    public float bulletSpeed = 20f;
    public Rigidbody2D rb;
	Vector2 velocity;
	private int wallCount = 0;
	public int maxWallCount = 1;
    
    // Start is called before the first frame update
    void Start() {
   		//bullet speed
    	rb.velocity = transform.right * bulletSpeed;
	
    }
    
    void Update() {
		velocity = rb.velocity;
    }
    
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
    		Destroy(bullet);
    	}
	
		if (wallCount >= maxWallCount) {
			Destroy(bullet);
		}
		else {
			float speed = velocity.magnitude;
        	Vector3 direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal);
        	rb.velocity = direction * speed;
        	wallCount += 1;
        }

	}
}
