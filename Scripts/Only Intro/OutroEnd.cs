using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroEnd : MonoBehaviour 
{
	GameObject introEnder;

	void Start () 
	{
		introEnder = transform.GetChild (0).gameObject;
		introEnder.SetActive (false);
		StartCoroutine (EndIntro ());
	}

	IEnumerator EndIntro()
	{
		yield return new WaitForSeconds (6f);
		introEnder.SetActive (true);
		Time.timeScale = 0;
	}

	public void OpenTwitter()
	{
		Application.OpenURL("https://twitter.com/GreepyBrainsDev");
	}
}
