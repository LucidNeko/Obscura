using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	//collection of the audio clips attached to the manager
	//attach these in the inspector and reference as clip[0], clip[1] etc
	public AudioClip[] clips;

	private AudioSource source;

	void Start() {
		source = GetComponent<AudioSource> (); //grab the AudioSource
	}
	
	// Update is called once per frame
	void Update() {
		//if the '1' key is pressed, play the clip
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			PlayClip(clips[0]);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			PlayClip(clips[1]);
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			PlayClip(clips[2]);
		}
	}

	// Helper method to play the specified clip
	private void PlayClip(AudioClip clip) {
		if(source.isPlaying) {
			source.Stop();
		}

		source.clip = clip;
		source.Play ();
	}
}
