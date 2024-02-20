using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーを実行するクラス
    /// 実行はコルーチンで行う
    /// </summary>
    public class BehaviorRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTreeGraph _graph;
        private Node _root;
        
        /// <summary>
        /// 外部からビヘイビアツリーを実行する関数
        /// </summary>
        public void RunTree()
        {
            _graph = _graph.Clone();

            foreach (var node in _graph.nodes)
            {
                var behavioreNode = node as Node;
                behavioreNode.OnAwake();
            }
            _root = _graph.nodes.Find(n => n is Root) as Root;
            Run();
        }
        
        /// <summary>
        /// 実際にビヘイビアツリーを実行する内部関数
        /// </summary>
        private void Run()
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                Node node = stack.Peek();
                BehavioreNodeState state = node.Update();
                switch (state)
                {
                    case BehavioreNodeState.Waiting:
                        break;
                    case BehavioreNodeState.Success:
                        stack.Pop();
                        break;
                    case BehavioreNodeState.Failure:
                        stack.Pop();
                        break;
                    case BehavioreNodeState.Running:
                        break;
                }
            }
        }
    }
}
