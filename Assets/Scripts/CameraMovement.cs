using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    Camera cameraRef;
    float nextFloorHeight;
    private void Start()
    {
        cameraRef = GetComponent<Camera>();
        nextFloorHeight = GameManager.instance.mapElementRadius;
    }

    bool flag = false;
    private void Update()
    {
        transform.position += (Vector3)(Vector2.up * cameraSpeed * Time.deltaTime);
        if (transform.position.y - cameraRef.orthographicSize > nextFloorHeight)
        {
            GameManager.instance.IncreaceFloorNumber();
            nextFloorHeight = (GameManager.instance.floorNumber) * GameManager.instance.mapElementRadius;
        }

        if (FindObjectOfType<CharecterMovement>())
        {
            if (FindObjectOfType<CharecterMovement>().transform.position.y + FindObjectOfType<CharecterMovement>().charecterHeight / 2 + 0.5f < transform.position.y - cameraRef.orthographicSize && !flag)
            {
                flag = true;
                GameManager.instance.Lose();
            }
        }
    }
}
