using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	private Vector3 m_DefaultPosition;

	public void Awake() {
		m_DefaultPosition = transform.localPosition;
	}

	public void Shake(float intensity, float duration) {
		StartCoroutine (ShakeCoroutine(intensity, duration));
	}

	public IEnumerator ShakeCoroutine(float intensity, float duration) {
		float time = 0f;
		while(time < duration) {
			time += Time.deltaTime;
			Vector3 offset = SmoothRandom.GetVector3 (intensity).normalized;
			offset.Scale(new Vector3(Random.Range(0f, 1f) < 0.5f ? 1 : -1, 
			                         Random.Range(0f, 1f) < 0.5f ? 1 : -1, 
			                         Random.Range(0f, 1f) < 0.5f ? 1 : -1));
			transform.localPosition = Vector3.Lerp(transform.localPosition, m_DefaultPosition + offset, Time.deltaTime * intensity*1.5f);
			yield return null;
		}
		transform.localPosition = m_DefaultPosition;
	}
}
