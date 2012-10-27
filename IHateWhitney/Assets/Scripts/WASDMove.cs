using UnityEngine;
using System.Collections;

public class WASDMove : MonoBehaviour {
	
	public float horizontalRate;
	public float verticalRate;
	public Transform target;
	
	// Use this for initialization
	void Start () {
		if (target == null)
		{
			target = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		var deltaX = Input.GetAxis("Horizontal") * horizontalRate * Time.deltaTime;
		var deltaY = Input.GetAxis("Vertical") * verticalRate * Time.deltaTime;
		
		var translation = new Vector3(deltaX, 0, deltaY);
		
		transform.Translate(translation);
		
	}
}
