using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAvoider : Kinematic
{
    WallAvoidance myMoveType;

    void Start()
    {
        myMoveType = new WallAvoidance();
        myMoveType.character = this;
        myMoveType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}