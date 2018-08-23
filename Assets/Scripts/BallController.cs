using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Boo.Lang;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class BallController : MonoBehaviour
{
	public Vector3 Force;
	public float Distance;
	

	public Material MaterialBallScored;
	private Vector3 scaler = new Vector3(1000, 1000, 1000);
	private bool hasBeenScored = false;
	
	// Use this for initialization
	void Start()
	{
		var scaledForce = Vector3.Scale(scaler, Force);
		GetComponent<Rigidbody>().AddForce(scaledForce);
		StartCoroutine(DoDespawn(20));
	}
	
	void Update()
	{
		if(transform.position.y < 0)
			Destroy(gameObject);
	}

	IEnumerator DoDespawn(float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
	
	private bool hasTriggeredTop = false;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Court")
		{
			StartCoroutine(DoDespawn(1.0f));
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "TriggerTop")
		{
			hasTriggeredTop = true;
		}
        else if (other.name == "TriggerBottom")
        {
			if (hasTriggeredTop  && !hasBeenScored)
			{
				GetComponent<Renderer>().material = MaterialBallScored;
				//BallSpawnerController.Instance.DoScoreReport(String.Format("{0}, {1}", Distance, Force.y));
				BallSpawnerController.Instance.DoScoreReport(Distance, Force.y);
				
			}

			hasBeenScored = true;
		}
	}

	

}
