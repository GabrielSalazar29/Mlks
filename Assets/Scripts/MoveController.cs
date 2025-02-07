using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour {

	public static MoveController instance;
	public int speed;
	public CharacterController characterController;
	private float _rotacaoX;
	private float _rotacaoY;
	private Vector2 _moveInput;
	private float _gravity;

	private void Awake() {
		if (instance != null)
			Destroy(instance);

		instance = this;
	}

	public void OnMove(InputValue value) {
		_moveInput = value.Get<Vector2>();
	}

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update() {
		if (transform.position.y > 1.080005)
			_gravity -= 9 * Time.deltaTime;

		Vector3 movimento = _moveInput.y * speed * Time.deltaTime * transform.forward +
							_moveInput.x * speed * Time.deltaTime * transform.right +
							new Vector3(0, _gravity, 0);

		characterController.Move(movimento);

		_rotacaoX += Input.GetAxis("Mouse X");
		_rotacaoY += Input.GetAxis("Mouse Y");

		transform.rotation = Quaternion.Euler(-Mathf.Clamp(_rotacaoY, -80, 80), _rotacaoX, 0f);
	}
}
