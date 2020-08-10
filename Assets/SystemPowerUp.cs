using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SystemPowerUp : MonoBehaviour
{
    public AudioClip powerup;
    public AudioSource audio;
    public static float health = 100f;
  
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = powerup;  // Plays system power up sound for LevelTwo
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Debug.Log("You are Dead");//kill scene here
        }
    }
}
