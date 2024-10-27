using UnityEngine;

namespace GameEngine.Animation
{
    public class RotateAroundGlobalY : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 30f;

        private void Update() => 
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
    }
}
