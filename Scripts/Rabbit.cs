using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Kinematic
{
    PathFollow myMoveType;
    LookWhereGoing myRotateType;

    public GameObject[] Path = new GameObject[4];

    void Start()
    {
        myMoveType = new PathFollow();
        myMoveType.character = this;
        myMoveType.path = Path;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}