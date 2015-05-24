using UnityEngine;
using System.Collections;

public class PushMessageScript : MonoBehaviour {

	public Animator pushPanel;
	public bool repeat = false;

	bool hasPlayed = false;
	

	void OnTriggerEnter(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.name == "Player")
			{
				pushPanel.SetBool("Push Message Hidden", false);
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.name == "Player")
			{
				hasPlayed = true;
				pushPanel.SetBool("Push Message Hidden", true);
			}
		}
	}
}
