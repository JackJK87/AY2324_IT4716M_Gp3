using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "cus")
		{
			anim.SetTrigger("open");
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "cus")
		{
			anim.SetTrigger("close");
		}
	}
}
