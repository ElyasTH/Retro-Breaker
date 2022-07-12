using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public int lifeCount = 3;
    public int blockCount = 0;
    [HideInInspector]
    public int ballCount = 1;
    private int score = 0;
    private float health = 1f;

    [Header("GameObjects")]
    public GameObject deathEffect;
    public GameObject player;
    public GameObject ball;
    public GameObject levelupText;
    public GameObject fireball;
    public GameObject cam;
    public GameObject xpCanM;

    

    [Header("Audio")]
    public AudioSource music;
    public TextMeshProUGUI scoreText;
    public AudioSource powerUp;
    public AudioSource levelUp;
    public AudioSource coin1;
    public AudioSource coin2;
    public AudioSource coin3;

    [Header("UI")]
    public Slider slider;
    public Slider level;
    public int xp = 40;
    public int currentLevel = 1;
    public int maxXP = 1000;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI xpText;

    private void Start()
    {
        level.maxValue = maxXP;
    }

    public void loseLife(){
        lifeCount--;
        LifeText.text = lifeCount.ToString();
        StartCoroutine(Shake(0.2f, 0.2f));

        Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        if (lifeCount == 0)
        {
            Time.timeScale = 0.2f;
            music.pitch = 0.6f;
            player.SetActive(false);
            Destroy(ball.gameObject);
        }
        else
        {
            ball.GetComponent<BallMovement>().reset();
        }
    }

    public void addLife(){
        PlaySound(powerUp);
        lifeCount++;
        LifeText.text = lifeCount.ToString();
    }

    public void addBall(){
        ballCount++;
    }

    public void addScore(int score){
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    public void addXP(int addxp) 
    {
        xp += addxp;
        level.value = xp;

             xpText.text = "+" +  xp.ToString();
             StartCoroutine(ShakeAny(xpCanM,0.1f, 0.3f));
        int n = Random.Range(1, 4);
        switch (n) 
        {
            case 1:
                PlaySound(coin1);
                break;
            case 2:
                PlaySound(coin2);
                break;
            case 3:
                PlaySound(coin3);
                break;
        }


        if (level.value >= maxXP)
        {
            StartCoroutine(Shake(2f, 0.05f));
            levelupText.SetActive(false);
            levelupText.SetActive(true);
            lifeCount++;
            LifeText.text = lifeCount.ToString();
            PlaySound(levelUp);
            currentLevel++;
            maxXP *=3;
            level.maxValue = maxXP;
            level.value = 40;
            levelText.text = currentLevel.ToString();
            xp = 40;
        }
    }

    public void fireballPowerUp(){
        PlaySound(powerUp);
        Instantiate(fireball, new Vector3(-6, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(-4, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(-2, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(0, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(2, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(4, 1.02f, -13), Quaternion.identity);
        Instantiate(fireball, new Vector3(6, 1.02f, -13), Quaternion.identity);
    }


    public void getDamaged() 
    {
        health -= 0.5f;
        slider.value = health;
        if (health == 0)
        {
            loseLife();
            health = 1f;
        }
    }

    public int getlevel() { return currentLevel; }

    //cam shake
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 origin = cam.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(x, y, origin.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cam.transform.localPosition = origin;
    }

    //any obj shake
    public IEnumerator ShakeAny(GameObject obj,float duration, float magnitude)
    {
        Vector3 origin = obj.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            obj.transform.localPosition = new Vector3(x, origin.y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        obj.transform.localPosition = origin;
    }

    //sounds
    public void PlaySound(AudioSource adu) 
    {
        adu.Play();
    }


}
