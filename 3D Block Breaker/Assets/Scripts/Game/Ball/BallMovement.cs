using UnityEngine;
using UnityEngine.Events;

public class BallMovement : MonoBehaviour
{

    public bool locked = true;
    public Rigidbody rb;
    public int startForce = 150;
    public Transform player;
    public GameHandler gameHandler;
    private float lastZ = 0;
    public float checkZDelay = 3f;
    public UnityEvent onCollisionEvent;
    public float minSpeed, maxSpeed;

    void Start(){
        rb.AddForce(0,0,startForce);
        transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        if (locked) Lock();
    }

    private void FixedUpdate()
    {
        if (!locked){
            if (rb.velocity.magnitude < minSpeed) rb.AddForce(rb.velocity.normalized * Time.deltaTime * 100);
            if (rb.velocity.magnitude > maxSpeed) rb.AddForce(rb.velocity.normalized * Time.deltaTime * -100);

            if (Mathf.Abs(gameObject.transform.position.z-lastZ) < 0.1){
                checkZDelay -= Time.deltaTime;
            }
            else {
                checkZDelay = 3f;
            }
            if (checkZDelay <= 0){
                rb.AddForce(0,0,-10);
                checkZDelay = 3f;
            }
            lastZ = transform.position.z;

            if (transform.position.z < -13f)
            {
                checkZDelay = 3f;
                if (gameHandler.ballCount <= 1){
                    gameHandler.loseLife(this.gameObject);
                }
                else{
                    gameHandler.ballCount--;
                    Destroy(this.gameObject);
                }
                gameHandler.resetCombo();
            } 
        }
        else{
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision col){
        onCollisionEvent?.Invoke();
    }

    public void Lock(){
        player.GetComponent<PlayerMovement>().LockBall(this);
        transform.SetParent(player);
        locked = true;
        transform.localScale = new Vector3(0.7f,0.7f,0.7f);
    }

    public void Launch(){
        if (locked){
            locked = false;
            transform.SetParent(null);
            rb.AddForce(0, 0, startForce);
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
    }
}
