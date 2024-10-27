using GameEngine.Interaction;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Mechanics
{
    public class PickUp : MonoBehaviour, ITriggerEnterListener, ITriggerExitListener
    {
        [SerializeField, Required]
        private TriggerBroadcaster _triggerBroadcaster;

        [SerializeField, ReadOnly]
        private Pickable _lastTriggerPickable;
        
        private void OnEnable()
        {
            _triggerBroadcaster.OnTriggerEntered += TriggerEnterHandle;
            _triggerBroadcaster.OnTriggerExited += TriggerExitHandle;
        }

        private void OnDisable()
        {
            _triggerBroadcaster.OnTriggerEntered -= TriggerEnterHandle;
            _triggerBroadcaster.OnTriggerExited -= TriggerExitHandle;
        }

        public void TriggerEnterHandle(Collider other)
        {
            if (ValidateTrigger(other))
            {
                _lastTriggerPickable = other.GetComponentInParent<Pickable>();
            }        
        }

        public void TriggerExitHandle(Collider other)
        {
            if (ValidateTrigger(other))
            {
                if (other.GetComponentInParent<Pickable>() == _lastTriggerPickable)
                {
                    _lastTriggerPickable = null;
                }
            }
        }

        public bool ValidateTrigger(Collider other)
        {
            return other.GetComponentInParent<Pickable>() && (other.GetComponent<HitBoxMarker>() != null);
        }
    }
}
