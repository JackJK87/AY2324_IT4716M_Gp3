using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailBook : MonoBehaviour
{
    public GameObject canvas; //trailBook canvas
    public bool openBB; // check if the canvas is open or not 
    public mouseLooking mouselooking; //getting the mouse looking script 
    public GameObject trailSolved1; //show pic of trail that we solved 
    public GameObject trailNotSolved1;

    public move playerMvm;
    public examineObject examineObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canvas.activeSelf == true)
        {
            openBB = true;
            playerMvm.enabled = false;
            mouselooking.enabled = false;
        }
        else
        {
            openBB = false;
            mouselooking.enabled = true;
            Cursor.lockState = CursorLockMode.Locked; 
            playerMvm.enabled = true;
            mouselooking.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (openBB == false)
            {
                canvas.SetActive(true);
                mouselooking.enabled = false;
                openBB = true;
                Cursor.lockState = CursorLockMode.None;
            }else
            {
                canvas.SetActive(false);
                mouselooking.enabled = true;
                openBB = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (examineObject.firstTimeWatched.Contains(examineObject.CubeCanvas))
        {
            trailSolved1.SetActive(true);
            trailNotSolved1.SetActive(false);
        }
    }
}
