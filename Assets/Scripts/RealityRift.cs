using UnityEngine;
using System.Collections;

public class RealityRift : MonoBehaviour {

	private enum RiftState {
		Light, Dark
	}

	public Transform m_Player;
	public TrackingCameraRig m_Camera;
	public ParticleSystem particleEffect;

	public bool m_HardMode = true;

	public GameObject m_Light;
	public GameObject m_Dark;

	private RiftState m_State;

	private AudioSource m_Source;

	void Start() {
		m_State = RiftState.Light;
		m_Dark.SetActive (false);

		m_Source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			m_Source.Play();
			PlayerRift ();
			switch(m_State) {
			case RiftState.Light :
				m_Light.SetActive (false);
				m_Dark.SetActive (true);
				m_State = RiftState.Dark;
				break;
			case RiftState.Dark :
				m_Dark.SetActive (false);
				m_Light.SetActive (true);
				m_State = RiftState.Light;
				break;
			}
		}
	}

	void PlayerRift() {
		StartCoroutine(Combust (10));
		StartCoroutine(m_Camera.Shake (3, 0.5f));
	}

	IEnumerator Combust(float speed) {
		float t;
		t = 0;
		while (t < 1) {
			t += Time.deltaTime * speed;
			m_Player.localScale = Vector3.Lerp(new Vector3(1,1,1), new Vector3(0, 0, 0), t);
			yield return null;
		}

		t = 0;
		while (t < 1) {
			t += Time.deltaTime * speed;
			m_Player.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(1,1,1), t);
			yield return null;
		}
	}
}
