using GameEngine.Interaction;
using Sirenix.OdinInspector;
using UnityEngine;

public class Trap : MonoBehaviour, ITriggerEnterListener
{
    [SerializeField, Required]
    private TriggerBroadcaster _triggerBroadcaster;

    private void OnEnable()
    {
        _triggerBroadcaster.OnTriggerEntered += TriggerEnterHandle;
    }

    private void OnDisable()
    {
        _triggerBroadcaster.OnTriggerEntered -= TriggerEnterHandle;
    }

    public void TriggerEnterHandle(Collider other)
    {
        if (ValidateTrigger(other))
        {
            Activate(other);
        }
    }

    public bool ValidateTrigger(Collider other)
    {
        return other.GetComponentInParent<Actor>() && (other.GetComponent<HitBoxMarker>() != null);
    }

    private void Activate(Collider other)
    {
        Debug.Log($"Trap '{gameObject.name}' ACTIVATED!");
    }
}