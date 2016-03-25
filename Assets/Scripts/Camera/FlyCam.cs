using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {
	
	
	public float speed = 50.0f;		// max speed of camera
	public float sensitivity = 0.25f; 		// keep it from 0..1
	public bool inverted = false;
	
	
	private Vector3 lastMouse = new Vector3(255, 255, 255);
	
	
	// smoothing
	public bool smooth = true;
	public float acceleration = 0.1f;
	private float actSpeed = 0.0f;			// keep it from 0 to 1
	private Vector3 lastDir = new Vector3();
	
	
	// Use this for initialization
	void Start () {
	
	}

    float rotationX;
    float rotationY;
	
	// Update is called once per frame
	void Update () {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

       transform.rotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
 
		
		
		
		
		
		// Movement of the camera
		
		Vector3 dir = new Vector3();			// create (0,0,0)
		
		if (Input.GetKey(KeyCode.W)) dir.z += 1.0f;
		if (Input.GetKey(KeyCode.S)) dir.z -= 1.0f;
		if (Input.GetKey(KeyCode.A)) dir.x -= 1.0f;
		if (Input.GetKey(KeyCode.D)) dir.x += 1.0f;
        if (Input.GetKey(KeyCode.LeftControl)) dir.y -= 1.0f;
        if (Input.GetKey(KeyCode.LeftShift)) dir.y += 1.0f;
        dir.Normalize();
		
		
		if (dir != Vector3.zero) {
			// some movement 
			if (actSpeed < 1)
				actSpeed += acceleration * Time.deltaTime * 40;
			else 
				actSpeed = 1.0f;
			
			lastDir = dir;
		} else {
			// should stop
			if (actSpeed > 0)
				actSpeed -= acceleration * Time.deltaTime * 20;
			else 
				actSpeed = 0.0f;
		}
		
		
		
		
		if (smooth) 
			transform.Translate( lastDir * actSpeed * speed * Time.deltaTime );
		
		else 
			transform.Translate ( dir * speed * Time.deltaTime );
		
		
	}
	
	void OnGUI() {
		GUILayout.Box ("actSpeed: " + actSpeed.ToString());
	}
}
