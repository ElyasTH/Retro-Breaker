using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private float acceleration = 0.0f;
    [SerializeField] float minX = 0, maxX = 0, minY = 0, maxY = 0;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2 && transform.position.x < maxX && Input.mousePosition.y > Screen.height / 6)
            {
                acceleration = speed * Time.deltaTime;
                transform.position += new Vector3(acceleration, 0, 0);
            }
            else if (Input.mousePosition.x < Screen.width / 2 && transform.position.x > minX && Input.mousePosition.y > Screen.height / 6)
            {
                acceleration = -speed * Time.deltaTime;
                transform.position += new Vector3(acceleration, 0, 0);
            }
        }
    }
}
