using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmLight : MonoBehaviour {
    MirrorFlag mirrorflag;
    Light lt;
    ParticleSystem ps;
    // Use this for initialization
    void Start () {
        mirrorflag = GameObject.FindGameObjectWithTag("MirrorTag").GetComponent<MirrorFlag>();
        lt = GetComponent<Light>();
        ps = GetComponent<ParticleSystem>();
       

        if (ps)
        {
            var emission = ps.emission;
            emission.enabled = true;
            lt.enabled = true;

        }
    }
	
	// Update is called once per frame
	void Update () {

        if (ps)
        {
        var emission = ps.emission;
        lt.enabled = !mirrorflag.mirroruse;
        emission.enabled = !mirrorflag.mirroruse;

        }
    }
}
