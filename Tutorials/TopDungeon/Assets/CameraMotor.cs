using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform look_at;
    public float bound_x = 0.15f;
    public float bound_y = 0.05f;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float delta_x = look_at.position.x - transform.position.x;
        if(delta_x > bound_x)
        {
            delta.x = delta_x - bound_x;
        }
        else if(delta_x < -bound_x)
        {
            delta.x = delta_x + bound_x;
        }

        float delta_y = look_at.position.y - transform.position.y;
        if (delta_y > bound_y)
        {
            delta.y = delta_y - bound_y;
        }
        else if (delta_y < -bound_y)
        {
            delta.y = delta_y + bound_y;
        }

        transform.position += new Vector3(delta.x, delta.y);
    }
}
