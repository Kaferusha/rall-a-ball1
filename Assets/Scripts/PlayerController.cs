using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	private Rigidbody rb;
	private int count;
	private bool isFalling = false;

	void Start() {
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		winText.text = "";
	}

	void FixedUpdate() {
		float moveHorizontal;
		float moveVertical;

		if(SystemInfo.deviceType == DeviceType.Desktop) {
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
			rb.AddForce(movement * speed);

			if(Input.GetKey (KeyCode.Space) && !isFalling){
				rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
			}

		} else {
			moveHorizontal = Input.acceleration.x;
			moveVertical = Input.acceleration.y;
			Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
			rb.AddForce(movement * speed);
		}


		isFalling = true;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive(false);
			count++; 
			SetCountText();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString();
		if(count >= 12)
			winText.text = "You win!";
	}

	void OnCollisionStay() {
		isFalling = false;
	}
}