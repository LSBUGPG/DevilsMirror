using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpirit : MonoBehaviour {
    MirrorFlag mirrorflag;
    Collider coll;
    ParticleSystem ps;
    Behaviour halo;
    // Use this for initialization
    void Start () {
        mirrorflag = GameObject.FindGameObjectWithTag("MirrorTag").GetComponent<MirrorFlag>();
        coll = GetComponent<Collider>();
        coll.enabled = true;
        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = true;
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        var emission = ps.emission;
        if (mirrorflag.mirroruse == true)
        {
            coll.enabled = false;
            emission.enabled = false;
            halo.enabled = false;
        }
        else
        {
            coll.enabled = true;
            emission.enabled = true;
            halo.enabled = true;
        }
    }


}
