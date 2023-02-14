using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : MonoBehaviour
{
    public float radius = 1.0f;
    public float strength = 1.0f;
    public List<Transform> targets;

    void Update()
    {
        Vector3 steeringForce = Vector3.zero;
        int count = 0;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < radius)
            {
                Vector3 difference = transform.position - target.position;
                difference.Normalize();

                difference /= distance;

                steeringForce += difference;
                count++;
            }
        }

        if (count > 0)
        {
            steeringForce /= count;
        }

        steeringForce *= strength;

        GetComponent<Rigidbody>().velocity += steeringForce;
    }
}
