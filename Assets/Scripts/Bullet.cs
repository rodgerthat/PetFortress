using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float velocityX = 5f;
    public float velocityY = 0f;

    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {

        rb2D = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        rb2D.velocity = new Vector2(velocityX, velocityY);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
