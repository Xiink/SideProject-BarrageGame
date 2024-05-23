using System.IO;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Values;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Editor
{
    public class EnemyDataEditor : OdinMenuEditorWindow
    {
        [MenuItem("TBSF/EnemyDataEditor Editor")]
        private static void OpenWindow()
        {
            GetWindow<EnemyDataEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree odinMenuTree = new OdinMenuTree();
            var addnewData = new TestAddPhases();
            odinMenuTree.Add("Add new unit data",addnewData);
            odinMenuTree.AddAllAssetsAtPath("Enemy Data", "Assets/Game/Datas/EnemyDatas",
                typeof(EnemyData));
            return odinMenuTree;
        }

        public class TestAddPhases
        {
            [Button("Add new Data")]
            private void AddData()
            {
                //Phases.GetPhases.Add("TestData");
            }
        }
    }
}