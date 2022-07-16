using UnityEngine;
using UnityEngine.Events;

public class BallMovement : MonoBehaviour
{

    public bool locked = true;
    public Transform lockPosition;
    public Rigidbody rb;
    public int startForce = 200;
    public GameObject player;
    public GameObject powerUpLight;
    public GameHandler gameHandler;
    private float lastZ = 0;
    public int checkZDelay = 0;

    public static bool lockAndLaunch = false;
    private float lockAndLaunchTime = 5;
    public UnityEvent onCollisionEvent;

    void Start(){
        rb.AddForce(0,0,startForce);
    }

    private void FixedUpdate()
    {
        if (!locked){
            if (rb.velocity.magnitude < 15) rb.AddForce(rb.velocity.normalized * Time.deltaTime * 100);

            if (Mathf.Abs(gameObject.transform.position.z-lastZ) < 0.1){
                checkZDelay++;
            }
            else {
                checkZDelay = 0;
            }
            if (checkZDelay > 10000*Time.deltaTime){
                rb.AddForce(0,0,-10);
                checkZDelay = 0;
            }
            lastZ = transform.position.z;

            if (transform.position.z < -13f)
            {
                checkZDelay = 0;
                if (gameHandler.ballCount == 1){
                    gameHandler.loseLife(this.gameObject);
                }
                else{
                    gameHandler.ballCount--;
                    Destroy(this.gameObject);
                }
            } 
        }
        else{
            gameObject.transform.position = lockPosition.position;
        }

        if ((Input.GetMouseButton(0) || Input.GetKeyUp(KeyCode.Space)) && locked){
            locked = false;
            rb.velocity = new Vector3(0,0,0);
            rb.AddForce(0, 0, startForce);
        }

        if (lockAndLaunch){
            lockAndLaunchTime -= Time.deltaTime;
            powerUpLight.SetActive(true);
            if (lockAndLaunchTime <= 0){
                lockAndLaunch = false;
                lockAndLaunchTime = 5;
                powerUpLight.SetActive(false);
            }
        }
    }

    public void reset(){
        transform.position = new Vector3(0, 0.9f,  -5.22f);
        locked = true;
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "Player" && lockAndLaunch) this.locked = true;
        onCollisionEvent?.Invoke();
    }
}
