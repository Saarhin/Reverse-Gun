using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    // Start is called before the first frame update
    Color firstColor;



    void Start()
    {
        Cursor.visible = false;
        firstColor = cursor.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), FindObjectOfType<CharecterMovement>().transform.position);
        if (distance > GameManager.instance.reverseGunFiringRange)
        {
            cursor.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
            cursor.GetComponent<SpriteRenderer>().color = firstColor;


        cursor.transform.position = Vector2.right * Camera.main.ScreenToWorldPoint(Input.mousePosition).x + Vector2.up * Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }
}
