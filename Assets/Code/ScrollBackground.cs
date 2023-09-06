using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float ScrollSpeed;

    void Update()
    {
        if(transform.position.x <= 0)
        {
            transform.position = Vector2.right * 5;
        }
        else
        {
            transform.position -= (Vector3)Vector2.right * ScrollSpeed * Time.deltaTime;
        }
    }
}
