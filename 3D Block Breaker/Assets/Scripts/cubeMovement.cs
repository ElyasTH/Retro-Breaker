using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    public int lives = 0;
    public int startlives = 0;
    public int score;
    public Transform tr;
    private float speed = 1.5f;
    public Material FourLives;
    public Material ThreeLives;
    public Material TwoLives;
    public Material oneLife;
    public Material fireBall;
    public Material lockAndLaunch;
    public Material lifeBlock;
    public Material ballInc;
    public GameObject fireParticle;
    public GameObject healParticle;
    public GameObject ballIncreaseParticle;
    public GameObject lockParticle;
    [SerializeField]
    private static float destroyerDelay = 150;
    private static bool isDestroying = false;
    bool isLifeBlock, isBallIncrease, isFireBall, isLaunch;


    public GameObject destroyParticle;
    private GameObject gameHandler;


    private int diffculty = 0;

    void Start()
    {
        gameHandler = GameObject.Find("GameHandler");
        gameHandler.GetComponent<GameHandler>().blockCount += 1;
        diffculty = gameHandler.GetComponent<GameHandler>().getlevel() + 3;
        if (diffculty > 5)
            diffculty -= 2;
        if (diffculty/3 >= 1000)
            diffculty = 1000;

        int Special = Random.Range(1, 90);
        if (Special > 3 && Special < 9)
        {
            isFireBall = true;
            GetComponent<Renderer>().material = fireBall;
            GameObject fire = Instantiate(fireParticle, transform.position, Quaternion.identity);
            fire.transform.rotation = new Quaternion(-90,0,0,90);
            fire.transform.parent = transform;
        }
        else if (Special > 10 && Special < 14)
        {
            isLaunch = true;
            GetComponent<Renderer>().material = lockAndLaunch;
            GameObject lockLaunch = Instantiate(lockParticle, transform.position, Quaternion.identity);
            lockLaunch.transform.rotation = new Quaternion(-90,0,0,90);
            lockLaunch.transform.parent = transform;
        }
        else if (Special == 2)
        {
            isLifeBlock = true;
            GetComponent<Renderer>().material = lifeBlock;
            GameObject heal = Instantiate(healParticle, transform.position, Quaternion.identity);
            heal.transform.rotation = new Quaternion(-90,0,0,90);
            heal.transform.parent = transform;
        }
        else if (Special > 15 && Special < 20)
        {
            isBallIncrease = true;
            GetComponent<Renderer>().material = ballInc;
            GameObject ballIncrease = Instantiate(ballIncreaseParticle, transform.position, Quaternion.identity);
            ballIncrease.transform.rotation = new Quaternion(-90,0,0,90);
            ballIncrease.transform.parent = transform;
        }


        if (isLifeBlock || isBallIncrease || isFireBall || isLaunch
)
            lives = 1;
        else
        {
            lives = Random.Range(1, 1000);
            if (lives < 30 + diffculty && lives > 0)
                lives = score = 4;
            else if (lives >= 30 + diffculty && lives < 100 + diffculty / 2 + diffculty)
                lives = score = 3;
            else if (lives >= 100 + +diffculty + diffculty /2 && lives < 350 + diffculty + diffculty /2 + diffculty / 3)
                lives = score = 2;
            else if (lives >= 350 + diffculty / 3 + diffculty /2 + diffculty && lives <= 1000)
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
        startlives = lives;
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

        if (transform.position.z < -9 && !isDestroying){
            isDestroying = true;
            BlockDestroyerScript.init();
            gameHandler.GetComponent<GameHandler>().getDamaged();
            ChangeLife(true);
        }

        if (isDestroying){
            destroyerDelay -= Time.deltaTime;
            if (destroyerDelay <= 0){
                isDestroying = false;
                destroyerDelay = 10;
            }
        }
    }

    public void ChangeLife(bool isDestroyer) 
    {
        if (!isDestroyer){
            gameHandler.GetComponent<GameHandler>().addXP(Random.Range(1, 17) * startlives * diffculty / 5 * (PlayerPrefs.GetInt("level") + 1) * 10);
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
                    Instantiate(GameObject.FindGameObjectWithTag("Ball"), transform.position, Quaternion.identity);
                    gameHandler.GetComponent<GameHandler>().addBall();
                }
                else if (isLaunch){
                    BallMovement.lockAndLaunch = true;
                    gameHandler.GetComponent<GameHandler>().PlaySound(gameHandler.GetComponent<GameHandler>().powerUp);
                }
                gameHandler.GetComponent<GameHandler>().addCombo();
                gameHandler.GetComponent<GameHandler>().addXP(Random.Range(23,57) * startlives * diffculty/5);
                gameHandler.GetComponent<GameHandler>().StartCoroutine(gameHandler.GetComponent<GameHandler>().Shake(0.1f, 0.025f));
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else{
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
