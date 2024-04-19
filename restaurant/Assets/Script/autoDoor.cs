using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDoor : MonoBehaviour
{

    public Transform player;
    public float distance = 2f;
    public Animator animator;
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    public static bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance <distance && isOpen == false && phoneCalling.called == false)
        {
            phoneCalling.startTheCall = true;
        }




        if (playerDistance<distance && isOpen == false && phoneCalling.called == true)
        {
            Open();
        }
        else if (playerDistance>distance && isOpen)
        {
            Close();
        }
    }

    void Open()
    {
        animator.Play("doorOpen");
        isOpen = true;
        audioSource.PlayOneShot(openSound);
    }

    void Close()
    {
        animator.Play("doorClose");
        isOpen = false;
        audioSource.PlayOneShot(closeSound);
    }
}
