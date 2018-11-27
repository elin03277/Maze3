using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	public Camera cam;

	private Vector3 velocity;
	private Vector3 rotation;
	public Vector3 cameraRotation;

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity){
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation){
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _cameraRotation){
		cameraRotation = _cameraRotation;
	}

	void FixedUpdate(){
		PerformMovement ();
		PerformRotation ();
	}

	void PerformMovement(){
		rb.velocity = velocity;
	}

	void PerformRotation(){
		rb.MoveRotation (rb.rotation * Quaternion.Euler(rotation));
		if (cam != null) {
			cam.transform.Rotate (-cameraRotation);
		}
	}
}
