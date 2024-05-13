using UnityEngine;
using Zenject;

namespace Game.Scripts.Battle.Misc
{
    public interface ICameraProvider
    {
        Vector3 GetMousePosition();
    }

    public class CameraProvider : ICameraProvider
    {
        [Inject] private Camera _mainCamera;

        public Vector3 GetMousePosition()
        {
            var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var mousePos = mouseRay.origin;
            mousePos.z = 0;
            return mousePos;
        }
    }
}