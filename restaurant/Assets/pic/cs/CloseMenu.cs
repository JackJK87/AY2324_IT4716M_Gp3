using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] GameObject quit;

    public void menuCancel()
    {
        quit.SetActive(false);
    }
}
