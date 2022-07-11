using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    public float scrollingSpeed = -0.1f;


    private MeshRenderer mr;
    private float xScrolling;


    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        Scroll();
    }


    void Scroll()
    {
        xScrolling = Time.time * scrollingSpeed;

        Vector2 offset = new Vector2(mr.sharedMaterial.mainTextureOffset.x, xScrolling);

        mr.sharedMaterial.SetTextureOffset("_MainTex", offset);

    }
}
