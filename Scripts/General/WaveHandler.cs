using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveHandler : MonoBehaviour 
{
	public List<GameObject> spawnPoints;
	public List<GameObject> enemyTypes;

	public GameObject boss1;
	public GameObject boss2;

	GameObject player;

	public int enemysLeft;



	Text leftText1;
	Text leftText2;

	Text waveText1;
	Text waveText2;

	PlayerPowerHandler pph;
	CameraFollow cf;

	[Header("Audio")]
	AudioSource ads;

	public AudioClip Wave1S;
	public AudioClip Wave2S;
	public AudioClip Wave3S;
	public AudioClip Wave4S;
	public AudioClip Wave5S;
	public AudioClip WaveBoss1S;
	public AudioClip Wave6S;
	public AudioClip Wave7S;
	public AudioClip Wave8S;
	public AudioClip Wave9S;
	public AudioClip Wave10S;
	public AudioClip WaveBoss2S;

	void Start () 
	{
		pph = GameObject.FindGameObjectWithTag ("Player").gameObject.GetComponent<PlayerPowerHandler> ();
		cf = Camera.main.GetComponent<CameraFollow> ();
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		ads = gameObject.GetComponent<AudioSource> ();
		ads.Stop ();

		leftText1 = transform.GetChild (0).gameObject.GetComponent<Text> ();
		leftText2 = transform.GetChild (1).gameObject.GetComponent<Text> ();
		waveText1 = transform.GetChild (2).gameObject.GetComponent<Text> ();
		waveText2 = transform.GetChild (3).gameObject.GetComponent<Text> ();

		leftText1.enabled = false;
		leftText2.enabled = false;

		waveText1.enabled = false;
		waveText2.enabled = false;
		Debug.Log (spawnPoints.Count);
	}


	void Update () 
	{
		enemysLeft = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		leftText1.text = "Left: \n" + enemysLeft;
		leftText2.text = "Left: \n" + enemysLeft;
	}

	IEnumerator SpawnEnemys(int spawnAmount, float spawnDelay)
	{

		int spawnChooser = Random.Range (0, spawnPoints.Count);;

		leftText1.enabled = true;
		leftText2.enabled = true;

		waveText1.enabled = true;
		waveText2.enabled = true;

		for (int i = 0; i < spawnAmount; i++) 
		{
			if (spawnChooser >= spawnPoints.Count) 
			{
				spawnChooser = 0;
			}

			int enemyTypeChooser = Random.Range (0, enemyTypes.Count);

			GameObject currEnemy = (GameObject) Instantiate (enemyTypes [enemyTypeChooser], spawnPoints [spawnChooser].transform.position, Quaternion.Euler(0,0,0));
			spawnChooser++;

			yield return new WaitForSeconds (spawnDelay);
		}
	}

	public IEnumerator Wave1()
	{
		waveText1.text = "Wave: \n 1";
		waveText2.text = "Wave: \n 1";

		ads.clip = Wave1S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(3, 0.2f));

		yield return new WaitForSeconds (0.1f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave2 ());
	}

	IEnumerator Wave2()
	{
		waveText1.text = "Wave: \n 2";
		waveText2.text = "Wave: \n 2";

		ads.clip = Wave2S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(5, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave3 ());
	}

	IEnumerator Wave3()
	{
		waveText1.text = "Wave: \n 3";
		waveText2.text = "Wave: \n 3";

		ads.clip = Wave3S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(8, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave4 ());
	}

	IEnumerator Wave4()
	{
		waveText1.text = "Wave: \n 4";
		waveText2.text = "Wave: \n 4";

		ads.clip = Wave4S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(12, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave5 ());
	}

	IEnumerator Wave5()
	{
		waveText1.text = "Wave: \n 5";
		waveText2.text = "Wave: \n 5";

		ads.clip = Wave5S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(15, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Boss1 ());
	}

	IEnumerator Boss1()
	{
		pph.stopLoss = true;
		player.transform.position = new Vector2 (0, 0);
		cf.bossMode = true;
		float camXSave = cf.smoothTimeX;
		float camYSave = cf.smoothTimeY;

		cf.smoothTimeX = 0.3f;
		cf.smoothTimeY = 0.3f;

		waveText1.text = "Wave: \n BOSS WAVE!";
		waveText2.text = "Wave: \n BOSS WAVE!";

		ads.clip = WaveBoss1S;
		ads.Play ();

		Instantiate (boss1, new Vector3 (0, 8.5f, 10), Quaternion.identity);

		yield return new WaitForSeconds (1f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		yield return new WaitForSeconds (0.3f);

		pph.stopLoss = true;

		float goalPower = 1f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		cf.smoothTimeX = camXSave;
		cf.smoothTimeY = camYSave;
		cf.bossMode = false;

		StartCoroutine (Wave6 ());
	}

	IEnumerator Wave6()
	{
		waveText1.text = "Wave: \n 6";
		waveText2.text = "Wave: \n 6";

		ads.clip = Wave6S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(12, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave7 ());
	}

	IEnumerator Wave7()
	{
		waveText1.text = "Wave: \n 7";
		waveText2.text = "Wave: \n 7";

		ads.clip = Wave7S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(15, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave8 ());
	}

	IEnumerator Wave8()
	{
		waveText1.text = "Wave: \n 8";
		waveText2.text = "Wave: \n 8";

		ads.clip = Wave8S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(20, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave9 ());
	}

	IEnumerator Wave9()
	{
		waveText1.text = "Wave: \n 9";
		waveText2.text = "Wave: \n 9";

		ads.clip = Wave9S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(24, 0.2f));

		yield return new WaitForSeconds (0.3f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Wave10 ());
	}

	IEnumerator Wave10()
	{
		waveText1.text = "Wave: \n 10";
		waveText2.text = "Wave: \n 10";

		ads.clip = Wave10S;
		ads.Play ();

		StartCoroutine(SpawnEnemys(30, 0.7f));

		yield return new WaitForSeconds (1f);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		float goalPower = pph.currPower + 0.5f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		yield return new WaitForSeconds (0.3f);

		StartCoroutine (Boss2 ());
	}

	IEnumerator Boss2()
	{
		pph.stopLoss = true;
		player.transform.position = new Vector2 (0, 0);
		cf.bossMode = true;
		float camXSave = cf.smoothTimeX;
		float camYSave = cf.smoothTimeY;

		cf.smoothTimeX = 0.3f;
		cf.smoothTimeY = 0.3f;

		waveText1.text = "Wave: \n BOSS WAVE!";
		waveText2.text = "Wave: \n BOSS WAVE!";

		ads.clip = WaveBoss2S;
		ads.Play ();

		Instantiate (boss2, new Vector3 (0, 8.5f, 10), Quaternion.identity);

		yield return new WaitForSeconds (1);
		yield return new WaitUntil (() => enemysLeft <= 0);

		pph.stopLoss = true;

		pph.currPower = 1;

		yield return new WaitForSeconds (0.3f);

		pph.stopLoss = true;

		float goalPower = 1f;

		for (float i = pph.currPower; i < goalPower; i += 0.005f)
		{
			pph.currPower = i;
			yield return new WaitForSeconds (pph.powerLossPerSecond);
		}

		cf.smoothTimeX = camXSave;
		cf.smoothTimeY = camYSave;
		cf.bossMode = false;

		StartCoroutine (EndGame ());
	}

	IEnumerator EndGame()
	{
		waveText1.enabled = false;
		waveText2.enabled = false;

		leftText1.enabled = false;
		leftText2.enabled = false;

		yield return new WaitForSeconds (0.2f);
		SceneManager.LoadScene (3);
	}
}