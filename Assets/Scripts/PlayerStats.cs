using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    float maxhealth = 100;
    public float currenthealth;
    float maxstamina = 100;
    public float currentstamina;
    float maxmirror = 100;
    public float currentmirror;
    public float lightspiritcount = 0;
    public float darkspiritcount = 0;
    float totalspiritcount;
    float lightmeter;
    float darkmeter;
    public Slider healthSlider;
    public Slider staminaSlider;
    public Slider mirrorSlider;
    public Slider lightspiritSlider;
    public Slider darkspiritSlider;
    float attack = 1;
    float defense = 1;
    float atkmod = 1;
    float defmod = 1;
    public AudioClip heartbeatslow;
    public AudioClip heartbeatfast;
    AudioSource playerAudio;

	// Use this for initialization
	void Start () {
        playerAudio = GetComponent<AudioSource> ();

        currenthealth = maxhealth;
        currentstamina = maxstamina;
        currentmirror = maxmirror;
	}
	
	// Update is called once per frame
	void Update () {

        healthSlider.value = currenthealth / maxhealth * 100;
        staminaSlider.value = currentstamina / maxstamina * 100;
        lightspiritSlider.value = lightmeter;
        darkspiritSlider.value = darkmeter;
        mirrorSlider.value = currentmirror;

        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
        if (currentmirror > maxmirror)
        {
            currentmirror = maxmirror;
        }
        if (currentmirror < 0)
        {
            currentmirror = 0;
        }
        if (currentstamina > maxstamina)
        {
            currentstamina = maxstamina;
        }
        if (currentstamina < 0)
        {
            currentstamina = 0;
        }

        totalspiritcount = lightspiritcount + darkspiritcount;
        if (totalspiritcount < 10)
        {
            lightmeter = lightspiritcount * 10;
            darkmeter = darkspiritcount * 10;
        }
        else
        {
            if (lightmeter > darkmeter)
            {
                lightmeter = 100;
                darkmeter = darkspiritcount / totalspiritcount * 200;
            }
            if (darkmeter > lightmeter)
            {
                lightmeter = lightspiritcount / totalspiritcount * 200;
                darkmeter = 100;
            }
            if (lightmeter == darkmeter)
            {
                lightmeter = 100;
                darkmeter = 100;
            }
            if (lightmeter < 50)
            {
                atkmod = 0.67f;
            }
            else
            {
                atkmod = 1;
            }
            if (darkmeter < 50)
            {
                defmod = 0.67f;
            }
            else
            {
                defmod = 1;
            }

        }
        if (totalspiritcount >= 20)
        {
            if (lightspiritcount >= darkspiritcount * 0.75f)
            {
                defense += 0.1f;
                maxhealth += 10;
            }
            if (darkspiritcount >= lightspiritcount * 0.75f)
            {
                attack += 0.1f;
                maxstamina += 10;
            }
            lightspiritcount = 0;
            darkspiritcount = 0;
        }
        if (currenthealth < maxhealth * 0.25f)
        {
            playerAudio.clip = heartbeatfast;
            playerAudio.Play ();
        }
	}

}
