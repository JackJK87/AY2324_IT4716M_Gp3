using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{

    public GameObject dialogueUI;
    public int dialogueLine = 0;
    public int dialogueL = 5;
    private bool shown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shown = true;
        }

        if (shown == true)
        {
            dialogue();
        }
    }
    
    public void dialogue()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (dialogueLine < dialogueL)
            {
                Debug.Log("+");
                dialogueLine++;
            }
        }
        
    }

    public void game()
    {
        SceneManager.LoadScene("game");
    }
}


