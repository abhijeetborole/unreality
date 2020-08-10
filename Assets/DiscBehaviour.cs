using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public AudioClip woosh;
    public AudioSource audio;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 7.5f)
        {
            if (flag)
            {
                audio.clip = woosh; // Throwing Sound for the Tron Disc
                audio.Play();
                flag = false;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hand"))
        {
            Destroy(this.gameObject);
            flag = true;
        }
    }
}


