using System.IO;
using UnityEngine;
using GraphProcessor;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Assertions;

namespace BehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーを表示するウィンドウクラス
    /// </summary>
    public class BehaviorTreeWindow : BaseGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            Assert.IsNotNull(graph);
            var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(graph));
            titleContent = new GUIContent(ObjectNames.NicifyVariableName(fileName));
            if (graphView == null)
            {
                graphView = new DefaultBehaviorTree(this);
            }
            rootView.Add(graphView);
        }
    }
}
#endif

