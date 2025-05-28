using System;

using UnityEngine;

using UnityEngine.UI;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Grid Layout Group Cell Size Fitter")]

    public sealed class GridLayoutGroupCellSizeFitter : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        private GridLayoutGroup gridLayoutGroup = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private RectTransform container = null;

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Button(nameof(Fit))]

        private GridAxis axis = 0;

        private void Reset()
        {
            container = transform as RectTransform;
        }

        public void Fit()
        {
            Vector2 cellSize = gridLayoutGroup.cellSize;

            if (axis.HasFlag(GridAxis.Horizontal) == true)
            {
                cellSize.y = container.rect.height - gridLayoutGroup.padding.vertical;
            }

            if (axis.HasFlag(GridAxis.Vertical) == true)
            {
                cellSize.x = container.rect.width - gridLayoutGroup.padding.horizontal;
            }

            FixedUndo.RecordObject(gridLayoutGroup, "Fit Grid Layout Group Cell Size");

            gridLayoutGroup.cellSize = cellSize;

            FixedEditorUtility.SetDirty(gridLayoutGroup);
        }
    }
}