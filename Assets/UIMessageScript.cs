using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum messageTypes {Camera, Jump, Movement, Spin, Custom};

public class UIMessageScript : MonoBehaviour {


	public GameObject panel;
	public Text message;
	public messageTypes messageType;
	public string customMessage;
	public bool repeat = false;

	Animator animator;
	bool hasPlayed = false;

	string cameraMessage = "";


	// Use this for initialization
	void Start () {
		animator = panel.GetComponent<Animator>();

	}
	

	void OnTriggerEnter(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				displayMessage(messageType);
				//pushPanel.SetBool("ShiftWorld Message Hidden", false);
			}
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				hasPlayed = true;
				//pushPanel.SetBool("ShiftWorld Message Hidden", true);
			}
		}
	}



	public void displayMessage(messageTypes type){

		switch (type) {
		case messageTypes.Camera:
			message.text = "Bla bla camera";
			break;

		case messageTypes.Jump:
			message.text = "Bla bla jump";
			break;
		}

	}

}
