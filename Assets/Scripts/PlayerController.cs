using UnityEngine;

public class PlayerController : MonoBehaviour {
	public bool drawDebug;
	public float rayDistance;
	private Ray _ray;
	private RaycastHit _hit;
	private bool _isHitting;
	private float _currentHitDistance;
	private string _targetObjectName;

	void Update() {
		_ray.origin = transform.position + (transform.forward * 0.3f);
		_ray.direction = transform.forward;

		_isHitting = Physics.SphereCast(_ray, MoveController.instance.characterController.height / 4, out _hit, rayDistance);

		_currentHitDistance = _isHitting ? _hit.distance : rayDistance;

		if (_isHitting && _hit.transform.name != "Plane") {
			_targetObjectName = _hit.transform.name;
			if (Input.GetKeyDown(KeyCode.E) && _hit.transform.gameObject.layer == 6) {
				if (InventoryController.instance.AddItemToInventory(_hit.transform.gameObject)) {
					_hit.transform.gameObject.SetActive(false);
					UIController.instance.CallMensageBox($"Pegou o item \"{_targetObjectName}\"");
				}
				else
					UIController.instance.CallMensageBox("Inventário cheio!");
			}

			if (Input.GetKeyDown(KeyCode.Mouse0) && _hit.transform.gameObject.layer == 6)
				_hit.transform.GetComponent<Follow>().SetStat(30);

		}

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;

		if (drawDebug) {
			Debug.DrawLine(_ray.origin, _ray.origin + (_ray.direction * _currentHitDistance));
			Gizmos.DrawWireSphere(_ray.origin + (_ray.direction * _currentHitDistance), MoveController.instance.characterController.height / 4);
		}
	}
}