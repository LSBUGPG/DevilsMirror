using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    AudioSource audsource;
    public AudioClip collectlight;
    public AudioClip collectdark;


	// Use this for initialization
	void Start () {

        audsource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(AudioClip snd)
    {
        audsource.clip = snd;
        audsource.Play();
    }
}
