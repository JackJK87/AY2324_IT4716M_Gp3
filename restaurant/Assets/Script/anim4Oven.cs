using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim4Oven : MonoBehaviour
{
    public Transform player;
    public float distance = 2f;
    public Animator animator;
    private AudioSource audioSource;
    public static bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance < distance && isOpen == false)
        {
            Open();
        }
        else if (playerDistance > distance && isOpen)
        {
            Close();
        }
    }

    void Open()
    {
        animator.Play("ovenOpen");
        isOpen = true;
    }

    void Close()
    {
        animator.Play("ovenClose");
        isOpen = false;
    }

}
