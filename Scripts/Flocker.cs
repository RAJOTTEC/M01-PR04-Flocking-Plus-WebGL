using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public bool avoidObstacles = false;
    public GameObject myCohereTarget;
    BlendedSteering mySteering;
    PrioritySteering myAdvancedSteering;
    Kinematic[] Boids;

    void Start()
    {
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBoids = GameObject.FindGameObjectsWithTag("bird");
        Boids = new Kinematic[goBoids.Length - 1];
        int j = 0;
        for (int i = 0; i < goBoids.Length - 1; i++)
        {
            if (goBoids[i] == this)
            {
                continue;
            }
            Boids[j++] = goBoids[i].GetComponent<Kinematic>();
        }
        separate.targets = Boids;

        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];
        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 5f;

        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = cohere;
        mySteering.behaviors[1].weight = 10f;

        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;

        WallAvoidance myAvoid = new WallAvoidance();
        myAvoid.character = this;
        myAvoid.target = myCohereTarget;
        myAvoid.flee = true;
        BlendedSteering myHighPrioritySteering = new BlendedSteering();
        myHighPrioritySteering.behaviors = new BehaviorAndWeight[1];
        myHighPrioritySteering.behaviors[0] = new BehaviorAndWeight();
        myHighPrioritySteering.behaviors[0].behavior = myAvoid;
        myHighPrioritySteering.behaviors[0].weight = 1f;

        myAdvancedSteering = new PrioritySteering();
        myAdvancedSteering.groups = new BlendedSteering[2];
        myAdvancedSteering.groups[0] = new BlendedSteering();
        myAdvancedSteering.groups[0] = myHighPrioritySteering;

        myAdvancedSteering.groups[1] = new BlendedSteering();
        myAdvancedSteering.groups[1] = mySteering;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (!avoidObstacles)
        {
            steeringUpdate = mySteering.getSteering();
        }
        else
        {
            steeringUpdate = myAdvancedSteering.getSteering();
        }
        base.Update();
    }
}