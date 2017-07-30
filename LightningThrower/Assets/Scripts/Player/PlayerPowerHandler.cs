using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerHandler : MonoBehaviour 
{
	[Header("General")]
	GameObject powerBar;

	public float maxPower;
	public float currPower;
	public float powerLossPerSecond;

	public bool startPowerLoss;
	public bool stopLoss;

	public bool godMode;


	DeathHandler dh;

	void Start () 
	{
		powerBar = gameObject.transform.GetChild (1).gameObject.transform.GetChild (2).gameObject;
		currPower = maxPower;
		dh = GameObject.FindGameObjectWithTag ("DeathHandler").gameObject.GetComponent<DeathHandler> ();

		startPowerLoss = true;
	}

	void Update ()
	{
		if (godMode) 
		{
			currPower = Mathf.Infinity;
		} else 
		{

			if (startPowerLoss) 
			{
				startPowerLoss = false;
				StartCoroutine (PowerLoss ());
			}

			if (currPower > 1) 
			{
				currPower = 1;
			} else if (currPower < 0.05f && currPower >= 0) 
			{
				dh.Death ();
				Time.timeScale = 0;
				currPower = 0;
			} else if (currPower <= 0) 
			{
				dh.Death ();
				Time.timeScale = 0;
				currPower = 0;
				stopLoss = true;
			}

			powerBar.transform.localScale = new Vector3 (currPower, 1, 1);
		}

	}


	IEnumerator PowerLoss()
	{
		for (currPower = currPower; currPower > 0; currPower -= 0.0002f) 
		{
			if (!stopLoss) 
			{
				yield return new WaitForSeconds (powerLossPerSecond);
			} else 
			{
				StopCoroutine (PowerLoss ());
				stopLoss = false;
			}
		}
		currPower = 0;
	}
}