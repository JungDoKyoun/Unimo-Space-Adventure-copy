using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player List Item")]

    public sealed class PhotonPlayerListItem : ManagedPooledObject<int, PhotonPlayerListItem>
    {

    }

    public abstract class PhotonPlayerListItem<TPhotonPlayerListItem> : ManagedPooledObject<int, TPhotonPlayerListItem>

        where TPhotonPlayerListItem : PhotonPlayerListItem<TPhotonPlayerListItem>
    {

    }
}