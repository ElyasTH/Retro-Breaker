using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    public GameObject impactParticle;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.02f, gameObject.transform.position.z+10f * Time.deltaTime);
    }
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Block" || col.gameObject.tag == "Wall"){
            Instantiate(impactParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (col.gameObject.tag == "Block"){
                col.gameObject.GetComponent<cubeMovement>().ChangeLife(false);
            } 
        }
    }
}
