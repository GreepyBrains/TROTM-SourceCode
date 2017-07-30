using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBoltController : MonoBehaviour 
{
	[Header("General")]
	public float speed;
	void Start () 
	{
		Destroy (gameObject, 7f);
	}

	void FixedUpdate () 
	{
		transform.position += transform.right * Time.deltaTime * speed;
	}
}