using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        int children = transform.childCount;
        for(int i = 0; i < children; i++) {
            Transform t = transform.GetChild(i);
            t.gameObject.SetActive(i == index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
            int children = transform.childCount;

            index++;
            index = index % children;

            for(int i = 0; i < children; i++) {
                Transform t = transform.GetChild(i);
                t.gameObject.SetActive(i == index);
            }
        }
    }
}
