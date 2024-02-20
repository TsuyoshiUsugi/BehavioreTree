using System;
using GraphProcessor;

namespace BehaviorTree
{
    [Serializable, NodeMenuItem("Root")]
    public class Root : Node
    {
        private new Node Parent;
        public Node Child;
        
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
