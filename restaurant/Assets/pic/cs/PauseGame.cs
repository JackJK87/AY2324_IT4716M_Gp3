using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject menuCall;
    public void Pause() 
    {
        menuCall.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        menuCall.SetActive(false);
        Time.timeScale = 1f;
    }
}
