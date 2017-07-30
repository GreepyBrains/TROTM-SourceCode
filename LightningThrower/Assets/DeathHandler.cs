using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour 
{
	GameObject deathCan;

	void Awake()
	{
		deathCan = transform.GetChild (0).gameObject;
		deathCan.SetActive (false);
	}

	public void Death()
	{
		deathCan.SetActive (true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (2);
	}
}
