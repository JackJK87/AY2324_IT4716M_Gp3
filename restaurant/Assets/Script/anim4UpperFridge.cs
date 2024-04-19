using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim4UpperFridge : MonoBehaviour
{
    public Animator anim;
    public static bool animPlay = false;
    [SerializeField] private LayerMask UpperFridge;
    [SerializeField] private Transform cam;
    private float hitRange4Fridge = 2;
    private RaycastHit hit4Fridge;    // Start is called before the first frame update


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(cam.position, cam.forward, out hit4Fridge, hitRange4Fridge, UpperFridge)&&(playerMovement.talked == true))
        {
            if (animPlay == false)
            {
                Debug.Log("fridgeUpperOpen");
                animPlay = true;
                anim.Play("fridgeUpperOpen");
            }
            else
            {
                Debug.Log("fridgeUpperClosed");
                animPlay = false;
                anim.Play("fridgeUpperClosed");
            }
        }
    }

}
