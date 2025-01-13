using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePercetion : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        //get all colliders inside sphere
        var colliders = Physics.OverlapSphere(transform.position, maxDist);
        foreach(var collider in colliders)
        {
            //do not include ouselves
            if(collider.gameObject == gameObject) continue;
            //check for matching tag
            if(tagName == "" || collider.tag == tagName)
            {
                //check if within max angle range
                Vector3 direction = collider.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle <= maxAngle)
                {
                    //add game object to result
                    result.Add(collider.gameObject);
                }
            }
        }

        return result.ToArray();
    }
}