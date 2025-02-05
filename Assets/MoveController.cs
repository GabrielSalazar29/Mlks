using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour {
	public int speed;
	private float rotacaoX;
	private float rotacaoY;
	private Vector2 moveInput;
	private Vector2 lookInput;
	public CharacterController characterController;
	private float gravity;

	public void OnMove(InputValue value) {
		moveInput = value.Get<Vector2>();
	}
	public void OnLook(InputValue value) {
		lookInput = value.Get<Vector2>();
	}


	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		if (transform.position.y > 1.080005)
			gravity -= 9 * Time.deltaTime;

		Vector3 movimento = moveInput.y * speed * Time.deltaTime * transform.forward +
							moveInput.x * speed * Time.deltaTime * transform.right +
							new Vector3(0, gravity, 0);

		characterController.Move(movimento);

		rotacaoX += Input.GetAxis("Mouse X");
		rotacaoY += Input.GetAxis("Mouse Y");

		transform.rotation = Quaternion.Euler(-Mathf.Clamp(rotacaoY, -80, 80), rotacaoX, 0f);
	}
}
