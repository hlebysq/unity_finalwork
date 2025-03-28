using ObjectScripts;
using Unity.Netcode;
using UnityEngine;

namespace Lobby
{
    public class GameStartingButton : NetworkBehaviour, IInteractable
    {
        [SerializeField] private string clientAttemptMsg;
        private TextHelper textHelper;
        
        private void Awake()
        {
            textHelper = GetComponent<TextHelper>();
        }
        
        public void Interact(Transform player)
        {
            if (!IsHost)
            {
                textHelper.TempDisplayText128Chars(clientAttemptMsg);
                return;
            }
            SceneLoader.Instance.LoadSceneGlobal(SceneLoader.Scene.Maze);
        }

        public bool CanInteract() => true;
    }
}