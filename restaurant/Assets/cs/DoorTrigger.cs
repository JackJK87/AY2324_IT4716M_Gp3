using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player")
		{
			anim.SetTrigger("open");
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "Player")
		{
			anim.SetTrigger("close");
		}
	}
}
