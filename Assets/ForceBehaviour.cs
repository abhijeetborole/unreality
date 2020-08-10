using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBehaviour : MonoBehaviour {
    public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        GameObject explode = (GameObject)Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(this);
        Destroy(explode, 1f);
        if(collision.gameObject.tag=="Enemy")
            Debug.Log("EnemyHit");
    }
}
