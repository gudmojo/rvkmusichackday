using UnityEngine;
using System.Collections;

public class BillboardBehavior : MonoBehaviour {
	
	public Transform cameraTransform;
	
	
	// Update is called once per frame
	void LateUpdate () {		
		this.transform.LookAt(cameraTransform);
		var euler = this.transform.eulerAngles;
		this.transform.rotation = Quaternion.Euler(90, euler.y, euler.z);
	}
}
