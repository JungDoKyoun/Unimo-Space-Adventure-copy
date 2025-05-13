using UnityEngine;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweening/Material Fader")]

    [RequireComponent(typeof(MaterialAlphaTweener))]

    public sealed class MaterialFader : AlphaFader<MaterialAlphaTweener>
    {

    }
}