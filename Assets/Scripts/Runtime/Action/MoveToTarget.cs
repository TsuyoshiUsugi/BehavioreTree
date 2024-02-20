using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Navmeshを使ってターゲットに移動する
    /// </summary>
    [Serializable, NodeMenuItem("Action/MoveToTarget")]
    public class MoveToTarget : Action
    {
        [Input(name = "Owner")] public GameObject Owner;
        [Input(name = "Target")] public GameObject Target;
        private NavMeshAgent _agent;
        
        public override void OnAwake()
        {
            Owner = GetInputNodes().First() as GameObject;
            _agent = Owner.GetComponent<NavMeshAgent>();
        }

        protected override void OnStart()
        {
            _agent.destination = Target.transform.position;
        }

        protected override BehavioreNodeState OnUpdate()
        {
            if (!_agent.hasPath)
            {
                return BehavioreNodeState.Failure;
            }
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                return BehavioreNodeState.Success;
            }
            return BehavioreNodeState.Running;
        }

        protected override void OnEnd()
        {
        }
    }
}
