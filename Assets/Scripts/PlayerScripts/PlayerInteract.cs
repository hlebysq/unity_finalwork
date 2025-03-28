using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using ObjectScripts;

namespace PlayerScripts
{
    public class PlayerInteract : NetworkBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform holdPointTransform;
    
        private PlayerInventory _inventory;
        private PlayerInputActions _inputActions;
        private InputAction _interact;
        private InputAction _drop;
        private InputAction _switchItem;
        private PlayerRayCaster _rayCaster;
        
        public Transform HoldPointTransform => holdPointTransform;

        private void Start()
        {
            if (!IsOwner) return;
            
            _inputActions = GetComponent<InputActionsContainer>().InputActions;
            _rayCaster = GetComponent<PlayerRayCaster>();
            _inventory = GetComponent<PlayerInventory>();

            _interact = _inputActions.Player.Interact;
            _drop = _inputActions.Player.Drop;
            _switchItem = _inputActions.Player.SwitchItem;
            
            OnEnable();
        }

        private void OnEnable()
        {
            if (!IsOwner) return;
            
            _interact.performed += Interact;
            _drop.performed += Drop;
            _switchItem.performed += SwitchItem;
        }

        private void OnDisable()
        {
            if (!IsOwner) return;
            
            _interact.performed -= Interact;
            _drop.performed -= Drop;
            _switchItem.performed -= SwitchItem;
        }
    
        private void Interact(InputAction.CallbackContext context)
        {
            Debug.Log("Interact");
            RaycastHit hit;
            if (_rayCaster.ViewPointRayCast(out hit)) 
            {
                if (hit.collider.TryGetComponent(out IGrabbable grabbable) && grabbable.CanInteract())
                {
                    if (_inventory.AddItem(grabbable))
                    {
                        Debug.Log("Item picked up");
                    }
                    return;
                }
                if (hit.collider.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
                {
                    interactable.Interact(transform);
                }
            }
        }

        private void Drop(InputAction.CallbackContext context)
        {
            _inventory.DropSelectedItem();
        }

        private void SwitchItem(InputAction.CallbackContext context)
        {
            _inventory.SwitchItem();
        }
    }
}
