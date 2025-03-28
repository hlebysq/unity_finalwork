using UnityEngine;

namespace PlayerScripts
{
    public class PlayerRayCaster : MonoBehaviour
    {
        [SerializeField] private float interactDistance;
        private Camera _playerCamera;

        private void Start()
        {
            _playerCamera = Camera.main;
        }
        public bool ViewPointRayCast(out RaycastHit hit)
        {
            Ray ray = _playerCamera.ViewportPointToRay(Vector3.one / 2f);
            return Physics.Raycast(ray, out hit, interactDistance);
        }
    }
}
