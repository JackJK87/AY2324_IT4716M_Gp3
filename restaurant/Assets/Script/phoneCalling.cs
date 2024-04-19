using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneCalling : MonoBehaviour
{

    public static bool called = false;
    public static bool pickCall = false;

    public AudioSource ringingSound;
    public AudioSource pickCallSound;

    public static bool startTheCall = false;
    public static bool onlyPlayOnce = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (startTheCall == true&& pickCall == false && onlyPlayOnce == false && dialogueShowItself.talkEnd == true)
        {
            onlyPlayOnce = true;
            ringingSound.Play();
            
        }

        if (Input.GetMouseButtonDown(0)&&pickCall==false && startTheCall == true && dialogueShowItself.talkEnd == true && onlyPlayOnce==true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray ,out hit))
            {
                if (hit.collider.CompareTag("test"))
                {
                        pickCall = true;
                        pickCallSound.Play();

                        ringingSound.Stop();
                        called = true;
                }
            }
            
        }


    }

}
