using UnityEngine;

namespace GameEngine.Interaction
{
    public interface ITriggerExitListener : ITriggerValidator
    {
        public void TriggerExitHandle(Collider other);
    }
}