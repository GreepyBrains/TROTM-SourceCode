using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("")]
	[Header("Player var's")]
	CameraFollow cf;
	GameObject graficObject;
	public GameObject dashPart;

	Transform shotPos;

	public float movementSpeed;
	public float dashSpeed;

	public bool canShoot;
	public bool canDash;

	Rigidbody2D rb;

	PlayerPowerHandler pph;

	[Header("Thunder bolt var's")]
	public GameObject thunderBolt;

	public float throwSpeed;


	[Header("Audio")]
	AudioSource ads;

	public AudioClip throwBoltSound;
	public AudioClip dashSound;



	void Start ()
	{
		ads = gameObject.GetComponent<AudioSource> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		pph = gameObject.GetComponent<PlayerPowerHandler> ();
		graficObject = gameObject.transform.GetChild (0).gameObject;
		shotPos = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.transform;
		cf = Camera.main.GetComponent<CameraFollow> ();
		canDash = true;
	}

	void FixedUpdate()
	{
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		graficObject.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);


		if (-Input.GetAxisRaw("Horizontal") > 0.5f || -Input.GetAxisRaw("Horizontal") < -0.5f)
		{

			transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, 0f, 0f));
		}

		if (-Input.GetAxisRaw("Horizontal") > -0.5f || -Input.GetAxisRaw("Horizontal") < 0.5f)
		{

			transform.Translate(new Vector3(-Input.GetAxisRaw("Horizontal") * -movementSpeed * Time.deltaTime, 0f, 0f));
		}

		if (-Input.GetAxisRaw("Vertical") > 0.5f || -Input.GetAxisRaw("Vertical") < -0.5f)
		{

			transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime, 0f));
		}

		if (-Input.GetAxisRaw("Vertical") > -0.5f || -Input.GetAxisRaw("Vertical") < 0.5f)
		{

			transform.Translate(new Vector3(0f, -Input.GetAxisRaw("Vertical") * -movementSpeed * Time.deltaTime, 0f));
		}
	}


	void Update ()
	{

		if(Input.GetButtonDown("Fire") && canShoot && pph.currPower > 0f)
		{
			canShoot = false;
			StartCoroutine (ThrowBolt ());
			ads.clip = throwBoltSound;
			ads.Play ();
			pph.currPower -= 0.05f;
			cf.ShakeCamera (0.1f, 0.3f);
		}

		if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.W) && canDash) 
		{
			canDash = false;
			rb.AddForce (Vector2.up * dashSpeed);
			ads.clip = dashSound;
			ads.Play ();
			GameObject currPart = (GameObject) Instantiate (dashPart, transform.position, Quaternion.identity);
			Destroy (currPart, 0.4f);
			StartCoroutine (DashInvulnerability ());
		}else if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.A) && canDash) 
		{
			canDash = false;
			rb.AddForce (Vector2.left * dashSpeed);
			ads.clip = dashSound;
			ads.Play ();
			GameObject currPart = (GameObject) Instantiate (dashPart, transform.position, Quaternion.identity);
			Destroy (currPart, 0.4f);
			StartCoroutine (DashInvulnerability ());
		}else if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.S) && canDash) 
		{
			canDash = false;
			rb.AddForce (Vector2.down * dashSpeed);
			ads.clip = dashSound;
			ads.Play ();
			GameObject currPart = (GameObject) Instantiate (dashPart, transform.position, Quaternion.identity);
			Destroy (currPart, 0.4f);
			StartCoroutine (DashInvulnerability ());
		}else if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.D) && canDash) 
		{
			canDash = false;
			rb.AddForce (Vector2.right * dashSpeed);
			ads.clip = dashSound;
			ads.Play ();
			GameObject currPart = (GameObject) Instantiate (dashPart, transform.position, Quaternion.identity);
			Destroy (currPart, 0.4f);
			StartCoroutine (DashInvulnerability ());
		}

	}

	IEnumerator ThrowBolt()
	{
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		GameObject currBolt = (GameObject)Instantiate (thunderBolt, shotPos.position, transform.rotation = Quaternion.Euler(0f, 0f, 0f));
		currBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		pph.currPower -= 0.005f;
		yield return new WaitForSeconds (0.1f);
		canShoot = true;
	}

	IEnumerator DashInvulnerability()
	{
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.15f);
		rb.velocity = new Vector2 (0, 0);
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (0.5f);
		canDash = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("EnemyBolt"))
		{
			pph.currPower -= 0.007f;
			cf.ShakeCamera (0.33f, 0.3f);
		}
	}
}