using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class breakCollision : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject wallParticle;
    public GameObject dustParticle;
    public UnityEvent onCollisionEvent;

    void OnCollisionEnter(Collision col)
    {
        Vector3 startingPos = transform.position;
        if (col.gameObject.tag == "Block")
        {
            col.gameObject.GetComponent<cubeMovement>().ChangeLife();
            Instantiate(dustParticle, gameObject.transform.position, Quaternion.identity);
            gameHandler.GetComponent<GameHandler>().addXP(Random.Range(13, 27));
            gameHandler.GetComponent<GameHandler>().blockCount -= 1;
        }
        else if (col.gameObject.tag == "Wall")
        {
            gameHandler.GetComponent<GameHandler>().StartCoroutine(gameHandler.GetComponent<GameHandler>().Shake(0.1f, 0.04f));
            Instantiate(wallParticle, gameObject.transform.position, Quaternion.identity);
        }
        else if (col.gameObject.tag == "Player"){
            gameHandler.resetCombo();
        }
        onCollisionEvent?.Invoke();
    }
}
