using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Test Stage Scene Controller")]

    public sealed class TestStageSceneController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private StageSceneDirector stageSceneDirector = null;

        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>�׽�Ʈ �ɼ�</b>", FontSize = 16)]

        [Margin]

        [Text("<b>ü�� ����</b>")]

        private bool infinityHealth = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� ����</b>")]

        private bool infinityFuel = false;

        private void Update()
        {
            if (infinityHealth == true)
            {

            }

            if (infinityFuel == true)
            {
                PlayerFuelManager.Fuel = PlayerFuelManager.MaxFuel;
            }
        }
    }
}