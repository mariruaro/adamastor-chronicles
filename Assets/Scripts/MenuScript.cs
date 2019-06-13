using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	public Text txtBestScore;
	public GameObject audioOnIcon;
	public GameObject audioOffIcon;

	void Start(){
		txtBestScore.text = PlayerPrefs.GetFloat ("BestScore", 0).ToString ("0.0");

		SetSoundState ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void StartGame(){
		SceneManager.LoadScene("GameScene");
	}

	public void ToggleSound(){
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) {
			PlayerPrefs.SetInt ("Muted", 1);
		} else {
			PlayerPrefs.SetInt ("Muted", 0);
		}

		SetSoundState ();
	}

	private void SetSoundState(){
		if (PlayerPrefs.GetInt ("Muted", 0) == 0) {
			AudioListener.volume = 1;
			audioOnIcon.SetActive (true);
			audioOffIcon.SetActive (false);
		} else {
			AudioListener.volume = 0;
			audioOnIcon.SetActive (false);
			audioOffIcon.SetActive (true);
		}
	}
}

