using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreedScroll : MonoBehaviour
{
    public float scrollY = 0.5f;
    private Renderer rn;
    private float temp;

    private void Start()
    {
        rn = GetComponent<Renderer>();
        temp = rn.material.mainTextureOffset.x;
    }

    private void Update()
    {
        float OffsetY = Time.time * scrollY;
        rn.material.mainTextureOffset = new Vector2(temp, OffsetY);
    }
}
