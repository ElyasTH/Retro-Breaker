using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public Transform PlayerrTransform;

    private Vector3 cmOffset;

    [Range(0.1f, 1f)]
    public float smoothness = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        cmOffset = transform.position - PlayerrTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = PlayerrTransform.position + cmOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothness*Time.deltaTime/600f);
    }



}
