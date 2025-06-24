using UnityEngine;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Skill Data Sheet", fileName = "Skill Data Sheet")]

    public sealed class SkillDataSheet : ScriptableGoogleSheet<SkillData>
    {

    }
}