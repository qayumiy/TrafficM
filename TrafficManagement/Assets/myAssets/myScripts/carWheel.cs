using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carWheel : MonoBehaviour {

	public WheelCollider targetWheel;

	private Vector3 wheelPos = new Vector3();
	private Quaternion wheelRot = new Quaternion();


	// Update is called once per frame

	public void Start()
	{
	//	targetWheel = this.GetComponent<WheelCollider>();
	}
	void Update () {
		targetWheel.GetWorldPose (out wheelPos, out wheelRot);

		transform.position = wheelPos;
		transform.rotation = wheelRot;
	}
}

