using UnityEngine;

namespace Game.Scripts.UI
{
    public class BackgroundRepeater : MonoBehaviour
    {
        public Transform player; // 角色的 Transform
        public float parallaxSpeed = 0.5f; // 背景移動的速度比例
        private Vector3 startPosition1, startPosition2;
        private GameObject background1, background2; // 兩個背景
        private float backgroundWidth;

        void Start()
        {
            // 找到兩張背景圖
            background1 = transform.GetChild(0).gameObject;
            background2 = transform.GetChild(1).gameObject;

            // 設置兩張背景圖的初始位置
            startPosition1 = background1.transform.position;
            startPosition2 = background2.transform.position;

            // 根據 SpriteRenderer 的尺寸來計算背景寬度
            backgroundWidth = background1.GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void Update()
        {
            // 背景跟隨角色水平移動，速度較慢，創建視差效果
            float offset = player.position.x * parallaxSpeed;

            // 更新背景位置
            background1.transform.position = new Vector3(startPosition1.x + offset, background1.transform.position.y, background1.transform.position.z);
            background2.transform.position = new Vector3(startPosition2.x + offset, background2.transform.position.y, background2.transform.position.z);

            // 當背景1完全移出視野，將它移動到背景2的另一側
            if (player.position.x - background1.transform.position.x > backgroundWidth)
            {
                background1.transform.position = new Vector3(background2.transform.position.x + backgroundWidth, background1.transform.position.y, background1.transform.position.z);
            }

            // 當背景2完全移出視野，將它移動到背景1的另一側
            if (player.position.x - background2.transform.position.x > backgroundWidth)
            {
                background2.transform.position = new Vector3(background1.transform.position.x + backgroundWidth, background2.transform.position.y, background2.transform.position.z);
            }
        }
    }
}