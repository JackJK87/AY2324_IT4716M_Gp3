using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue : MonoBehaviour
{
    public Text textComponment;
    public string[] lines;
    public float textSpeed = .05f;
    public GameObject canvas;

    private int index;

    public Transform cam;
    [SerializeField] private LayerMask interactionLayer;
    public bool talked = false;

    void Stsrt()
    {
        GetComponent<move>();
        GetComponent<mouseLooking>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("cus"))
            {
                if (playerMovement.talked == false)
                {
                    playerMovement.talked = true;
                    Debug.Log("E");
                    textComponment.text = string.Empty;
                    StartDialogue();
                }

            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (textComponment.text == lines[index])
            {
                NextLine();

            }
            else
            {
                StopAllCoroutines();
                textComponment.text = lines[index];
            }
        }


    }
    void StartDialogue()
    {
        canvas.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
        GetComponent<move>().enabled = false;
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponment.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index <lines.Length - 1)
        {
            index++;
            textComponment.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            canvas.SetActive(false);
            GetComponent<move>().enabled = true;
            playerMovement.talked = true;
        }
    }

}