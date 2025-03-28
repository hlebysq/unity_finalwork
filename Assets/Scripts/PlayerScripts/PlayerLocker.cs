﻿using UnityEngine;

namespace PlayerScripts
{
    public class PlayerLocker : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        public static PlayerLocker Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        public void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerMovement.CanRotate = true;
        }
        public void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerMovement.CanRotate = false;
        }

        public void LockMovement()
        {
            _playerMovement.CanMove = false;
        }
        public void UnlockMovement()
        {
            _playerMovement.CanMove = true;
        }
    }
}