using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Follow : MonoBehaviour {
	public NavMeshAgent agent;
	public Image image;
	private float life = 100;
	private float timer;

	public void Initialize() {
		life = 100;
		image.fillAmount = 1;
	}

	// Update is called once per frame
	void Update() {
		timer += Time.deltaTime;
		if (timer > 3) {
			timer = 0;
			var zumbis = transform.parent.childCount;

			if (zumbis < 10)
				Instantiate(this, transform.parent).Initialize();
		}

		agent.SetDestination(MoveController.instance.transform.position);
	}

	public void SetStat(int value) {
		life -= value;
		StartCoroutine(Animate(life / 100));

		if (life < 0) {
			Kill();
		}
	}

	private IEnumerator Animate(float target) {
		var start = image.fillAmount;
		var progress = 0f;
		while (image.fillAmount != target) {
			progress += Time.deltaTime * 5;
			image.fillAmount = Mathf.Lerp(start, target, progress);
			yield return null;
		}
	}

	public void Kill() {
		Destroy(gameObject);
	}
}
