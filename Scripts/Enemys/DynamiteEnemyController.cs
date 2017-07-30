using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteEnemyController : MonoBehaviour 
{
	[Header("General")]
	GameObject player;
	public GameObject explosion;

	Transform shotPos;

	Animator anim;
	public float moveSpeed;

	PlayerPowerHandler pph;
	CameraFollow cf;


	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		shotPos = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform;
		anim = gameObject.GetComponent<Animator> ();
		pph = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();
	}

	void Update()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}

	void FixedUpdate () 
	{
		if (Vector2.Distance (transform.position, player.transform.position) > 0.4f) 
		{
			transform.position += -transform.up * Time.deltaTime * moveSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			StartCoroutine (StartWarning ());
		}
	}

	IEnumerator StartWarning()
	{
		anim.SetBool ("warnToExplode", true);
		moveSpeed = moveSpeed * 1.5f;
		yield return new WaitForSeconds (0.9f);
		Destroy (gameObject);
		cf.ShakeCamera (0.5f, 0.4f);
		Instantiate (explosion, transform.position, Quaternion.identity);
	}
}