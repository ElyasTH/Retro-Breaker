using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 15;
    public Transform player;
    public float max_acceleration = 1;
    public float current_acceleration = 0;
    public GameObject launchButton;
    public GameObject powerUpLight;
    public Transform lockPosition;
    [SerializeField] private bool lockAndLaunch = false;
    private float lockAndLaunchTime = 5f;
    [SerializeField] private List<BallMovement> locked_balls;

    void Start(){
        locked_balls = new List<BallMovement>();
    }

    void Update(){
        if (lockAndLaunch){
            lockAndLaunchTime -= Time.deltaTime;
            if (lockAndLaunchTime <= 0){
                SetLockAndLaunch(false);
                lockAndLaunchTime = 5f;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a") && current_acceleration >= -max_acceleration)
        {
            current_acceleration -= 0.05f;
        }

        if (Input.GetKey("d") && current_acceleration <= max_acceleration)
        {
            current_acceleration += 0.05f;
        }

        else if (current_acceleration > 0) current_acceleration -= 0.05f;
        else if (current_acceleration < 0 && !Input.GetKey("a")) current_acceleration += 0.05f;
        if (current_acceleration > -0.05 && current_acceleration < 0.05) current_acceleration = 0;

        if ((current_acceleration < 0 && player.position.x < -6.7) || (current_acceleration > 0 && player.position.x > 5.6))
        {
            current_acceleration = 0;
        }
        player.position = new Vector3(player.position.x + current_acceleration * speed * Time.deltaTime, player.position.y, player.position.z);
    }

    public void LaunchBall(){
        if (locked_balls.Count > 0){
            locked_balls[0].Launch();
            locked_balls.Remove(locked_balls[0]);
        }

        if (locked_balls.Count > 0) launchButton.SetActive(true);
        else launchButton.SetActive(false); 
    }

    public void LockBall(BallMovement ball){
        if (!locked_balls.Contains(ball))
            locked_balls.Add(ball);

        ball.transform.position = new Vector3(lockPosition.position.x, lockPosition.position.y, lockPosition.position.z);

        if (locked_balls.Count > 0) launchButton.SetActive(true);
        else launchButton.SetActive(false); 
    }

    public void SetLockAndLaunch(bool value){
        lockAndLaunch = value;
        powerUpLight.SetActive(value);
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "Ball" && lockAndLaunch && !col.gameObject.GetComponent<BallMovement>().locked){
            col.gameObject.GetComponent<BallMovement>().Lock();
        }
    }
}
