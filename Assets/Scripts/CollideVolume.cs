using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideVolume : MonoBehaviour
{
    // Start is called before the first frame update
    Collider collider1, collider2;
    Vector3 direction;
    float distance;

    private void Awake()
    {
        collider1 = this.GetComponent<Collider>();
    }
    private void OnTriggerStay(Collider other)
    {
        collider2 = other;
        if (Physics.ComputePenetration(collider1, collider1.transform.position, collider1.transform.rotation,
collider2, collider2.transform.position, collider2.transform.rotation,
out direction, out distance))
        {
            // 这里可以计算出碰撞体的重叠面积
            float overlapVolume = collider1.bounds.Intersects(collider2.bounds) ? distance : 0f;
            Debug.Log(overlapVolume);
        }
        
    }
}
