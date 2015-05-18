using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicMessage : MonoBehaviour {

	public Animator panel;
	public Text message;

	void Start(){
		panel.SetBool("isHidden", true);
	}


	void OnCollisionEnter(Collision Collection)
	{
		if(Collection.gameObject.name == "Player")
		{
			message.text = "I changed the text";
			panel.SetBool("isHidden", false);
		}
	}

}
