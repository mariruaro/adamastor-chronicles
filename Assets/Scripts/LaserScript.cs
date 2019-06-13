using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class LaserScript : MonoBehaviour {
    
    private Vector2 speed = new Vector2(15, 0);
	private Renderer render;
    private Rigidbody2D laserBeam;
	private AudioSource laseraudio;
	private Animator myAnimation;
	public bool hit=false;
	public ParticleSystem explosion;
	
	// Use this for initialization
	void Start () {
        laserBeam = GetComponent<Rigidbody2D>();
		render=  GetComponent<Renderer>();
		laseraudio = this.gameObject.GetComponent<AudioSource>();
		myAnimation = GetComponent<Animator> ();
        laserBeam.velocity = speed * this.transform.localScale.x;
        Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
			
		if(myAnimation.GetBool("hit"))
		{
			render.enabled=false;
			Destroy(gameObject,0.32f);
			myAnimation.SetBool("hit", false);
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if(collidedObject.gameObject.tag== "Enemy" )
		{
			Instantiate(explosion,transform.position,transform.rotation);
			laseraudio.Play();
			myAnimation.SetBool("hit", true);				
				
            Destroy(collidedObject.gameObject);
		}
	}
}
