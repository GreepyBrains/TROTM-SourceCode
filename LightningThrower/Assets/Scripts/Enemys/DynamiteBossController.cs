using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamiteBossController : MonoBehaviour
{
	[Header("General")]
	GameObject player;
	public GameObject dynamite;

	GameObject throwPos1;
	GameObject throwPos2;

	CameraFollow cf;
	PlayerPowerHandler pph;

	Canvas bossCan;

	[Header("Health")]
	float currHealth;
	float maxHealth = 1;
	GameObject healthBar;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip gotHitSound;

	void Awake () 
	{
		maxHealth = 1;
		currHealth = maxHealth;
		healthBar = transform.GetChild (3).gameObject.transform.GetChild (0).gameObject.transform.GetChild (2).gameObject;

		ads = GameObject.FindGameObjectWithTag ("soundmanager").gameObject.GetComponent<AudioSource> ();

		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		throwPos1 = transform.GetChild (1).gameObject;
		throwPos2 = transform.GetChild (2).gameObject;

		pph = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();
		bossCan = transform.GetChild (3).gameObject.GetComponent<Canvas>();
		bossCan.worldCamera = Camera.main;

		StartCoroutine (StartBossFight ());
	}
	

	void LateUpdate () 
	{
		if (currHealth > 0) 
		{
			healthBar.transform.localScale = new Vector3 (currHealth, 1, 1);
		} else if(currHealth <= 0)
		{
			Destroy (gameObject);
		}
	}

	IEnumerator StartBossFight()
	{
		yield return new WaitForSeconds (1f);
		StartCoroutine (ThrowDymamite1 ());
	}


	IEnumerator ThrowDymamite1()
	{
		GameObject currDyn = (GameObject) Instantiate (dynamite, throwPos1.transform.position, Quaternion.Euler(0,0, 40));
		currDyn.transform.SetParent (transform);
		yield return new WaitForSeconds (1.2f);
		StartCoroutine (ThrowDymamite2 ());
	}

	IEnumerator ThrowDymamite2()
	{
		GameObject currDyn = (GameObject) Instantiate (dynamite, throwPos2.transform.position, Quaternion.Euler(0,0, -40));
		currDyn.transform.SetParent (transform);
		yield return new WaitForSeconds (1.2f);
		StartCoroutine (ThrowDymamite1 ());
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerBolt")) 
		{
			ads.clip = gotHitSound;
			ads.Play ();
			currHealth -= 0.03f;
			cf.ShakeCamera (0.5f, 0.4f);
			pph.currPower += 0.06f;
		}
	}
}
