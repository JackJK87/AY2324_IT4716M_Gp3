using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject pickObj;
    [SerializeField] private Transform parent;
    public playerMovement script;

    void Start()
    {

    }

    void Update()
    {
        script = GetComponent<playerMovement>();
    }
}
