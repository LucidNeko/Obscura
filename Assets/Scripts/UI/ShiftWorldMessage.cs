using UnityEngine;
using System.Collections;

public class ShiftWorldMessage : MonoBehaviour {

	public Animator pushPanel;
	public bool repeat = false;
	
	bool hasPlayed = false;
	
	
	void OnTriggerEnter(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				pushPanel.SetBool("ShiftWorld Message Hidden", false);
			}
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if (!hasPlayed || repeat) {
			if(other.gameObject.CompareTag("Player"))
			{
				hasPlayed = true;
				pushPanel.SetBool("ShiftWorld Message Hidden", true);
			}
		}
	}
}
