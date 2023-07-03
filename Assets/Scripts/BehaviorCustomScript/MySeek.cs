using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����һ���Զ������Ϊ���е�����ű�
/// �����ǿ�����Ϸ���嵽��Ŀ��λ�ã���seek���ƣ�
/// </summary>
public class MySeek : Action
{
    public float speed = 0;//�ƶ��ٶ�
    public SharedTransform target;//Ҫ�����Ŀ��λ��
    public float arriveDistance = 0.1f;//����ľ��루���������0.1ʱ����Ϊ���
    private float sqrarriveDistance ;
    public override void OnStart()
    {
        sqrarriveDistance = arriveDistance * arriveDistance;
    }

    //�����뵽��������ʱ�򣬻�һֱ�������������һֱ���������
    //������һ�� �ɹ�����ʧ�ܵ�״̬ ��ô�������
    //�������һ��running��״̬����������������������
    //OnUpdate�����ĵ���Ƶ�ʣ�Ĭ���Ǹ�Unity�����֡����һ�µ�
    public override TaskStatus OnUpdate()
    {
        if (target == null || target.Value==null) return TaskStatus.Failure;

        transform.LookAt(target.Value.position);//ֱ�ӳ���Ŀ��λ��
        //ÿһ֡�ƶ�����λ��
        transform.position=Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);
        //��� ����Ŀ��λ�õľ���Ƚ�С����Ϊ������Ŀ��λ�ã�ֱ��return�ɹ�
        if ((target.Value.position - transform.position).sqrMagnitude < sqrarriveDistance)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
    }
}
