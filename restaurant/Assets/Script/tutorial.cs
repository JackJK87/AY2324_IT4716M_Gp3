using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{

    public GameObject DoorText;
    public GameObject PhoneText;
    public bool doorTrigger = false;
    public bool Triggered = false;

    void Start()
    {

    }

    void Update()
    {
        if (phoneCalling.pickCall == false&&dialogueShowItself.talkEnd == true)
        {
            DoorText.SetActive(true);
            doorTrigger = true;
        }if (phoneCalling.onlyPlayOnce == true)
        {
            DoorText.SetActive(false);
            PhoneText.SetActive(true);
        }if (phoneCalling.pickCall == true)
        {
            PhoneText.SetActive(false);
        }
    }
}
