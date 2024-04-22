using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerTrigger : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            anim.SetTrigger("comein");
        }
    }


}
