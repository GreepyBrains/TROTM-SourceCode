using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBossController : MonoBehaviour 
{

	[Header("General")]
	LineRenderer lr;

	public bool canShootLaser1;
	public bool canShootLaser2;
	bool isShot1;
	bool isShot2;

	GameObject leftEyePos;
	GameObject rightEyePos;

	GameObject player;

	GameObject tryingPlayerPos;
	public float playerReachSpeed;

	public Color laserIdle;
	public Color beforeLaserShot;

	PlayerPowerHandler pph;
	CameraFollow cf;


	public GameObject explosion;

	bool instaLock1 = true;
	bool instaLock2 = true;

	Canvas bossCan;

	[Header("Health")]
	float maxHealth = 1;
	float currHealth;
	GameObject healthBar;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip gotHitSound;
	public AudioClip loadingLaser;

	void Awake ()
	{
		maxHealth = 1;
		currHealth = maxHealth;
		healthBar = transform.GetChild (4).gameObject.transform.GetChild (0).gameObject.transform.GetChild (2).gameObject;

		ads = GameObject.FindGameObjectWithTag ("soundmanager").gameObject.GetComponent<AudioSource> ();

		lr = gameObject.GetComponent<LineRenderer> ();
		lr.startWidth = 0.2f;
		lr.endWidth = 0.5f;
		player = GameObject.FindGameObjectWithTag ("Player");

		pph = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();

		bossCan = transform.GetChild (4).gameObject.GetComponent<Canvas> ();
		bossCan.worldCamera = Camera.main;

		leftEyePos = transform.GetChild (1).gameObject;
		rightEyePos = transform.GetChild (2).gameObject;
		tryingPlayerPos = transform.GetChild (3).gameObject;

		StartCoroutine (StartBossFight ());
	}

	void FixedUpdate()
	{
		Vector3 dir = player.transform.position - tryingPlayerPos.transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		tryingPlayerPos.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

		tryingPlayerPos.transform.position += -tryingPlayerPos.transform.up * Time.deltaTime * playerReachSpeed;
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

		if (canShootLaser1 && instaLock1) 
		{
			instaLock1 = false;
			StartCoroutine (ShootLaser1 ());
		}

		if (!isShot1 && canShootLaser1) 
		{
			lr.SetPosition (0, leftEyePos.transform.position);
			lr.SetPosition (1, tryingPlayerPos.transform.position);
		}

		if (canShootLaser2 && instaLock2) 
		{
			instaLock2 = false;
			StartCoroutine (ShootLaser2 ());
		}

		if (!isShot2 && canShootLaser2) 
		{
			lr.SetPosition (0, rightEyePos.transform.position);
			lr.SetPosition (1, tryingPlayerPos.transform.position);
		}
	}

	IEnumerator StartBossFight()
	{
		yield return new WaitForSeconds (2f);
		canShootLaser1 = true;
	}


	IEnumerator ShootLaser1()
	{
		lr.startColor = laserIdle;
		lr.endColor = laserIdle;

		yield return new WaitForSeconds (1.5f);

		lr.startColor = beforeLaserShot;
		lr.endColor = beforeLaserShot;
		ads.clip = loadingLaser;
		ads.Play ();

		yield return new WaitForSeconds (0.5f);
		if (Vector2.Distance (player.transform.position, tryingPlayerPos.transform.position) < 0.4f) 
		{
			pph.currPower -= 0.25f;
			cf.ShakeCamera (0.33f, 0.3f);
		}
		Instantiate (explosion, lr.GetPosition (1), Quaternion.identity);

		isShot1 = true;

		yield return new WaitForSeconds (0.7f);
		lr.enabled = false;

		yield return new WaitForSeconds(2f);
		canShootLaser1 = false;
		instaLock1 = true;
		lr.enabled = true;
		canShootLaser2 = true;
		isShot1 = false;
	}

	IEnumerator ShootLaser2()
	{
		lr.startColor = laserIdle;
		lr.endColor = laserIdle;

		yield return new WaitForSeconds (1.5f);

		lr.startColor = beforeLaserShot;
		lr.endColor = beforeLaserShot;

		ads.clip = loadingLaser;
		ads.Play ();
		yield return new WaitForSeconds (0.5f);
		if (Vector2.Distance (player.transform.position, tryingPlayerPos.transform.position) < 0.4f) 
		{
			pph.currPower -= 0.25f;
			cf.ShakeCamera (0.33f, 0.3f);
		}
		Instantiate (explosion, lr.GetPosition (1), Quaternion.identity);

		isShot2 = true;

		yield return new WaitForSeconds (0.7f);
		lr.enabled = false;

		yield return new WaitForSeconds(2f);
		canShootLaser2 = false;
		instaLock2 = true;
		lr.enabled = true;
		canShootLaser1 = true;
		isShot2 = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerBolt")) 
		{
			ads.clip = gotHitSound;
			ads.Play ();
			currHealth -= 0.03f;
			cf.ShakeCamera (0.2f, 0.4f);
			pph.currPower += 0.06f;
		}
	}
}