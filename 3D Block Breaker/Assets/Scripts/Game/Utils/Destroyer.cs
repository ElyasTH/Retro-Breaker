using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject parent;
    public void Destroy(){
        if (parent != null) Destroy(parent);
        Destroy(gameObject);
    }

}
