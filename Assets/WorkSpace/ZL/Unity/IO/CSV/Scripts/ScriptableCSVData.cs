using UnityEngine;

using ZL.CS.IO.CSV;

namespace ZL.Unity.IO.CSV
{
    public abstract class ScriptableCSVData : ScriptableObject, ICSVData
    {
        public abstract string GetHeader();

        public abstract void Import(string[] values);

        public abstract string Export();
    }
}