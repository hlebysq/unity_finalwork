using UnityEngine;

namespace ObjectScripts
{
    public interface IGrabbable : IInteractable
    {
        Sprite Icon { get; }
        public void Drop();
    }
}