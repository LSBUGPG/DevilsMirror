using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorIcon : MonoBehaviour {

    public MirrorFlag mirrorflag;
    public Sprite lightworld;
    public Sprite darkworld;
    Image ld;
	// Use this for initialization
	void Start () {
        ld = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (mirrorflag.mirroruse == true)
        {
            ld.sprite = darkworld;
        }
        else
        {
            ld.sprite = lightworld;
        }
	}
}
