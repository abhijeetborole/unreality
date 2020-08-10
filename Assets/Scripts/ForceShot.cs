using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShot : MonoBehaviour {
    public float damage = 35f;
    public OVRInput.Controller con;
    public Vector3 ac_rh;
    public Vector3 velocity;
    public float accl;
    public Transform hand;
    public Transform dir;
    public GameObject spell;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = dir.position - hand.position;
        velocity = new Vector3(direction.x,direction.y,0f);
        accl = 0f;
        ac_rh = OVRInput.GetLocalControllerAcceleration(con);
        accl = ac_rh.magnitude;
        Debug.Log("" + accl);
        if (accl > 25f) {
            GameObject casted = (GameObject)Instantiate(spell, hand.position, hand.rotation);
            // casted.GetComponent<Rigidbody>().AddForce(casted.transform.forward * speed, ForceMode.Impulse);

            casted.GetComponent<Rigidbody>().velocity = velocity;
            Destroy(casted, 6f);
            RaycastHit hit;
            if (Physics.Raycast(hand.position, direction, out hit))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }

}
