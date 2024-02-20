using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Decoratorノードの基底クラス
    /// </summary>
    public abstract class Decorator : Node
    {
        [Output(name = "Child"), Vertical] public Node Child;

        public override void OnAwake()
        {
            Child = GetOutputNodes().First() as Node;
        }
    }
}
