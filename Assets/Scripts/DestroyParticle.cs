using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	public float timeToDestroy;

	// Use this for initialization
	void Start () {
		Invoke ("KillParticle", timeToDestroy);
	}
	void KillParticle (){

		Destroy (gameObject);
	}	
}
