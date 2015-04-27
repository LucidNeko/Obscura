using UnityEngine;
using System.Collections;

public class RealityRift : MonoBehaviour {

	private enum RiftState {
		Light, Dark
	}

	public GameObject m_Light;
	public GameObject m_Dark;

	private RiftState m_State = RiftState.Light;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
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
}
