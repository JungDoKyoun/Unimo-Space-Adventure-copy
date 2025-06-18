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

        [Text("<b>테스트 옵션</b>", FontSize = 16)]

        [Margin]

        [Text("<b>체력 무한</b>")]

        private bool infinityHealth = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>연료 무한</b>")]

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