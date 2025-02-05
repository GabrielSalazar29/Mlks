using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour {
	public int speed;
	private float rotacaoX;
	private float rotacaoY;
	private Vector2 moveInput;
	private Vector2 lookInput;
	public CharacterController characterController;

	public void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }


    private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		
	}
	// Update is called once per frame
	void Update() {
		transform.Translate(moveInput.x * speed * Time.deltaTime, 0f, moveInput.y * speed * Time.deltaTime);		
		//characterController.Move(new Vector3(moveInput.x * speed * Time.deltaTime, 0f, moveInput.y * speed * Time.deltaTime));

		rotacaoX += Input.GetAxis("Mouse X");
		rotacaoY += Input.GetAxis("Mouse Y");

		transform.rotation = Quaternion.Euler(-Mathf.Clamp(rotacaoY, -80, 80), rotacaoX, 0f);
	}
}
