using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private Vector2 geschwindigkeit;

	public float smoothTimeY;
	public float smoothTimeX;

	GameObject player;

	public bool followPlayer;
	public bool bossMode;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;


	public float shakeTimer;
	public float shakeAmount;

	GameObject leftPos;
	GameObject rightPos;
	GameObject upPos;
	GameObject downPos;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		leftPos = transform.GetChild (0).gameObject;
		rightPos = transform.GetChild (1).gameObject;
		upPos  = transform.GetChild (2).gameObject;
		downPos  = transform.GetChild (3).gameObject;

	}

	void FixedUpdate()
	{
		if (bossMode) {
			
			float posX = Mathf.SmoothDamp (transform.position.x, 0, ref geschwindigkeit.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y, 4.62f, ref geschwindigkeit.y, smoothTimeY);

			transform.position = new Vector3 (posX, posY, transform.position.z);
		} else if (leftPos.transform.localPosition.x >= player.transform.position.x || rightPos.transform.localPosition.x <= player.transform.position.x || upPos.transform.localPosition.y <= player.transform.position.y || downPos.transform.localPosition.y >= player.transform.position.y)
		{
		
		}else if (followPlayer) 
		{
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref geschwindigkeit.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref geschwindigkeit.y, smoothTimeY);

			transform.position = new Vector3 (posX, posY, transform.position.z);
		}
	}

	void Update()
	{
		if (shakeTimer >= 0) 
		{
			Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3 (transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
			shakeTimer -= Time.deltaTime;
		}
	}

	public void ShakeCamera(float shakePwr, float shakeDur)
	{
		shakeAmount = shakePwr;
		shakeTimer = shakeDur;
	}
}