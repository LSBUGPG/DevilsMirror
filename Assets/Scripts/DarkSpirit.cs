using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSpirit : MonoBehaviour {
    MirrorFlag mirrorflag;
    Collider coll;
    ParticleSystem ps;
    Behaviour halo;
    // Use this for initialization
    void Start () {
        mirrorflag = GameObject.FindGameObjectWithTag("MirrorTag").GetComponent<MirrorFlag>();
        coll = GetComponent<Collider>();
        coll.enabled = false;
        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        var emission = ps.emission;
        if (mirrorflag.mirroruse == true)
        {
            coll.enabled = true;
            emission.enabled = true;
            halo.enabled = true;
        }
        else
        {
            coll.enabled = false;
            emission.enabled = false;
            halo.enabled = false;
        }
    }
}
