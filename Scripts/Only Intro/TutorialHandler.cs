using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour 
{
	GameObject tutImg;

	PlayerController pc;
	WaveHandler wh;

	void Awake () 
	{
		tutImg = transform.GetChild (0).gameObject;
		tutImg.SetActive (true);
		Time.timeScale = 0;

		pc = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerController> ();
		pc.canShoot = false;

		wh = GameObject.FindGameObjectWithTag ("WaveHandler").gameObject.GetComponent<WaveHandler> ();
	}


	public void GotItButton()
	{
		tutImg.SetActive (false);
		Time.timeScale = 1;
		StartCoroutine (NoShootAfterButtonPressed ());
	}

	IEnumerator NoShootAfterButtonPressed()
	{
		yield return new WaitForSeconds (0.3f);
		pc.canShoot = true;
		StartCoroutine (wh.Wave1 ());
	}
}
