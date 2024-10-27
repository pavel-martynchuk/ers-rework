using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEngine.Interaction
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class TriggerObserverVisualDebug : MonoBehaviour
    {
        [FormerlySerializedAs("_triggerObserver")]
        [SerializeField, Required] private TriggerBroadcaster _triggerBroadcaster;
        [SerializeField] private int _segmentCount = 50; 
        
        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            if (_triggerBroadcaster.TriggerCollider is SphereCollider sphereCollider)
            {
                GenerateCircleMesh(sphereCollider.radius);
            }
        }

        private void OnDisable() => 
            ClearMesh();

        private void LateUpdate()
        {
            if (_triggerBroadcaster.TriggerCollider is SphereCollider sphereCollider)
            {
                if (_meshFilter.mesh == null || _meshFilter.mesh.vertices.Length != _segmentCount + 1 ||
                    !Mathf.Approximately(_meshFilter.mesh.vertices[1].magnitude, sphereCollider.radius))
                {
                    GenerateCircleMesh(sphereCollider.radius);
                }
            }
        }

        private void ClearMesh() => 
            _meshFilter.mesh = null;

        private void GenerateCircleMesh(float radius)
        {
            Mesh mesh = new Mesh
            {
                name = "Trigger Visual Mesh"
            };

            Vector3[] vertices = new Vector3[_segmentCount + 1];
            int[] triangles = new int[_segmentCount * 3];

            vertices[0] = Vector3.zero;

            float angleStep = 360f / _segmentCount;
            for (int i = 0; i < _segmentCount; i++)
            {
                float angle = Mathf.Deg2Rad * angleStep * i;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                vertices[i + 1] = new Vector3(x, 0, z);

                if (i < _segmentCount - 1)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 2;
                    triangles[i * 3 + 2] = i + 1;
                }
                else
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = 1;
                    triangles[i * 3 + 2] = i + 1;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            _meshFilter.mesh = mesh;
        }
    }
}