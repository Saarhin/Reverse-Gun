using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    float somenumber = 0.008f;

    private void Update()
    {
        if (transform.localScale.x > 0)
            transform.localScale -= new Vector3(somenumber, 0, 0);
    }
}
