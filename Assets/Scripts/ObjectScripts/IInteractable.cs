using UnityEngine;

namespace ObjectScripts
{
    public interface IInteractable
    {
        void Interact(Transform interactor);
        bool CanInteract();
    }
}