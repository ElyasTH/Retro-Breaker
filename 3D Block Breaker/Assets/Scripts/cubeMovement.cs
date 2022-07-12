using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    public int lives = 0;
    public int score;
    public Transform tr;
    private float speed = 0.24f;
    public Material FourLives;
    public Material ThreeLives;
    public Material TwoLives;
    public Material oneLife;
    public Material fireBall;
    public Material lockAndLaunch;
    public Material lifeBlock;
    public Material ballInc;
    bool isLifeBlock, isBallIncrease, isFireBall, isLaunch;
    public GameObject destroyParticle;
    private GameObject gameHandler;

    void Start()
    {
        gameHandler = GameObject.Find("GameHandler");

        int Special = Random.Range(1, 90);
        if (Special > 3 && Special < 9)
        {
            isFireBall = true;
            GetComponent<Renderer>().material = fireBall;
        }
        else if (Special > 10 && Special < 14)
        {
            isLaunch
     = true;
            GetComponent<Renderer>().material = lockAndLaunch;
        }
        else if (Special == 2)
        {
            isLifeBlock = true;
            GetComponent<Renderer>().material = lifeBlock;
        }
        else if (Special > 15 && Special < 20)
        {
            isBallIncrease = true;
            GetComponent<Renderer>().material = ballInc;
        }


        if (isLifeBlock || isBallIncrease || isFireBall || isLaunch
)
            lives = 1;
        else
        {
            lives = Random.Range(1, 1000);
            if (lives < 30 && lives > 0)
                lives = score = 4;
            else if (lives >= 30 && lives < 100)
                lives = score = 3;
            else if (lives >= 100 && lives < 350)
                lives = score = 2;
            else if (lives >= 350 && lives <= 1000)
                lives = score = 1;

            switch (lives)
            {
                case 1:
                    GetComponent<Renderer>().material = oneLife;
                    break;
                case 2:
                    GetComponent<Renderer>().material = TwoLives;
                    break;
                case 3:
                    GetComponent<Renderer>().material = ThreeLives;
                    break;
                case 4:
                    GetComponent<Renderer>().material = FourLives;
                    break;
            }
        }
    }
    void Update()
    {
        tr.position = new Vector3(tr.position.x , tr.position.y, tr.position.z - speed * Time.deltaTime);

        if (isLifeBlock || isBallIncrease || isFireBall || isLaunch
){}
            // Debug.Log(lives); 
        else
        switch (lives)
        {
            case 1:
                GetComponent<Renderer>().material = oneLife;
                break;
            case 2:
                GetComponent<Renderer>().material = TwoLives;
                break;
            case 3:
                GetComponent<Renderer>().material = ThreeLives;
                break;
            case 4:
                GetComponent<Renderer>().material = FourLives;
                break;
        }

        if (transform.position.z < -9.5){
            gameHandler.GetComponent<GameHandler>().getDamaged();
            BlockDestroyerScript.init();
            gameHandler.GetComponent<GameHandler>().loseLife();
            Destroy(this.gameObject);
        }
    }

    public void ChangeLife() 
    {
        lives--;
        if (lives <= 0){
            gameHandler.GetComponent<GameHandler>().addScore(score);
            if (isLifeBlock){
                gameHandler.GetComponent<GameHandler>().addLife();
            }
            else if (isFireBall){
                gameHandler.GetComponent<GameHandler>().fireballPowerUp();
            }
            else if (isBallIncrease){
                // Instantiate(gameHandler.GetComponent<GameHandler>().ball, transform.position, Quaternion.identity);
                // gameHandler.GetComponent<GameHandler>().addBall();
            }
            else if (isLaunch){
                BallMovement.lockAndLaunch = true;
            }

            Destroy(this.gameObject);
        }
    }

    void OnDestroy(){
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
    }
}
