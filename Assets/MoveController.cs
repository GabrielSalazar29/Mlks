using UnityEngine;

public class MoveController : MonoBehaviour {
	public int speed;
	private float rotacaoX;


	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}
	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.W))
			transform.position += speed * Time.deltaTime * Vector3.forward;

		if (Input.GetKey(KeyCode.S))
			transform.position += speed * Time.deltaTime * Vector3.back;

		if (Input.GetKey(KeyCode.A))
			transform.position += speed * Time.deltaTime * Vector3.left;

		if (Input.GetKey(KeyCode.D))
			transform.position += speed * Time.deltaTime * Vector3.right;


		rotacaoX += Input.GetAxis("Mouse X");

		transform.rotation = Quaternion.Euler(0f, rotacaoX, 0f);
	}
}
