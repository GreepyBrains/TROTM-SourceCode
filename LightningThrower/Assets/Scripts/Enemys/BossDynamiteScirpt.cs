using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDynamiteScirpt : MonoBehaviour 
{
	GameObject player;
	public GameObject explosion;

	public Transform savedPlayerPos;

	public float throwSpeed;

	public bool canExplode;
	bool canEnter;
	bool savePlayerPos;


	Animator anim;
	CameraFollow cf;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		anim = gameObject.GetComponent<Animator> ();
		cf = Camera.main.gameObject.GetComponent<CameraFollow> ();

		savedPlayerPos = transform.GetChild (0);
		savePlayerPos = true;
		canEnter = true;
	}


	void Update () 
	{
		if (!canExplode && savePlayerPos) 
		{
			savePlayerPos = false;
			StartCoroutine (SavePlayerPos ());
		} else if (!canExplode && !savePlayerPos)
		{
			transform.position = Vector2.MoveTowards (transform.position, savedPlayerPos.position, throwSpeed);
		} else if (canExplode) 
		{
			canExplode = false;
			StartCoroutine (ExplodeDynamite ());
		}
	}

	IEnumerator SavePlayerPos()
	{
		if (canEnter) 
		{
			canEnter = false;
			yield return new WaitForSeconds (1f);
			transform.SetParent (null);
			savedPlayerPos.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
			yield return new WaitForSeconds (0.3f);
			canExplode = true;
			canEnter = true;
		}
	}

	IEnumerator ExplodeDynamite()
	{
		anim.SetBool ("explodeWarn", true);
		while (transform.position.y >= player.transform.position.y) 
		{
			yield return null;
		}

		Instantiate (explosion, transform.position, Quaternion.identity);
		cf.ShakeCamera (0.33f, 0.3f);
		Destroy (gameObject);
	}
}
