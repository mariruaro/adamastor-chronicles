using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCaracter : MonoBehaviour {
	public bool lookRight = true;
	// Use this for initialization
	void Start () {
		
			var s = transform.localScale;
			s.x *= -1;
			transform.localScale = s;
			lookRight = !lookRight;

	}

	
	// Update is called once per frame
	void Update () {

	}
}
