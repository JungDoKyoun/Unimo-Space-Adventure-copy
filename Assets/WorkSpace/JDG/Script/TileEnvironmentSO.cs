using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum EnvironmentType
    {
        None
    }

    [CreateAssetMenu(fileName = "TileEnvironmentSO", menuName = "SO/TileSO/TileEnvironmentSO")]
    public class TileEnvironmentSO : ScriptableObject
    {
        public EnvironmentType EnvironmentType;
        public Sprite EnviromentIcon;
        public string EnviromentName;
    }
}
