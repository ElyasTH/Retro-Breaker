using UnityEngine;
using TMPro;
public class breakCollision : MonoBehaviour
{
    public GameHandler gameHandler;
    public GameObject wallParticle;
    public GameObject dustParticle;

    void OnCollisionEnter(Collision col)
    {
        Vector3 startingPos = transform.position;
        if (col.gameObject.tag == "Block")
        {
            col.gameObject.GetComponent<cubeMovement>().ChangeLife();
            Instantiate(dustParticle, gameObject.transform.position, Quaternion.identity);
        }
        else if (col.gameObject.tag == "Wall")
        {
            Instantiate(wallParticle, gameObject.transform.position, Quaternion.identity);
        }
    }
}
