using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

	public int kill;
	public float speed=  10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position+=Vector3.left*speed*Time.deltaTime;

		//destroi objeto depois de parametro kill
		Destroy(gameObject, kill);
	}
}
