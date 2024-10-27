using UnityEngine;

namespace GameEngine.Interaction
{
    public interface ITriggerValidator
    {
        public bool ValidateTrigger(Collider other);
    }
}