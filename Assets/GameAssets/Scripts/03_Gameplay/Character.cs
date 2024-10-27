using UnityEngine;

namespace Gameplay
{
    public class Character : Actor
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.LogError(other.name);
        }
    }
}