using UnityEditor;
using UnityEngine;

namespace GameEngine.Interaction
{
    [CustomEditor(typeof(TriggerBroadcaster))]
    public class TriggerBroadcasterEditor : Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawTriggerObserver(TriggerBroadcaster triggerBroadcaster, GizmoType gizmoType)
        {
            if (triggerBroadcaster.TriggerCollider == null)
                return;

            if (triggerBroadcaster.TriggerCollider is SphereCollider sphereCollider)
            {
                DrawMultipleCirclesGizmo(triggerBroadcaster, sphereCollider);
            }
            if (triggerBroadcaster.TriggerCollider is MeshCollider meshCollider)
            {
                DrawMultipleCirclesGizmo(triggerBroadcaster, meshCollider);
            }
        }

        private static void DrawMultipleCirclesGizmo(TriggerBroadcaster triggerBroadcaster, SphereCollider collider)
        {
            Gizmos.color = triggerBroadcaster.VisualizationColor;

            Vector3 circleCenter = triggerBroadcaster.transform.position + collider.center;

            float maxRadius = collider.radius;

            const float step = 0.1f;

            int numCircles = Mathf.CeilToInt(maxRadius / step);

            const int segments = 36;
            const float angleStep = 360f / segments;

            for (int j = 0; j < numCircles; j++)
            {
                float currentRadius = maxRadius - j * step;
                if (currentRadius <= 0)
                    break;

                for (int i = 0; i < segments; i++)
                {
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    float nextAngle = (i + 1) * angleStep * Mathf.Deg2Rad;

                    Vector3 point1 = circleCenter + new Vector3(Mathf.Cos(angle) * currentRadius, 0.1f,
                        Mathf.Sin(angle) * currentRadius);
                    Vector3 point2 = circleCenter + new Vector3(Mathf.Cos(nextAngle) * currentRadius, 0.1f,
                        Mathf.Sin(nextAngle) * currentRadius);

                    Gizmos.DrawLine(point1, point2);
                }
            }
        }
        
        private static void DrawMultipleCirclesGizmo(TriggerBroadcaster triggerBroadcaster, MeshCollider collider)
        {
            Gizmos.color = triggerBroadcaster.VisualizationColor;

            Vector3 circleCenter = triggerBroadcaster.transform.position - triggerBroadcaster.transform.localPosition;

            float maxRadius = triggerBroadcaster.transform.localScale.x / 2;

            const float step = 0.1f;

            int numCircles = Mathf.CeilToInt(maxRadius / step);

            const int segments = 36;
            const float angleStep = 360f / segments;

            for (int j = 0; j < numCircles; j++)
            {
                float currentRadius = maxRadius - j * step;
                if (currentRadius <= 0)
                    break;

                for (int i = 0; i < segments; i++)
                {
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    float nextAngle = (i + 1) * angleStep * Mathf.Deg2Rad;
                    Vector3 point1 = circleCenter + new Vector3(Mathf.Cos(angle) * currentRadius, 0.1f, Mathf.Sin(angle) * currentRadius);
                    Vector3 point2 = circleCenter + new Vector3(Mathf.Cos(nextAngle) * currentRadius, 0.1f, Mathf.Sin(nextAngle) * currentRadius);
                    Gizmos.DrawLine(point1, point2);
                }
            }
        }

    }
}