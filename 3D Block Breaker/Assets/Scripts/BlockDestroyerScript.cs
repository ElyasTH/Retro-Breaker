using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyerScript : MonoBehaviour
{
    private static bool initDestruction = false;
    // Update is called once per frame
    void Update()
    {
        if (initDestruction){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+0.5f);
        }

        if (transform.position.z > -2.6){
            transform.position = new Vector3(transform.position.x, transform.position.y, -14);
            initDestruction = false;
        }
    }

    public static void init(){
        initDestruction = true;
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Block"){
            Destroy(col.gameObject);
        }
    }
}
