using System;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable, NodeMenuItem("Root")]
    public class Root : Node
    {
        [Output(name = "Child"), Vertical] public Node Child;

        protected override void OnStart()
        {
            
        }

        protected override BehavioreNodeState OnUpdate()
        {
            return Child.Update();
        }

        protected override void OnEnd()
        {
            
        }
    }
}
