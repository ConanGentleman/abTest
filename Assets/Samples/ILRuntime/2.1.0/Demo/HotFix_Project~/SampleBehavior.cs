﻿//using SampleBehaviorTree;
using HotFix_Project;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static GameDefineHotFix;

public class SampleBehavior : HotFixMonoBehaviour
{
    //[ContextMenu("ExcuteBehavior")]
    //void ExcuteBehavior()
    //{
    //    UICtrl.instance.actionPanel.SetActive(false);
    //    StartCoroutine(c_ExcuteBehavior(this.root));
    //}

    //IEnumerator c_ExcuteBehavior(BehaviorNode n)
    //{
    //    yield return StartCoroutine(n.Start());
    //    Debug.Log("行为树流程结束");

    //    while (playerC.state != PlayerSate.wait) yield return null;
    //    while (EffectCtrl.instance.playeffect) yield return null;



    //    if (behaviorEnd != null) behaviorEnd.Invoke();

    //}
    //PlayerController playerC;
    //private BehaviorNode root;
    //private BehaviorNode root_Advanced_Attack;
    //private BehaviorNode root_Advanced_Auxiliary;

    //public System.Action behaviorEnd;
    //// Start is called before the first frame update
    //void Start()
    //{

    //    playerC = this.GetComponent_HotFix<PlayerController>();

    //    root = this.CreateBehavior();

    //    root_Advanced_Attack = CreateBehavior_Advanced_Attack();
    //    root_Advanced_Auxiliary = CreateBehavior_Advanced_Auxiliary();

    //    playerC.sampleBehavior = this;
    //}


    //private void OnDestroy()
    //{
    //    behaviorEnd = null;

    //}

    //[ContextMenu("ExcuteBehavior")]
    //void ExcuteBehavior()
    //{
    //    UICtrl.instance.actionPanel.SetActive(false);
    //    StartCoroutine(c_ExcuteBehavior(this.root));
    //}

    //IEnumerator c_ExcuteBehavior(BehaviorNode n)
    //{
    //    yield return StartCoroutine(n.Start());
    //    Debug.Log("行为树流程结束");

    //    while (playerC.state != PlayerSate.wait) yield return null;
    //    while (EffectCtrl.instance.playeffect) yield return null;



    //    if (behaviorEnd != null) behaviorEnd.Invoke();

    //}




    ////创建行为树
    //BehaviorNode CreateBehavior()
    //{
    //    var selector = new Selector();

    //    var sequence_1 = new Sequence();

    //    var condition = new InActiveSkillRange();
    //    var action1 = new UseSkill();

    //    sequence_1.nodes.Add(condition);
    //    sequence_1.nodes.Add(action1);
    //    //设计图中左节点
    //    selector.nodes.Add(sequence_1);

    //    //中间节点
    //    var sequence_2 = new Sequence();
    //    var move = new MoveToTarget();
    //    sequence_2.nodes.Add(move);
    //    //重用节点优化内存,节点执行前要重置属性
    //    sequence_2.nodes.Add(sequence_1);

    //    selector.nodes.Add(sequence_2);


    //    //最后一个节点
    //    var sequence_3 = new Sequence();
    //    var wait = new Wait();
    //    sequence_3.nodes.Add(wait);

    //    selector.nodes.Add(sequence_3);



    //    //参数配置
    //    wait.playerC = this.playerC;
    //    move.playerC = this.playerC;
    //    condition.playerC = this.playerC;
    //    action1.inActiveSkillRange = condition;

    //    //核心思想,通过插入顺序决定流程控制
    //    //组合一
    //    // selector.nodes.Add(wait);
    //    // selector.nodes.Add(sequence_3);
    //    // selector.nodes.Add(sequence_1);

    //    //组合二
    //    // selector.nodes.Add(sequence_3);
    //    // selector.nodes.Add(wait);
    //    // selector.nodes.Add(sequence_1);

    //    return selector;



    //}


    //[ContextMenu("ExcuteAI01")]
    //void ExcuteAI01()
    //{
    //    UICtrl.instance.actionPanel.SetActive(false);
    //    StartCoroutine(c_Execute());
    //}

    //IEnumerator 行为树思想()
    //{
    //    if (主动技能范围内有对象吗())
    //    {
    //        释放技能();

    //    }
    //    else if (移动到最近的敌人())
    //    {
    //        if (主动技能范围内有对象吗())
    //        {
    //            释放技能();
    //        }
    //        else
    //        {
    //            待机();
    //        }

    //    }
    //    else
    //    {
    //        待机();
    //    }

    //    yield return 1;

    //}
    //IEnumerator c_Execute()
    //{

    //    var inActiveSkillRange = new InActiveSkillRange();
    //    inActiveSkillRange.playerC = playerC;

    //    yield return StartCoroutine(inActiveSkillRange.Execute());


    //    //if (主动技能范围内有对象吗())
    //    if (inActiveSkillRange.state == SampleBehaviorTree.State.Succeed)
    //    {
    //        //释放技能();
    //        // Debug.Log("释放技能");
    //        var useSkill = new UseSkill();
    //        useSkill.inActiveSkillRange = inActiveSkillRange;
    //        yield return StartCoroutine(useSkill.Execute());
    //    }
    //    else// if (移动到最近的敌人())
    //    {
    //        Debug.Log("MoveToTarget");
    //        var moveToTarget = new MoveToTarget();
    //        moveToTarget.playerC = playerC;

    //        yield return StartCoroutine(moveToTarget.Execute());

    //        //if (主动技能范围内有对象吗())
    //        //{
    //        //    释放技能();
    //        //}
    //        //else
    //        //{
    //        //    待机();
    //        //}

    //        inActiveSkillRange = new InActiveSkillRange();
    //        inActiveSkillRange.playerC = playerC;

    //        yield return StartCoroutine(inActiveSkillRange.Execute());


    //        //if (主动技能范围内有对象吗())
    //        if (inActiveSkillRange.state == SampleBehaviorTree.State.Succeed)
    //        {
    //            //释放技能();
    //            // Debug.Log("释放技能");
    //            var useSkill = new UseSkill();
    //            useSkill.inActiveSkillRange = inActiveSkillRange;
    //            yield return StartCoroutine(useSkill.Execute());
    //        }
    //        else
    //        {
    //            //待机();
    //            var wait = new Wait();
    //            wait.playerC = this.playerC;
    //            yield return StartCoroutine(wait.Execute());

    //        }

    //    }
    //    //else
    //    //{
    //    //    待机();
    //    //}

    //    yield return 1;

    //}



    //private void 待机()
    //{
    //    throw new NotImplementedException();
    //}

    //private bool 移动到最近的敌人()
    //{
    //    throw new NotImplementedException();
    //}

    //private void 释放技能()
    //{
    //    Debug.Log("释放技能");
    //}

    //private bool 主动技能范围内有对象吗()
    //{
    //    throw new NotImplementedException();
    //}

    //[ContextMenu("ExcuteBehavior 进攻型")]
    //public void ExcuteBehavior_Advanced()
    //{
    //    this.playerC.AISelect();
    //    UICtrl.instance.actionPanel.SetActive(false);
    //    StartCoroutine(c_ExcuteBehavior(this.root_Advanced_Attack));
    //}

    //[ContextMenu("ExcuteBehavior 辅助型")]
    //public void ExcuteBehavior_Auxiliary()
    //{
    //    this.playerC.AISelect();
    //    UICtrl.instance.actionPanel.SetActive(false);
    //    StartCoroutine(c_ExcuteBehavior(this.root_Advanced_Auxiliary));
    //}


    //BehaviorNode CreateBehavior_Advanced_Attack()
    //{
    //    var selector = new Selector();

    //    var sequence_1 = new Sequence();

    //    var condition_damageSkill = new InDamageSkillRange();
    //    var useSkill_damage = new UseSkill_Advanced();

    //    sequence_1.nodes.Add(condition_damageSkill);
    //    sequence_1.nodes.Add(useSkill_damage);

    //    var useAttack = new UseAttack();
    //    useAttack.playerC = this.playerC;


    //    var sequence_2 = new Sequence();
    //    var move = new MoveToTarget_Advanced();

    //    var sequence_3 = new Sequence();

    //    var condition_AuxiliarySkill = new InAuxiliarySkillRange();
    //    var useAuxiliarySkill = new UseSkill_Advanced();
    //    sequence_3.nodes.Add(condition_AuxiliarySkill);
    //    sequence_3.nodes.Add(useAuxiliarySkill);

    //    sequence_2.nodes.Add(move);
    //    //重用节点优化内存,节点执行前要重置属性
    //    sequence_2.nodes.Add(sequence_1);


    //    var wait = new Wait();

    //    selector.nodes.Add(sequence_1);

    //    selector.nodes.Add(sequence_2);

    //    selector.nodes.Add(useAttack);

    //    selector.nodes.Add(sequence_3);

    //    selector.nodes.Add(wait);

    //    //参数配置
    //    condition_AuxiliarySkill.playerC = this.playerC;
    //    useAuxiliarySkill.behaviorType = BehaviorType.Auxiliary;
    //    useAuxiliarySkill.inActiveSkillRange_Advanced = condition_AuxiliarySkill;
    //    wait.playerC = this.playerC;
    //    move.playerC = this.playerC;
    //    move.behaviorType = BehaviorType.Attck;
    //    condition_damageSkill.playerC = this.playerC;
    //    useSkill_damage.inActiveSkillRange_Advanced = condition_damageSkill;
    //    useSkill_damage.behaviorType = BehaviorType.Attck;


    //    return selector;
    //}
    //Auxiliary
    //BehaviorNode CreateBehavior_Advanced_Auxiliary()
    //{
    //    var selector = new Selector();

    //    var sequence_1 = new Sequence();

    //    var condition_damageSkill = new InDamageSkillRange();
    //    var useSkill_damage = new UseSkill_Advanced();

    //    sequence_1.nodes.Add(condition_damageSkill);
    //    sequence_1.nodes.Add(useSkill_damage);

    //    var useAttack = new UseAttack();
    //    useAttack.playerC = this.playerC;


    //    var sequence_2 = new Sequence();
    //    var move = new MoveToTarget_Advanced();

    //    var sequence_3 = new Sequence();

    //    var condition_AuxiliarySkill = new InAuxiliarySkillRange();
    //    var useAuxiliarySkill = new UseSkill_Advanced();
    //    sequence_3.nodes.Add(condition_AuxiliarySkill);
    //    sequence_3.nodes.Add(useAuxiliarySkill);

    //    sequence_2.nodes.Add(move);
    //    //重用节点优化内存,节点执行前要重置属性
    //    sequence_2.nodes.Add(sequence_3);


    //    var wait = new Wait();

    //    selector.nodes.Add(sequence_3);

    //    selector.nodes.Add(sequence_2);

    //    selector.nodes.Add(sequence_1);

    //    selector.nodes.Add(useAttack);

    //    selector.nodes.Add(wait);

    //    //参数配置
    //    condition_AuxiliarySkill.playerC = this.playerC;
    //    useAuxiliarySkill.behaviorType = BehaviorType.Auxiliary;
    //    useAuxiliarySkill.inActiveSkillRange_Advanced = condition_AuxiliarySkill;
    //    wait.playerC = this.playerC;
    //    move.playerC = this.playerC;
    //    move.behaviorType = BehaviorType.Auxiliary;
    //    condition_damageSkill.playerC = this.playerC;
    //    useSkill_damage.inActiveSkillRange_Advanced = condition_damageSkill;
    //    useSkill_damage.behaviorType = BehaviorType.Auxiliary;


    //    return selector;
    //}
}


