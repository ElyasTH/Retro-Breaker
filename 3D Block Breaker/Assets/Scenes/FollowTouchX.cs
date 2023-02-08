using UnityEngine;

public class FollowTouchX : MonoBehaviour
{
    [SerializeField] private float smoothness = 0.1f;  

    private bool isDragging;  

    [SerializeField] private float minX = -10f;  
    [SerializeField] private float maxX = 10f; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float enter;
            if (plane.Raycast(ray, out enter))
            {
                Vector3 targetPos = ray.GetPoint(enter);
                targetPos.y = transform.position.y;  
                targetPos.z = transform.position.z;  

                // Move the object towards the target position with smoothness
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothness);

                isDragging = true;  
            }
        }
        else if (Input.GetMouseButtonUp(0))  
        {
            isDragging = false;  
        }

        if (isDragging)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Cast the ray and get the point where it intersects with the plane
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float enter;
            if (plane.Raycast(ray, out enter))
            {
                Vector3 targetPos = ray.GetPoint(enter);
                targetPos.y = transform.position.y;  
                targetPos.z = transform.position.z;  

                // Move the object towards the target
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothness);
            }
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }
}

