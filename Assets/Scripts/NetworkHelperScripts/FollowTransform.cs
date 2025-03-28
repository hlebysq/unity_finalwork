using Unity.Netcode;
using UnityEngine;

public class FollowTransform : NetworkBehaviour
{
    private Transform target;
    private bool targetSet = false;
    
    public void Follow(Transform origin)
    {
        target = origin;
        targetSet = true;
    }
    
    public void Unfollow() => targetSet = false;

    private void LateUpdate()
    {
        if (!targetSet) return;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}