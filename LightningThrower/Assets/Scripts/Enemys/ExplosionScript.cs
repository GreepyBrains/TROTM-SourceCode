using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour 
{
	PlayerPowerHandler pph;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip explosion;

	void Start () 
	{
		ads = gameObject.GetComponent<AudioSource> ();
		ads.clip = explosion;
		ads.Play ();
		pph = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		Destroy (gameObject, 0.7f);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			pph.currPower -= 0.12f;
		}			
	}
}
