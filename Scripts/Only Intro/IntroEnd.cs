using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroEnd : MonoBehaviour 
{
	void Start () 
	{
		StartCoroutine (endIntro ());
	}

	IEnumerator endIntro()
	{
		yield return new WaitForSeconds (23f);
		SceneManager.LoadScene (2);
	}

	public void SkipIntro()
	{
		SceneManager.LoadScene (2);
	}
}
