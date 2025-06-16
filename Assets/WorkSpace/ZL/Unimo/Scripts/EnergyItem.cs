using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Energy Item (Pooled)")]

    public sealed class EnergyItem : Item
    {
        [SerializeField]

        private int energy = 1;

        public override void GetItem(PlayerManager player)
        {
            player.GetEnergy(energy);

            Disappear();
        }
    }
}