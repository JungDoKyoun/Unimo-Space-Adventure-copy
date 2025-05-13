using System;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Grid Layout Group Cell Size Fitter")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(GridLayoutGroup))]

    public sealed class GridLayoutGroupCellSizeFitter : MonoBehaviour
    {
        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private GridLayoutGroup gridLayoutGroup;

        [Space]

        [SerializeField]

        private RectTransform container;

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(Fit))]

        private Axis axis;

        private void Reset()
        {
            container = transform as RectTransform;
        }

        public void Fit()
        {
            Vector2 cellSize = gridLayoutGroup.cellSize;

            if (axis.HasFlag(Axis.Horizontal) == true)
            {
                cellSize.y = container.rect.height - gridLayoutGroup.padding.vertical;
            }

            if (axis.HasFlag(Axis.Vertical) == true)
            {
                cellSize.x = container.rect.width - gridLayoutGroup.padding.horizontal;
            }

            FixedUndo.RecordObject(gridLayoutGroup, "Fit Grid Layout Group Cell Size");

            gridLayoutGroup.cellSize = cellSize;

            FixedEditorUtility.SetDirty(gridLayoutGroup);
        }

        [Flags]

        public enum Axis
        {
            Horizontal = 1,

            Vertical = 2,
        }
    }
}