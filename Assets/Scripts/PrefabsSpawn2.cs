using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsSpawn2 : MonoBehaviour {

	private float nextSpawn = 0;

	public Transform[] prefabToSpawnTwo;
	public float spawnRate=3;
	public float randomDelay = 1;
	public AnimationCurve spawnCurve;
	public AnimationCurve spawnCurve2;
	private AnimationCurve mainCurve;
	public float curveLenghtInSeconds;
	private float startTime;
	private float completeTime;
	public float jitter = 0.25f;
	private Transform enemy;
	private float ActualTime
	{
		get{return Time.time- completeTime;}
	}
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		completeTime = startTime;
		//mainCurve =spawnCurve;
	}

	// Update is called once per frame
	void Update () {
		enemy=prefabToSpawnTwo[Random.Range(0, prefabToSpawnTwo.Length)];
		
		
		
		if (Time.time > nextSpawn) {
			Instantiate (enemy, transform.position, Quaternion.identity);
			nextSpawn = Time.time + spawnRate + Random.Range (0, randomDelay);

			float curvePos = (Time.time - startTime) / curveLenghtInSeconds;
			if (curvePos > 1f) {
				curvePos = 1f;
				startTime = Time.time;
			}
			//nextSpawn = Time.time + mainCurve.Evaluate (curvePos)+ Random.Range(-jitter,jitter);
		}
		if((int)ActualTime%10==0)
		{
			spawnRate -= 0.004f;
			//if(mainCurve==spawnCurve)
				//mainCurve= spawnCurve2;
			//else mainCurve = spawnCurve;
		}


	}
}