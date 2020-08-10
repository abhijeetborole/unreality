using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SpawnDisc : MonoBehaviour
{
    public GameObject disc;
    public Transform player;
    public Transform spawnl;
    public Transform spawnr;
    private bool first;
    private GameObject spawnedL;
    private GameObject spawnedR;
    
    // Start is called before the first frame update
    void Start()
    {
        first = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            spawnedL = Instantiate(disc, spawnl.position, spawnl.rotation);
            spawnedR = Instantiate(disc, spawnr.position, spawnr.rotation);
            Debug.Log("Discs Spawned");
            first = false;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))   
        {
           spawnedL =  Instantiate(disc, spawnl.position, spawnl.rotation);//Interchange spawnl& spawnr for Left & Right Triggers if it's inverted
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            spawnedR = Instantiate(disc, spawnr.position, spawnr.rotation);
        }

            spawnedL.transform.position = spawnl.position;
            spawnedR.transform.position = spawnr.position;
        
    }
}
