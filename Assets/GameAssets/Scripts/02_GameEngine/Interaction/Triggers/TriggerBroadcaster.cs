using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameEngine.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class TriggerBroadcaster : MonoBehaviour
    {
        public Collider TriggerCollider => _triggerCollider;
        [SerializeField, Required] 
        private Collider _triggerCollider;

        public Color VisualizationColor => _visualizationColor;
        [SerializeField, Required] 
        private Color _visualizationColor = Color.white;
        
        public event Action<Collider> OnTriggerEntered;
        public event Action<Collider> OnTriggerExited;
        public event Action<Collider> OnTriggerStayed;
        
        private bool _isBroadcastingEnabled = true;
        
        private void OnValidate()
        {
            ValidateCollider();
        }

        private void Awake()
        {
            ValidateCollider();
        }

        private void ValidateCollider()
        {
            if (_triggerCollider != null)
            {
                if (_triggerCollider.isTrigger)
                {
                    return;
                }
            }
            
            _triggerCollider = GetComponent<Collider>();
            _triggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isBroadcastingEnabled) return;
            OnTriggerEntered?.Invoke(other);
        } 

        private void OnTriggerExit(Collider other)
        {
            if (!_isBroadcastingEnabled) return;
            OnTriggerExited?.Invoke(other);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (!_isBroadcastingEnabled) return;
            OnTriggerStayed?.Invoke(other);
        }

        public void EnableTriggerBroadcast() => 
            _isBroadcastingEnabled = true;

        
        public void DisableTriggerBroadcast() => 
            _isBroadcastingEnabled = false;
    }
}