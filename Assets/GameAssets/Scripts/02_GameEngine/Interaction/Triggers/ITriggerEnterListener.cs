using UnityEngine;

namespace GameEngine.Interaction
{
    public interface ITriggerEnterListener : ITriggerValidator
    {
        public void TriggerEnterHandle(Collider other);
    }
}