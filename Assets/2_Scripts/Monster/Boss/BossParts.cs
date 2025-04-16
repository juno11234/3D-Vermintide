using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parts = Boss.Parts;

public class BossPartNode
{
    public Parts Part;
    public Collider Collider;
}


[System.Serializable]
public class BossParts
{
    public Collider bodyColl;
    public Collider leftArmColl;
    public Collider rightArmColl;
    public Collider leftLegColl;
    public Collider rightLegColl;

    public BossPartNode[] BossNodeArray;

    public void Initialize()
    {
        Collider[] colliders = { bodyColl, leftArmColl, rightArmColl, leftLegColl, rightLegColl };

        Parts[] parts = { Parts.Body, Parts.LeftArm, Parts.RightArm, Parts.LeftLeg, Parts.RightLeg };
        
        BossNodeArray = new BossPartNode[colliders.Length];

        for (int i = 0; i < BossNodeArray.Length; i++)
        {
            BossNodeArray[i] = new BossPartNode();
            BossNodeArray[i].Part = parts[i];
            BossNodeArray[i].Collider = colliders[i];
        }
    }

    public Parts GetPart(Collider collider)
    {
        for (int i = 0; i < BossNodeArray.Length; i++)
        {
            if (BossNodeArray[i].Collider == collider) 
                return BossNodeArray[i].Part;
        }

        return Parts.Unknow;
    }
}