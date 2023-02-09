using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class BallCollision : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject wallParticle;
    public GameObject dustParticle;
    public GameObject playerParticle;
    public AudioSource playerCollision;
    public UnityEvent onCollisionEvent;

    void OnCollisionEnter(Collision col)
    {
        if (gameObject.GetComponent<BallMovement>().locked) return;
        Vector3 startingPos = transform.position;
        if (col.gameObject.tag == "Block")
        {
            col.gameObject.GetComponent<BlockMovement>().ChangeLife(false);
            Instantiate(dustParticle, gameObject.transform.position, Quaternion.identity);
            gameHandler.GetComponent<GameHandler>().blockCount -= 1;
        }
        else if (col.gameObject.tag == "Wall")
        {
            gameHandler.GetComponent<GameHandler>().StartCoroutine(gameHandler.GetComponent<GameHandler>().Shake(0.1f, 0.04f));
            Instantiate(wallParticle, gameObject.transform.position, Quaternion.identity);
        }
        else if (col.gameObject.tag == "Player"){
            gameHandler.resetCombo();
            Instantiate(playerParticle, gameObject.transform.position, Quaternion.identity);
            playerCollision.Play();
        }
        onCollisionEvent?.Invoke();
    }
}
