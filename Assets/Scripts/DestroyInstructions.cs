using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyInstructions : MonoBehaviour {
	
	float time=2.5f;
	
	void Start () {
		Destroy(gameObject, time);
		
	}
	
	
}