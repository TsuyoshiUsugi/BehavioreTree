using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 子を持つことのできるノードの基底クラス
    /// 開始時と終了時に実行する子ノードのインデックスの初期化を行う
    /// </summary>
    public abstract class Branch : Node
    {
        [Output(name = "Children", allowMultiple = true), Vertical] public Node Children;
        protected List<Node> _children = new List<Node>();
        protected int _currentChildIndex = 0;

        protected override void OnAwake()
        {
            _children = GetOutputNodes() as List<Node>;
        }
        
        protected override void OnStart()
        {
            _currentChildIndex = 0;
        }

        protected override void OnEnd()
        {
            _currentChildIndex = 0;
        }
    }
}
