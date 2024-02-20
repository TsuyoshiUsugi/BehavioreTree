using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 指定した回数だけ子ノードを実行するノード
    /// 一度でも失敗したら失敗を返す
    /// 全部成功したら成功を返す
    /// </summary>
    [Serializable, NodeMenuItem("Decorator/Repeater")]
    public class Repeater : Decorator
    {
        [SerializeField] private int _repeatCount;
        private int _currentCount;
        protected override void OnStart()
        {
            if (_repeatCount <= 0)
                throw new InvalidOperationException("繰り返し回数は1以上である必要があります");
            _currentCount = 0;
        }

        protected override BehavioreNodeState OnUpdate()
        {
            while (_currentCount < _repeatCount)
            {
                var childState = Child.Update();
                switch (childState)
                {
                    case BehavioreNodeState.Success:
                        _currentCount++;
                        break;
                    case BehavioreNodeState.Failure:
                        return BehavioreNodeState.Failure; // 子ノードが失敗した場合はすぐに失敗を返す
                    case BehavioreNodeState.Running:
                        return BehavioreNodeState.Running; // 子ノードがまだ実行中の場合は、実行中を返す
                }
            }

            return BehavioreNodeState.Success; // 全ての繰り返しが成功した場合
        }
        
        protected override void OnEnd()
        {
            
        }
    }
}
