using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Kinematic
{
    Seek myMoveType;
    Face mySeekRotateType;
    LookWhereGoing myFleeRotateType;

    public float radius = 1.0f;

    public bool flee = false;

    public Transform target;

    public float wanderRadius;
    public float wanderDistance;
    public float wanderJitter;

    private Vector3 targetPos;

    void Start()
    {
        myMoveType = new Seek();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = flee;

        mySeekRotateType = new Face();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;

        myFleeRotateType = new LookWhereGoing();
        myFleeRotateType.character = this;
        myFleeRotateType.target = myTarget;
    }

    protected override void Update()
    {
        targetPos = UpdateWanderTarget();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < radius)
        {
            steeringUpdate = new SteeringOutput();
            steeringUpdate.linear = myMoveType.getSteering().linear;
            steeringUpdate.angular = flee ? myFleeRotateType.getSteering().angular : mySeekRotateType.getSteering().angular;
        }
        base.Update();
    }

    Vector3 RandomWanderTarget()
    {
        float randomAngle = Random.Range(0f, 360f);

        Vector3 targetPos = transform.position + wanderDistance * new Vector3(Mathf.Sin(randomAngle), 0, Mathf.Cos(randomAngle));

        return targetPos;
    }

    Vector3 UpdateWanderTarget()
    {
        targetPos += new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);

        targetPos = targetPos.normalized * wanderRadius;

        return targetPos;
    }
}
