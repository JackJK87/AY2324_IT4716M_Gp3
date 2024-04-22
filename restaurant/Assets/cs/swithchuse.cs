using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swithchuse : MonoBehaviour
{
    public Material redswitch, whiteswitch;
    bool redB;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnMouseDown()
    {
        redB =!redB;
        if(redB)
        {
            this.GetComponent<Renderer>().material = whiteswitch;
        }
        else
        {
            this.GetComponent<Renderer>().material = redswitch;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
