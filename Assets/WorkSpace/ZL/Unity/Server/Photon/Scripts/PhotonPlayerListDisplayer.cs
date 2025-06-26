using Photon.Pun;

using Photon.Realtime;

using UnityEngine;

using ZL.Unity.Pooling;

namespace ZL.Unity.Server.Photon
{
    [AddComponentMenu("ZL/Server/Photon/Photon Player List Displayer")]

    public class PhotonPlayerListDisplayer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        protected DictionaryObjectPool<int> playerListItemPool = null;

        public void Refresh()
        {
            playerListItemPool.CollectAll();

            foreach (var player in PhotonNetwork.PlayerList)
            {
                Add(player);
            }
        }

        public virtual void Add(Player player)
        {
            playerListItemPool.TryGenerate(player.ActorNumber, out var item);

            item.transform.SetAsLastSibling();

            item.gameObject.SetActive(true);
        }

        public void Remove(Player player)
        {
            playerListItemPool[player.ActorNumber].gameObject.SetActive(false);
        }
    }
}