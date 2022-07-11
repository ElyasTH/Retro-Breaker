using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMove : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, -speed * Time.deltaTime);
        if (transform.position.z < -6f)
            Destroy(this.gameObject);
    }
}
