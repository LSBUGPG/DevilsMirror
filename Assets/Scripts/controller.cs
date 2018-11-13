using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public int currentSpeed;
    public int walkSpeed = 10;
    public int sprintSpeed = 18;
    public int rotationSpeed = 250;
    bool sprint;
    [Header("Misc. Variables")]
    public PlayerStats playerstats;
    public AudioClip collectlight;
    public AudioClip collectdark;
    AudioSource source;
    public Sound sound;
    bool onGround;

    void Start()
    {
        source = GetComponent<AudioSource>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translationZ = Input.GetAxis("Vertical") * currentSpeed * 0.67f;
        float translationX = Input.GetAxis("Horizontal") * currentSpeed;
        float rotation = Input.GetAxis("Mouse X") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(translationX, 0, translationZ);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        //create a raycast for the jumping animation
        RaycastHit hit;
        Vector3 physicsCentre = this.transform.position + this.GetComponent<BoxCollider>().center;
        Debug.DrawRay(physicsCentre, Vector3.down*1.2f, Color.red, 1);
        if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.2f))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                onGround = true;
            }
        }
        else
        {
            onGround = false;
        }

        if (Input.GetKeyDown("space"))
        {
            if (onGround==true)
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * 350);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (playerstats.currentstamina > 0)
            {
                sprint = true;
            }
            else
            {
                sprint = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;

        }
        if (sprint)
        {
            currentSpeed = sprintSpeed;
            playerstats.currentstamina -= 0.5f;
        }
        if (!sprint)
        {
            currentSpeed = walkSpeed;
            playerstats.currentstamina += 0.1f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // AudioSource spirit;
        if (other.gameObject.CompareTag("SpiritLight"))
        {
            playerstats.lightspiritcount += 1;
            playerstats.currentmirror += 10;
            sound.PlaySound(sound.collectlight);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("SpiritDark"))
        {
            playerstats.darkspiritcount += 1;
            playerstats.currentmirror += 10;
            sound.PlaySound(sound.collectdark);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.gameObject.transform.localEulerAngles = new Vector3(0,90,0);
            }
            
        }
    }
}