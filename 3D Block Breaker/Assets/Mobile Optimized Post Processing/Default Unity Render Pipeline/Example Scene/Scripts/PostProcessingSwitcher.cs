using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingSwitcher : MonoBehaviour
{
    private PostProcessor processor;
    
    // Start is called before the first frame update
    void Start()
    {
        processor = GetComponent<PostProcessor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            processor.enabled = !processor.enabled;
        }
    }
}
