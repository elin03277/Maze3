using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	
	public float speed = 5f;
	public float lookSensitivity = 3f;

	private PlayerMotor motor;

	void Start(){
		motor = GetComponent<PlayerMotor> ();
	}

	void Update(){
		float xMov = Input.GetAxisRaw ("Horizontal");
		float zMov = Input.GetAxisRaw ("Vertical"); 
        if(xMov != 0 || zMov != 0) {
            if(!AudioManager.instance.isPlaying("run")) {
                AudioManager.instance.Play("run");
            }
        } else {
            AudioManager.instance.Stop("run");
        }

        Vector3 moveHorizontal = transform.right * xMov;
		Vector3 moveVertical = transform.forward * zMov;

		Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed * Time.deltaTime * 25;
		motor.Move (velocity);

		float yRot = Input.GetAxisRaw ("Mouse X");

		Vector3 rotation = new Vector3 (0, yRot, 0f)*lookSensitivity;

		motor.Rotate (rotation);

		float xRot = Input.GetAxisRaw ("Mouse Y");

		Vector3 cameraRotation = new Vector3 (xRot, 0f, 0f)*lookSensitivity;

		motor.RotateCamera (cameraRotation);
	}

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Door")
            SceneManager.LoadScene("PongAI");
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Wall") {
            AudioManager.instance.Play("wall");
        }
    }
}
