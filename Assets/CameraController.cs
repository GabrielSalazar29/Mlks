using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Camera camera;
	private const float playerFov = 90f;

	void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			StopAllCoroutines();
			StartCoroutine(Zoom(30f));
		}
	}

	public IEnumerator Zoom(float target) {

		var startFOV = camera.fieldOfView;
		var progress = 0f;
		while (camera.fieldOfView != target) {
			progress += Time.deltaTime * 2;
			camera.fieldOfView = Mathf.Lerp(startFOV, target, progress);
			if (Input.GetKeyUp(KeyCode.F))
				break;
			yield return null;
		}

		yield return new WaitUntil(() => !Input.GetKey(KeyCode.F));

		startFOV = camera.fieldOfView;
		progress = 0f;
		while (camera.fieldOfView != playerFov) {
			progress += Time.deltaTime * 2;
			camera.fieldOfView = Mathf.Lerp(startFOV, playerFov, progress);
			yield return null;
		}
	}
}