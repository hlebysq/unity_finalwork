using System;
using Unity.Netcode;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Configure")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float maxVerticalAngle;
        [SerializeField] private float minVerticalAngle;
        [SerializeField] private Transform head;
    
        private Rigidbody _playerRigidbody;
        private PlayerInputActions _inputActions;
        public bool CanRotate { get; set; } = true;
        public bool CanMove { get; set; } = true;
        
        private void Start()
        {
            if (!IsOwner) return;
            
            _inputActions = GetComponent<InputActionsContainer>().InputActions;
            _playerRigidbody = GetComponent<Rigidbody>();

            // Some variable adjustments (for neater numbers in the editor parameters)
            mouseSensitivity /= 10f;
        }
        private void Update()
        {
            if (!IsOwner) return;
            
            if (CanRotate)
            {
                var lookVector = _inputActions.Player.Look.ReadValue<Vector2>();
                RotatePlayerY(lookVector.x * mouseSensitivity);
                RotatePlayerHeadX(lookVector.y * mouseSensitivity);
            }
        }
    
        private void FixedUpdate()
        {
            if (!IsOwner) return;
            if (CanMove)
            {
                var moveVector = _inputActions.Player.Move.ReadValue<Vector2>();
                MovePlayer(moveVector);
            }
        }

        private void MovePlayer(Vector2 moveVector)
        {
            _playerRigidbody.linearVelocity = transform.TransformDirection(new Vector3(
                moveVector.x * moveSpeed,
                _playerRigidbody.linearVelocity.y, 
                moveVector.y * moveSpeed
            ));
        
            // Alternative movement system (if player has no RigidBody)
            //var moveVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            //transform.Translate( moveSpeed * new Vector3(moveVector.x, 0f, moveVector.y), Space.Self);
        }

        private void RotatePlayerY(float angle)
        {
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, angle, 0));
            
            // Alternative movement system (if player has no RigidBody)
            //transform.Rotate(0, angle, 0);
        }
        
        private void RotatePlayerHeadX(float angle)
        {
            var euler = head.eulerAngles;
            euler.x -= angle;
            // Without the following eulerAngles teleport the camera when out of (-180; 180) interval
            switch (euler.x)
            {
                case > 180:
                    euler.x -= 360;
                    break;
                case < -180:
                    euler.x += 360;
                    break;
            }
        
            euler.x = Math.Clamp(euler.x, minVerticalAngle, maxVerticalAngle);
            head.eulerAngles = euler;
        }
        
    }
    
}