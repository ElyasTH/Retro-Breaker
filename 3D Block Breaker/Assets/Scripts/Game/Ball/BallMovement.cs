using UnityEngine;
using UnityEngine.Events;

public class BallMovement : MonoBehaviour
{

    public bool locked = true;
    public Rigidbody rb;
    public float startSpeed = 12;
    public Transform player;
    public GameHandler gameHandler;
    private float lastZ = 0;
    public float checkZDelay = 3f;
    public UnityEvent onCollisionEvent;
    [SerializeField] private float speed = 15;
    public float speedUpZ = -3.8f;
    public float speedUpMultiplier = 2f;
    private bool highZ = false;

    void Start(){
        rb.velocity = startSpeed * new Vector3(0,0,1);
        speed = startSpeed;
        if (locked) Lock();
    }

    private void FixedUpdate()
    {
        if (!locked){

            if (transform.position.z > speedUpZ && !highZ){
                highZ = true;
                speed *= speedUpMultiplier;
            }
            else if (transform.position.z < speedUpZ && highZ){
                highZ = false;
                speed /= speedUpMultiplier;
            }

            rb.velocity = speed * rb.velocity.normalized;

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
                if (GameObject.FindGameObjectsWithTag("Ball").Length <= 1){
                    gameHandler.loseLife(this.gameObject);
                }
                else{
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
        GetComponent<SphereCollider>().enabled = false;
    }

    public void Launch(){
        if (locked){
            locked = false;
            transform.SetParent(null);
            rb.velocity = startSpeed * new Vector3(0,0,1);
            speed = startSpeed;
            GetComponent<SphereCollider>().enabled = true;
        }
    }
}
