using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnimation;
    public float forceJump;
    private float wolfKillTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float starTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;
    public AudioSource shootingSfx;

    private float shootingRate = 0.25f;
    private float shootCooldown = 0f;
    public Transform spawnLaserBeam;
    public GameObject laser;
    private GameObject ClonedLaser;

    private bool jumpCommand = false;
    private bool shootCommand = false;
    public GameObject Snow;
    private EllipsoidParticleEmitter snowobject;
    private float ActualScore
    {
        get { return (Time.time - starTime); }
    }
    // Use this for initialization
    void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimation = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();
		GameObject ClonedLaser;
		starTime = Time.time;
        snowobject = Snow.GetComponent<EllipsoidParticleEmitter>();
        Snow.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {


        Controller();
        CommandsConditions();
        SnowTimes();
        
		
        
	}

    void ScoreUpdates()
    {
        scoreText.text = (Time.time - starTime).ToString("0.0");
        if (ClonedLaser != null)
        {
            var hit = ClonedLaser.gameObject.GetComponent<Animator>(); ;
            bool point = hit.GetBool("hit");
            if (point)
            {
                starTime -= 10f;
            }
        }
    }

    void SnowTimes()
    {
        if (ActualScore > 10 && !Snow.activeSelf)
        {

            Snow.SetActive(true);

            snowobject.minSize = 0.04f;
            snowobject.maxSize = 0.1f;


        }

        if ((int)ActualScore%50==0 && Snow.activeSelf)
            //> 50f && ActualScore < 70f)||
            //(ActualScore > 31 && ActualScore < 41f) ||
            //(ActualScore > 50f && ActualScore < 61f) ||
            //(ActualScore > 70f && ActualScore < 81f) ||
            //(ActualScore > 90f && ActualScore < 101f) ||
            //(ActualScore > 110f && ActualScore < 121f))


        {
            snowobject.maxSize += 0.005f;
        }

        
    }

    void CommandsConditions()
    {
        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;

        if (shootCommand)
        {
            Fire();
            shootCooldown = shootingRate;
        }
        //animacao diferente de morte
        if (wolfKillTime == -1)
        {
            //pulo

            if (jumpCommand && jumpsLeft > 0)
            {
                myAnimation.SetBool("run", false);

                //segundo pulo igual do primeiro
                if (jumpsLeft == 2)
                {
                    myAnimation.SetBool("jump", true);

                }
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;

                }
                //segundo pulo menos poderoso que duplo click
                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * forceJump * 0.75f);
                    myAnimation.SetBool("jump", false);

                }
                else
                {
                    myRigidBody.AddForce(transform.up * forceJump);
                    myAnimation.SetBool("DoubleJump", true);

                }


                jumpsLeft--;

                jumpSfx.Play();
            }
            else if (!myAnimation.GetBool("jump") && !myAnimation.GetBool("DoubleJump"))
                myAnimation.SetBool("run", true);

            myAnimation.SetFloat("vVelocity", Mathf.Abs(myRigidBody.velocity.y));
            scoreText.text = (Time.time - starTime).ToString("0.0");
            if (ClonedLaser != null)
            {
                var hit = ClonedLaser.gameObject.GetComponent<Animator>(); ;
                bool point = hit.GetBool("hit");
                if (point)
                {
                    starTime -= 10f;
                }
            }
        }
        else
        {
            if (Time.time > wolfKillTime + 2)
            {
                SceneManager.LoadSceneAsync("GameScene");
            }
        }
        jumpCommand = false;
        shootCommand = false;
    }

    public void JumpCommand()
    {
        if (!jumpCommand)
            jumpCommand = true;
    }

    public void LaserCommand()
    {
        if (!shootCommand)
            shootCommand = true;
    }

    public void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetButtonUp("Jump"))
            jumpCommand = true;
        if (Input.GetKeyDown(KeyCode.X))
        {
            shootCommand = true;
        }
    }

    //parar codigo de enemy spawn
    void Fire()
    {
        if (myAnimation.GetBool("WolfKill")==false && shootCooldown <= 0f)
            if (laser != null)
            {
				ClonedLaser = Instantiate(laser, spawnLaserBeam.position, Quaternion.identity) as GameObject;
				
                ClonedLaser.transform.localScale = this.transform.localScale;
                shootingSfx.Play();
            }
    }

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) { 
			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			foreach (PrefabsSpawn spawner in FindObjectsOfType<PrefabsSpawn>()) {
				spawner.enabled = false;
			}

			foreach (PrefabsSpawn2 spawner in FindObjectsOfType<PrefabsSpawn2>()) {
				spawner.enabled = false;
			}
			//se o player morre troca animaçao e faz ele descer e subir na tela
			wolfKillTime = Time.time;
			myAnimation.SetBool ("WolfKill", true);
			myRigidBody.velocity = Vector2.zero;
			myRigidBody.AddForce (transform.up * forceJump);
			myCollider.enabled = false;

			deathSfx.Play ();

			float currentBestScore = PlayerPrefs.GetFloat ("BestScore", 0);
			float currentScore = Time.time - starTime;

			if (currentScore > currentBestScore) {
				PlayerPrefs.SetFloat ("BestScore", currentScore);
			}


		} else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) { //reseta numero de pulos
			jumpsLeft = 2;
            myAnimation.SetBool("jump", false);
            myAnimation.SetBool("DoubleJump", false);
		} 
	}﻿
	
}
