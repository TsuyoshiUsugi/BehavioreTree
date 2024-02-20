using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            SetRoot();
            SetEveryNodeParent();
            CallOnAwake();
            Run();
        }

        /// <summary>
        /// 全ノードの親を設定する
        /// </summary>
        private void SetEveryNodeParent()
        {
            foreach (var node in _graph.nodes)
            {
                var behavioreNode = node as Node;
                if (behavioreNode is Root || behavioreNode is not Node)
                    continue;
                behavioreNode.Parent = behavioreNode.GetInputNodes().First() as Node;
            }
        }

        /// <summary>
        /// 全ノードのAwake処理を行う。これは一度しか呼ばれない
        /// </summary>
        private void CallOnAwake()
        {
            foreach (var node in _graph.nodes)
            {
                var behavioreNode = node as Node;
                behavioreNode.OnAwake();
            }
        }

        private void SetRoot()
        {
            _root = _graph.nodes.Find(n => n is Root) as Root;
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
