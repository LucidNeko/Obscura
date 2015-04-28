﻿using UnityEngine;
using System.Collections;

public class RealityRift : MonoBehaviour {

	private enum RiftState {
		Light, Dark
	}

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
		if (m_HardMode) {
			if (Input.GetKey(KeyCode.LeftShift)) {

				if(m_State == RiftState.Light) {
					m_Source.Play();
				}

				m_Light.SetActive (false);
				m_Dark.SetActive (true);
				m_State = RiftState.Dark;
			} else {

				if(m_State == RiftState.Dark) {
					m_Source.Play();
				}

				m_Dark.SetActive (false);
				m_Light.SetActive (true);
				m_State = RiftState.Light;
			}
		} else {
			if (Input.GetKeyDown (KeyCode.LeftShift)) {

				m_Source.Play();

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
}
