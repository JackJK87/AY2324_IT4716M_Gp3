using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 10f;
    public GameObject gameover;

    Text text;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        text = GetComponent<Text>();
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime -= Time.deltaTime;
            text.text = Mathf.Ceil(currentTime).ToString();
        }
        else
        {
            gameover.SetActive(true);
            text.enabled = false;
        }
    }
}
