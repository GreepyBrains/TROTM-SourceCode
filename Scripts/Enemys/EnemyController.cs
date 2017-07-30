using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	[Header("General")]
	GameObject player;
	public GameObject thunderBolt;

	Transform shotPos;

	public float moveSpeed;
	public float sidewayMoveSpeed;

	bool canShoot;
	bool canShootWhileRunning;

	int wayChooser;


	PlayerPowerHandler pph;
	CameraFollow cf;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip gotHitSound;
	public AudioClip enemyThunderboltSound;

	void Start () 
	{
		ads = GameObject.FindGameObjectWithTag ("soundmanager").gameObject.GetComponent<AudioSource> ();

		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		shotPos = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform;

		wayChooser = Random.Range (0, 100);

		pph = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();

		canShoot = true;
		canShootWhileRunning = true;
	}

	void Update()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}

	void FixedUpdate () 
	{

		if (Vector2.Distance (transform.position, player.transform.position) > 10f) 
		{
			transform.position += -transform.up * Time.deltaTime * moveSpeed;
		}else if (Vector2.Distance (transform.position, player.transform.position) > 4.5f && canShootWhileRunning) 
		{
			canShootWhileRunning = false;
			StartCoroutine (ShootWhileRunning ());
			transform.position += -transform.up * Time.deltaTime * moveSpeed;
		}else if(Vector2.Distance (transform.position, player.transform.position) > 4.5f && wayChooser >= 50)
		{
			transform.position += -transform.up * Time.deltaTime * moveSpeed;
			transform.position += transform.right * Time.deltaTime * sidewayMoveSpeed;
		}else if(Vector2.Distance (transform.position, player.transform.position) > 4.5f && wayChooser < 50)
		{
			transform.position += -transform.up * Time.deltaTime * moveSpeed;
			transform.position += -transform.right * Time.deltaTime * sidewayMoveSpeed;
		} else if(Vector2.Distance (transform.position, player.transform.position) <= 4.5f && canShoot)
		{
			canShoot = false;
			StartCoroutine (Shoot ());
		}	
	}

	IEnumerator Shoot()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		ads.clip = enemyThunderboltSound;
		ads.Play ();
		GameObject currBolt = (GameObject)Instantiate (thunderBolt, shotPos.position, transform.rotation = Quaternion.Euler(0f, 0f, 0f));
		cf.ShakeCamera (0.07f, 0.3f);
		currBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		yield return new WaitForSeconds (1.6f);
		canShoot = true;
	}

	IEnumerator ShootWhileRunning()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		ads.clip = enemyThunderboltSound;
		ads.Play ();
		GameObject currBolt = (GameObject)Instantiate (thunderBolt, shotPos.position, transform.rotation = Quaternion.Euler(0f, 0f, 0f));
		cf.ShakeCamera (0.07f, 0.3f);
		currBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		yield return new WaitForSeconds (3f);
		canShootWhileRunning = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerBolt")) 
		{
			ads.clip = gotHitSound;
			ads.Play ();
			Destroy (col.gameObject);
			cf.ShakeCamera (0.35f, 0.3f);
			pph.currPower += 0.07f;
			Destroy (gameObject);
		}
	}
}