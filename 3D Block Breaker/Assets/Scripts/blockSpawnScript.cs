using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSpawnScript : MonoBehaviour
{


    public float maxTime = 1f;
    [SerializeField]
    private float startX;

    private float timer = 0.0f;
    public GameObject brickLayout;

    public GameObject layout1;
    public GameObject layout2;
    public GameObject layout3;
    public GameObject layout4;
    public GameObject layout5;

    private void Awake()
    {
        Vector3 randSpawnPos = new Vector3(startX, 0, 0.2f);
        Instantiate(brickLayout, randSpawnPos, Quaternion.identity);
    }
    private void Update()
    {
        if (timer > maxTime)
        {
            int x = Random.Range(1, 5);
            Vector3 randSpawnPos = new Vector3(-4.75f, 1f, 7.3f);
            switch (x) 
            {
                case 1:
                    Instantiate(layout1, randSpawnPos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(layout2, randSpawnPos, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(layout3, randSpawnPos, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(layout4, randSpawnPos, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(layout5, randSpawnPos, Quaternion.identity);
                    break;
            }

            timer = 0;
        }

        timer += Time.deltaTime;

    }


}


