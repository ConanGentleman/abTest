using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͬ��Ϊ�������е�CanSeeOject�Ĺ��ܴ�������
/// �����ж�Ŀ���Ƿ�����Ұ��
/// </summary>
public class MyCanSeeObject : Conditional
{
    public Transform[] targets;//�ж��Ƿ�����Ұ�ڵ�Ŀ��
    public float fieldOfViewAngle = 90f;//��Ұ��Χ
    public float viewDistance = 7;//��Ұ����

    //���target��Ҫ��������������ʹ�õģ����ҵ���Ұ��Χ�ڵĵ��ˣ������˴��ݸ���seek���������
    //ͬʱͨ������SharedTransform���ͣ�����ʹ�øñ����ܹ����ʵ���Ϊ���ı���
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
