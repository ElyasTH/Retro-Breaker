using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxMovement : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1.2f;


    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
