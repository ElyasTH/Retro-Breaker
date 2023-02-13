using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float speed = 10.0f;
    private float acceleration = 0.0f;
    [SerializeField] float minX = 0, maxX = 0;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2 && transform.position.x < maxX)
            {
                acceleration = speed * Time.deltaTime;
                transform.position += new Vector3(acceleration, 0, 0);
            }
            else if (Input.mousePosition.x < Screen.width / 2 && transform.position.x > minX)
            {
                acceleration = -speed * Time.deltaTime;
                transform.position += new Vector3(acceleration, 0, 0);
            }
        }
    }
}
