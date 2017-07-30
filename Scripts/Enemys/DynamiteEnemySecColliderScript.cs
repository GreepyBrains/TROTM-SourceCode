using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteEnemySecColliderScript : MonoBehaviour 
{
	PlayerPowerHandler pph;
	CameraFollow cf;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip gotHitSound;

	void Start()
	{
		ads = GameObject.FindGameObjectWithTag ("soundmanager").gameObject.GetComponent<AudioSource> ();
		pph = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("PlayerBolt")) 
		{
			ads.clip = gotHitSound;
			ads.Play ();
			Destroy (col.gameObject);
			cf.ShakeCamera (0.33f, 0.25f);
			pph.currPower += 0.1f;
			Destroy (gameObject.transform.parent.gameObject);
		}
	}
}
