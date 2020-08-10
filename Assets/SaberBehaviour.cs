using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberBehaviour : MonoBehaviour
{
    public AudioClip ignite;
    public AudioClip swing;
    public AudioClip clash;
    public AudioClip shut;
    public AudioSource audio;
    private Rigidbody rb;
    public Transform hand;// To reset Saber
    public GameObject spark;//Spark on Saber Contact
    public Vector3 colp;//Collision Point
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            this.transform.position = hand.position;  //Resets Saber Position
        }
        
        if (rb.velocity.magnitude > 10f)
        {
            if (!audio.isPlaying)
            {
                audio.clip = swing;//Play swinging sound
                audio.Play();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Hand"))
        {
            colp = collision.GetContact(0).point;
            GameObject colspark = Instantiate(spark, colp, transform.rotation); //Spark on saber contact
            Destroy(colspark, 1f);
            Destroy(spark,1f);
            if (!audio.isPlaying)
            {
                audio.clip = clash; //Play collision sound
                audio.Play();
            }
        }
        else
        {
            if (!audio.isPlaying)
            {
                if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) ||
                    OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) // Check collision with hand and Trigger Press to avoid unnecessary triggering of Saber Ignition Sound
                {
                    audio.clip = ignite; //Play ignition Sound. Make UI Changes here for Saber Equipped.
                    audio.Play();
                }                
            }
        }
    }
}
