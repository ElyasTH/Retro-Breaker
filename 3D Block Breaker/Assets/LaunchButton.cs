using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour
{
    public Transform player;
    public int leftX, rightX;

    void Update(){
        if (player.position.x < -0.5) this.transform.position = new Vector3(leftX, transform.position.y, transform.position.z);
        else this.transform.position = new Vector3(rightX, transform.position.y, transform.position.z);
    }
}
