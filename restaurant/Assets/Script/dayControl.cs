using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayControl : MonoBehaviour
{

    public static bool gameStart;
    public dialogueShowItself gameOpen;

    public static bool showTutorialText = false;

    void Start()
    {
        gameStart = false;
        gameOpen.enabled = false;
    }

    void Update()
    {
        if (gameStart == false&& dialogueShowItself.talked == false) //the game just start , and havent ans phone call 
        {
            gameStart = true;
            gameOpen.enabled = true;
        }


    }
}
