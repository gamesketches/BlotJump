using UnityEngine;
using System.Collections;

public class CameraRotationLock : MonoBehaviour {

	Quaternion myRotation;
	// Use this for initialization
	void Start () {
		myRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = myRotation;
	}
}
