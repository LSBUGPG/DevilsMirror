using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorFlag : MonoBehaviour {
    public bool mirroruse = false;
    public PlayerStats playerstats;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (playerstats.currentmirror > 0)
            {
                playerstats.currentmirror -= 5;
                if (mirroruse == false)
                {
                    mirroruse = true;
                }
                else
                {
                    mirroruse = false;
                }
            }
            
        }

	}
}
