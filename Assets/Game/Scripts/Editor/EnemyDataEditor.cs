using System.IO;
using System.Linq;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Phases;
using Game.Scripts.Helpers;
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
            var addNewPhases = new TestAddPhases();
            enemyTree.Add("Enemy Data/Phases",addNewPhases);

            // for (int i = 0; i < GetPhaseNames.Names.Count; i++)
            // {
            //     string phase = GetPhaseNames.Names[i].ToString();
            //     enemyTree.AddAssetAtPath($"Enemy Data/Phases/{phase}",$"Assets/Game/Datas/EnemyDatas/Phase/{phase}.asset");
            // }
            for (int i = 0; i < GetPhases.phases.Count; i++)
            {
                // string phase = GetPhaseNames.Names[i].ToString();
                var phase = GetPhases.phases[i];
                enemyTree.AddAssetAtPath($"Enemy Data/Phases/{phase.Text}",$"Assets/Game/Datas/EnemyDatas/Phase/{phase.Text}.asset");
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
            [ShowInInlineEditors]
            public EnemyPhase NewEnemyPhase;

            [ValidateInput(nameof(ValidateName), defaultMessage: "Name is Empty")]
            [LabelText("顯示名稱 :")]
            public string DisplayName;

            // [ValidateInput(nameof(ValidateUID), defaultMessage: "uid 重複")]
            // [LabelText("唯一UID :")]
            // public uint uid;

            [Button("Add new Data")]
            private void AddData()
            {
                NewEnemyPhase = ScriptableObject.CreateInstance<EnemyPhase>();

                NewEnemyPhase.DisplayName = DisplayName;
                var phases = GetPhases.phases;
                var newUid = phases.Last().Value.uid + 1;
                // var uids = GetPhaseUids.Ids;
                // uint newUid = GetPhaseUids.Ids.Last() + 1;
                NewEnemyPhase.uid = newUid;

                AssetDatabase.CreateAsset(NewEnemyPhase,$"Assets/Game/Datas/EnemyDatas/Phase/{DisplayName}.asset");
                AssetDatabase.SaveAssets();
            }

            /// <summary>
            /// 驗證Name是否正常
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            private static bool ValidateName(string name)
            {
                return ValidationHelper.ValidateString(name);
            }

            // private static bool ValidateUID(uint id)
            // {
            //     return ValidationHelper.PhaseUIDValidation(GetPhaseUids.Ids, id);
            // }
        }



        // private static bool ValidateUID(uint id)
        // {
            // return ValidationHelper.PhaseUIDValidation(Phase.GetPhases(), id);
        // }
    }
}