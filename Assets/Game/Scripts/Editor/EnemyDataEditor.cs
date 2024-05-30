using System.IO;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Values;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
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
            OdinMenuTree enemyTree = new OdinMenuTree();
            enemyTree.Config.DrawSearchToolbar = true;
            //var addnewData = new TestAddPhases();
            //odinMenuTree.Add("Add new unit data",addnewData);

            enemyTree.AddAssetAtPath("Enemy Data", "Assets/Game/Datas/EnemyDatas/NormalEnemyData.asset", typeof(EnemyData));
            enemyTree.Add("Enemy Data/Phases",new GUI());

            for (int i = 0; i < GetAllPhases.GetAllPhaseNames.Count; i++)
            {
                string phase = GetAllPhases.GetAllPhaseNames[i].ToString();
                enemyTree.AddAssetAtPath($"Enemy Data/Phases/{phase}",$"Assets/Game/Datas/EnemyDatas/Phase/{phase}.asset");
            }

            enemyTree.Add("Enemy Data/Sequences",new GUI());
            enemyTree.Add("Enemy Data/Steps",new GUI());
            enemyTree.Add("Enemy Data/Actions",new GUI());
            // enemyTree.AddAllAssetsAtPath("Enemy Data", "Assets/Game/Datas/EnemyDatas",
            //     typeof(EnemyData),true);
            return enemyTree;
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