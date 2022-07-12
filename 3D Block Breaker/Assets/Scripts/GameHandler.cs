using UnityEngine;
using TMPro;

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
    public GameObject fireball;
    private int combo = 0;
    public void loseLife(GameObject ball){
        lifeCount--;
        LifeText.text = lifeCount.ToString();

        Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        if (lifeCount == 0)
        {
            Time.timeScale = 0.2f;
            music.pitch = 0.6f;
            player.SetActive(false);
            Destroy(ball.gameObject);
            this.ball = GameObject.FindGameObjectsWithTag("Ball")[0];
        }
        else
        {
            ball.GetComponent<BallMovement>().reset();
            this.ball = ball;
        }
    }

    public void addLife(){
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
    }

    public void resetCombo(){
        combo = 0;
    }
}
