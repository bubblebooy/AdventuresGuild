using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float zoomSpeed = 10f;
    public float panBorderThickness = 10f;
    private float zoom;
    public Vector2 panLimit;

    private void Start()
    {
        zoom = Camera.main.orthographicSize;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        float scrollHorizontal = Input.GetAxis("Horizontal");
        float scrollVertical = Input.GetAxis("Vertical");
        float scrollZoom = Input.GetAxis("Mouse ScrollWheel");

        pos.y += scrollVertical * panSpeed * Time.deltaTime;
        pos.x += scrollHorizontal * panSpeed * Time.deltaTime;
        zoom -= scrollZoom * zoomSpeed * 100f * Time.deltaTime;

        //if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))// || Input.mousePosition.y >= Screen.height - panBorderThickness)
        //{
        //    pos.y += panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))// || Input.mousePosition.y <= panBorderThickness)
        //{
        //    pos.y -= panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))// || Input.mousePosition.x >= Screen.width - panBorderThickness)
        //{
        //    pos.x += panSpeed * Time.deltaTime;
        //}
        //if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))// || Input.mousePosition.x <= panBorderThickness)
        //{
        //    pos.x -= panSpeed * Time.deltaTime;
        //}
        //if(Input.mouseScrollDelta.y > 0)
        //{
        //    Camera.main.orthographicSize -= zoomSpeed;
        //}
        //if (Input.mouseScrollDelta.y < 0)
        //{
        //    Camera.main.orthographicSize += zoomSpeed;
        //}

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
        zoom = Mathf.Clamp(zoom, 3, 20);
        Camera.main.orthographicSize = zoom;

        transform.position = pos;
    }
}
