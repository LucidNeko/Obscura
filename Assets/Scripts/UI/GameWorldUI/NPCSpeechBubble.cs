using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCSpeechBubble : MonoBehaviour {

	public string[] strings;
	public Text messageText;
	public Animator NPCAnimator;

	private int numMessages;
	private int messageIndex;

	private float readTime = 2.0F; // Adjusts the read time of a message
	private float charSpeed = 0.05F; // Adjusts how quickly the text writes to screen


	// Use this for initialization
	void Start () {

		numMessages = strings.Length;


		
	}
	



	void OnTriggerEnter(Collider other){




		// If the player walks into the trigger
		if (other.gameObject.CompareTag ("Player")) {

			// Reset the index to 0
			messageIndex = 0;

			// Make sure there is a message to display
			if (numMessages > 0) {

				NPCAnimator.SetBool("showNPCMessage", true);

				StartCoroutine (displayMessage (strings [0]));
			}
		}
	}

	/**
	 * Displays each message in order and each message character by character.
	 */
	IEnumerator displayMessage(string str){ 
		
		messageText.text = ""; 
		int i = 0; 
		
		while( messageIndex <= numMessages && i < str.Length){

			messageText.text += str[i++]; 
			
			if( i == str.Length ){

				// Wait for player to read message.
				yield return new WaitForSeconds(readTime);

				// Return the message text to empty.
				messageText.text = ""; 

				// If not the last message.
				if(messageIndex < numMessages -1){
					i = 0; 
					messageIndex ++;
					str = strings [messageIndex];
				}
			}
			else{
				// Wait between characters.
				yield return new WaitForSeconds(charSpeed); 
			}
		}

		NPCAnimator.SetBool("showNPCMessage", false);
	}
	


	




}
