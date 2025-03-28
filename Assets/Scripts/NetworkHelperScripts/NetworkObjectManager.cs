﻿using ObjectScripts;
using UnityEngine;
using Unity.Netcode;

public class NetworkObjectManager : NetworkBehaviour
{
    [SerializeField] private SerializableSOList serializableSOList;
    public static NetworkObjectManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnGrabbable(GrabbableSO grabbableSO, Vector3 position, Quaternion rotation)
    {
        SpawnGrabbableServerRpc(serializableSOList.grabbables.IndexOf(grabbableSO), position, rotation);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnGrabbableServerRpc(int SOIndex, Vector3 position, Quaternion rotation)
    {
        GrabbableSO grabbableSO = serializableSOList.grabbables[SOIndex];
        Transform spawnedObject = Instantiate(grabbableSO.prefab, position, rotation);
        NetworkObject spawnedNetworkObject = spawnedObject.GetComponent<NetworkObject>();
        spawnedNetworkObject.Spawn(true);
    }
}