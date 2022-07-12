using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public TextMeshProUGUI LifeText;
    public int lifeCount = 3;
    [HideInInspector]
    public int ballCount = 1;
    public GameObject deathEffect;
    public GameObject player;
    public GameObject ball;
    public AudioSource music;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float health = 1f;
    public GameObject fireball;

    //soundEffects
    public AudioSource powerUp;

    //UI
    public Slider slider;


    public void loseLife(){
        lifeCount--;
        LifeText.text = lifeCount.ToString();

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


    //sounds
    public void PlaySound(AudioSource ad) 
    {
        ad.Play();
    }

}
