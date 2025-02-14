using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public bool drawDebug;
	public float rayDistance;
	private Ray ray;
	private RaycastHit hit;
	private bool isHitting;
	private float currentHitDistance;
	public List<GameObject> inventario = new();
	private Dictionary<string, string> teclasInventario = new Dictionary<string, string>();
	private string nomeHittado;

	void Update() {
		ray.origin = transform.position + (transform.forward * 0.3f);
		ray.direction = transform.forward;

		isHitting = Physics.SphereCast(ray, MoveController.instance.characterController.height / 4, out hit, rayDistance);

		currentHitDistance = isHitting ? hit.distance : rayDistance;

		if (isHitting && hit.transform.name != "Plane") {
			nomeHittado = hit.transform.name;
			if (Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.layer == 6) {
				inventario.Add(hit.transform.gameObject);
				hit.transform.gameObject.SetActive(false);

				UIController.instance.CallMensageBox($"Pegou o item \"{nomeHittado}\"");
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;

		if (drawDebug) {
			Debug.DrawLine(ray.origin, ray.origin + (ray.direction * currentHitDistance));
			Gizmos.DrawWireSphere(ray.origin + (ray.direction * currentHitDistance), MoveController.instance.characterController.height / 4);
		}
	}
}