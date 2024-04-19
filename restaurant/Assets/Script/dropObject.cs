using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropObject : MonoBehaviour
{
    GameObject obj;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Drop");
            rb.isKinematic = false;
            rb.WakeUp();
        }
    }




}    