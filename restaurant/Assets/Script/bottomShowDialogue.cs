using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bottomShowDialogue : MonoBehaviour
{
    public Text text; 
    public string[] lines; //set the place to place lines
    public float textSpeed = .05f; //set lines display speed
    public GameObject canvas;
    private int index; 
    public Transform cam;
    public float distance = 1f; //auto text out 
    public Transform cus; //check customer distance to player
    public bool talked = false;

    void Start()
    {

    }

    void Update()
    {
        //float playerDistance = Vector3.Distance(transform.position, cus.position);

        if (Input.GetKeyDown(KeyCode.E)&&talked == false) //if player close enough to customer 
        {
            StartDialogue();
            talked = true;
        }
        else
        {

        }

        
    }

    void StartDialogue()
    {
        canvas.SetActive(true);
        index = 0;
        StartCoroutine(UpdateDialogue(lines[index]));
    }

    private IEnumerator UpdateDialogue(string dialogue)
    {
        text.text = dialogue;
        yield return new WaitForSeconds(3);

        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(UpdateDialogue(lines[index]));
        }
        else
        {
            canvas.SetActive(false);
        }
    }

}
