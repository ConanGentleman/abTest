using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同行为树里已有的CanSeeOject的功能大致相似
/// 用于判断目标是否在视野内
/// </summary>
public class MyCanSeeObject : Conditional
{
    public Transform[] targets;//判断是否在视野内的目标
    public float fieldOfViewAngle = 90f;//视野范围
    public float viewDistance = 7;//视野距离

    //这个target是要传出给其它任务使用的，即找到视野范围内的敌人，并将此传递给如seek，让其跟踪
    //同时通过定义SharedTransform类型，可以使得该变量能够访问到行为树的变量
    public SharedTransform returnTarget;

    public override TaskStatus OnUpdate()
    {
        if (targets == null) return TaskStatus.Failure;
        foreach(var target in targets){
            float distance = (target.position - transform.position).magnitude;
            float angle = Vector3.Angle(transform.forward, target.position - transform.position);
            if (distance < viewDistance && angle < fieldOfViewAngle)
            {
                this.returnTarget.Value = target;
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
