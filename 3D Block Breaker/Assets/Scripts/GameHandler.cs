using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public int lifeCount = 3;
    public int blockCount = 0;
    [HideInInspector]
    public int ballCount = 1;
    private int score = 0;
    public float health = 1f;

    [Header("GameObjects")]
    public GameObject deathEffect;
    public GameObject player;
    public GameObject ball;
    public GameObject levelupText;
    public GameObject fireball;
    public GameObject cam;
    public GameObject xpCanM;
    public GameObject comboCanvas;
    public GameObject GameOverCan;

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
    private bool isSliderChanging;
    public float currentSliderValue = 1f;
    public Slider level;
    public int xp = 40;
    public int currentLevel = 1;
    public int maxXP = 1000;
    public int combo = 0;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI comboText;   

    private void Start()
    {
        int i = 1;
        if (PlayerPrefs.GetInt("level") > 1)
            while (i < PlayerPrefs.GetInt("level"))
            {
                maxXP *= 3;
                i++;
            }

        currentLevel = PlayerPrefs.GetInt("level");
        level.maxValue = maxXP;
        levelText.text = PlayerPrefs.GetInt("level").ToString();
    }

    void Update(){
        if (isSliderChanging) updateSlider();
    }

    public void loseLife(GameObject ball){
        lifeCount--;
        if (lifeCount > 0) health = 1f;
        else health = 0f;
        isSliderChanging = true;
        LifeText.text = lifeCount.ToString();
        resetCombo();
        StartCoroutine(Shake(0.2f, 0.2f));
        Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        if (lifeCount == 0)
        {
            PlayerPrefs.SetInt("level", currentLevel);
            if(PlayerPrefs.GetInt("highScore") < score)
            PlayerPrefs.SetInt("highScore", score);

            Time.timeScale = 0.2f;
            music.pitch = 0.6f;
            player.SetActive(false);
            Destroy(ball.gameObject);
            GameOverCan.SetActive(true);
            GameOverCan.transform.position = new Vector3(cam.transform.position.x, GameOverCan.transform.position.y, GameOverCan.transform.position.z);
            this.ball = GameObject.FindGameObjectsWithTag("Ball")[0];
        }
        else
        {
            ball.GetComponent<BallMovement>().reset();
            this.ball = ball;
        }
    }

    public void addLife(){
        PlaySound(powerUp);
        lifeCount++;
        LifeText.text = lifeCount.ToString();
        health = 1f;
        isSliderChanging = true;
    }

    public void addBall(){
        PlaySound(powerUp);
        ballCount++;
    }

    public void addScore(int score){
        this.score += score + (combo + 2) / 2 + PlayerPrefs.GetInt("level");
        scoreText.text = this.score.ToString();
    }

    public void addXP(int addxp) 
    {
        xp += addxp * (combo+1)/2;
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
            currentLevel++;
            maxXP *=3;
            level.maxValue = maxXP;
            level.value = 40;
            levelText.text = currentLevel.ToString();
            if (currentLevel % 2 == 0 || currentLevel == 1)
            {
                lifeCount++;
                LifeText.text = lifeCount.ToString();
            }
            PlaySound(levelUp);
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

    public void addCombo(){
        combo++;
        if (!comboText.IsActive() && combo >= 2) comboText.gameObject.SetActive(true);
        comboText.text = "Combo X" + combo;
        StartCoroutine(ShakeAny(comboCanvas, 0.1f, combo*0.02f));
        if (comboText.transform.localScale.x < 0.06)
            comboText.transform.localScale = new Vector3(comboText.transform.localScale.x+0.0005f, comboText.transform.localScale.y+0.0005f,comboText.transform.localScale.z);
    }

    public void resetCombo(){
        combo = 0;
        comboText.gameObject.SetActive(false);
        comboText.transform.localScale = new Vector3(0.02f, 0.02f, 1);
    }

    public void getDamaged() 
    {
        health -= 0.5f;
        isSliderChanging = true;
        resetCombo();
    }

    public void updateSlider(){
        if (currentSliderValue > health){
            currentSliderValue -= 0.02f;
            slider.value = currentSliderValue;
        }
        else if (currentSliderValue < health){
            currentSliderValue += 0.02f;
            slider.value = currentSliderValue;
        }
        else if (currentSliderValue <= 0)
        {
            loseLife(this.ball);
            isSliderChanging = false;
            if (lifeCount > 0) slider.value = health = currentSliderValue = 1f;
        }
        else isSliderChanging = false;

        if (Mathf.Abs(currentSliderValue - health) < 0.02f){
            slider.value = currentSliderValue = health;
            isSliderChanging = false;    
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


    //Buttons
    public void onRestart() 
    {
        SceneManager.LoadScene(1);
    }
    public void onMenu()
    {
        SceneManager.LoadScene(0);
    }

}
