using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Energy Item (Spawned)")]

    public sealed class EnergyItem : Item
    {
        [Space]

        [SerializeField]

        private int energy = 1;

        public override void GetItem<T>(T getter)
        {
            if (getter is IEnergizer energizer)
            {
                energizer.GetEnergy(energy);
            }

            Disappear();
        }
    }
}