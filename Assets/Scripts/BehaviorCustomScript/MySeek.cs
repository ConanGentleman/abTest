using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这是一个自定义的行为树中的任务脚本
/// 功能是控制游戏物体到达目标位置（与seek类似）
/// </summary>
public class MySeek : Action
{
    public float speed = 0;//移动速度
    public SharedTransform target;//要到达的目标位置
    public float arriveDistance = 0.1f;//到达的距离（当距离相差0.1时，认为到达）
    private float sqrarriveDistance ;
    public override void OnStart()
    {
        sqrarriveDistance = arriveDistance * arriveDistance;
    }

    //当进入到这个任务的时候，会一直调用这个方法，一直到任务结束
    //当返回一个 成功或者失败的状态 那么任务结束
    //如果返回一个running的状态，那这个方法会继续被调用
    //OnUpdate方法的调用频率，默认是跟Unity里面的帧保持一致的
    public override TaskStatus OnUpdate()
    {
        if (target == null || target.Value==null) return TaskStatus.Failure;

        transform.LookAt(target.Value.position);//直接朝向目标位置
        //每一帧移动到的位置
        transform.position=Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);
        //如果 距离目标位置的距离比较小，认为到达了目标位置，直接return成功
        if ((target.Value.position - transform.position).sqrMagnitude < sqrarriveDistance)
            return TaskStatus.Success;
        else
            return TaskStatus.Running;
    }
}
