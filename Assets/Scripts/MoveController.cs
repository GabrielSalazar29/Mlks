using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour {

	public static MoveController instance;
	public float walkSpeed = 3f;
	public float runSpeed = 5f;
	public float currentSpeed;
	public float stamina = 100f;
	public int staminaConsume;
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
		currentSpeed = walkSpeed;
	}

	private void Update() {
		if (transform.position.y > 1.080005)
			_gravity -= 9 * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			StopAllCoroutines();
			StartCoroutine(Run());
		}

		Vector3 movimento = _moveInput.y * currentSpeed * Time.deltaTime * transform.forward +
							_moveInput.x * currentSpeed * Time.deltaTime * transform.right +
							new Vector3(0, _gravity, 0);

		characterController.Move(movimento);

		_rotacaoX += Input.GetAxis("Mouse X");
		_rotacaoY += Input.GetAxis("Mouse Y");

		transform.rotation = Quaternion.Euler(-Mathf.Clamp(_rotacaoY, -80, 80), _rotacaoX, 0f);
	}

    private IEnumerator Run()
    {
        float startStamina = stamina;
        float progress = 0f;
        while (stamina > 0.01f || Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            stamina -= Time.deltaTime * staminaConsume;

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentSpeed = walkSpeed;
                break;
            }

            if (stamina <= 0.01f)
            {
                UIController.instance.CallMensageBox("Você está cansado!");
				currentSpeed = walkSpeed;
			}

			yield return null;
        }

		yield return new WaitForSeconds(1);
        
        currentSpeed = walkSpeed;
        startStamina = stamina;
        progress = 0f;
        while (stamina <= 100)
        {
            progress += Time.deltaTime * 0.2f;
            stamina += Time.deltaTime * staminaConsume;
            yield return null;
        }
    }
}
