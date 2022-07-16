using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyerScript : MonoBehaviour
{
    private static bool initDestruction = false;
    public GameHandler gameHandler;
    // Update is called once per frame
    void Update()
    {
        if (initDestruction){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+10*Time.deltaTime);
        }

        if (transform.position.z > -2.6){
            transform.position = new Vector3(transform.position.x, transform.position.y, -10.8f);
            initDestruction = false;
        }
    }

    public static void init(){
        initDestruction = true;
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Block" && gameHandler.lifeCount > 0){
            col.gameObject.GetComponent<cubeMovement>().ChangeLife(true);
        }
    }
}
