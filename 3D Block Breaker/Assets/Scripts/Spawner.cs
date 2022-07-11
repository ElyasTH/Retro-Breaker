using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float maxTimeBigTree = 1f;
    public float maxTimeGrass = 1f;
    private float timer = 0.0f;
    private float timer2 = 0.0f;
    public GameObject bigTree;
    public GameObject grass;


    private void FixedUpdate()
    {
        float randPosX = Random.Range(-28f, 28f);
        while ((randPosX > -20f && randPosX < 20f) 
            || randPosX < -28f && randPosX > 28f) 
        {
         randPosX = Random.Range(-28f, 28f) + Random.Range(-5f,5f);
        }

        if (timer > maxTimeBigTree)
        {
            Vector3 randSpawnPos = new Vector3(randPosX, -11f, 37);
            Instantiate(bigTree, randSpawnPos, Quaternion.identity);

            timer = 0;
        }

                while ((randPosX > -20f && randPosX < 20f) 
            || randPosX < -28f && randPosX > 28f) 
        {
         randPosX = Random.Range(-28f, 28f) + Random.Range(-5f,5f);
        }

        if (timer2 > maxTimeGrass)
        {
            if (randPosX < 0 && randPosX - 2 > -28f)
                randPosX -= 2;
            else if(randPosX > 0 && randPosX + 2 < 28f)
                randPosX += 2;

            Vector3 randSpawnPos = new Vector3(randPosX, -11f, 35);
            Instantiate(grass, randSpawnPos, Quaternion.identity);

            timer2 = 0;
        }

        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
    }
}
