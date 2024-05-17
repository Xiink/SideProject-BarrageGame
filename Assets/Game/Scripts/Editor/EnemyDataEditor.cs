using System.IO;
using Game.Scripts.Enemy.Data;
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
           // odinMenuTree.Add("Create new unit data",_enemyData);
            odinMenuTree.AddAllAssetsAtPath("Enemy Data", "Assets/Game/Datas/EnemyDatas",
                typeof(EnemyData));
            return odinMenuTree;
        }
    }
}