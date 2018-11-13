using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkObject : MonoBehaviour {
    MirrorFlag mirrorflag;
    Renderer rend;
    // Use this for initialization
    void Start () {
        mirrorflag = GameObject.FindGameObjectWithTag("MirrorTag").GetComponent<MirrorFlag>();
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (mirrorflag.mirroruse == true)
        {
            rend.enabled = true;
        }
        else
        {
            rend.enabled = false;
        }
    }
}
