using UnityEngine;
using System.Collections;

public class SprintMessageScript : MonoBehaviour {

	public Animator pushPanel;
	public bool repeat = false;
	
	bool hasPlayed = false;
	
	
	void OnTriggerEnter(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				pushPanel.SetBool("Sprint Message Hidden", false);
			}
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				hasPlayed = true;
				pushPanel.SetBool("Sprint Message Hidden", true);
			}
		}
	}
}

		
