using System;

using UnityEngine;

using ZL.Unity.IO;

using ZL.Unity.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/String Table Manager (Singleton)")]

    public sealed class StringTableManager : MonoSingleton<StringTableManager>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(LoadLanguage))]

        [Button(nameof(SaveLanguage))]

        [Margin]

        private EnumPref<StringTableLanguage> targetLanguagePref = new("Target Language", StringTableLanguage.Korean);

        public StringTableLanguage TargetLanguage
        {
            get => targetLanguagePref.Value;

            set => targetLanguagePref.Value = value;
        }

        public event Action OnLanguageChangedAction = null;

        private void OnValidate()
        {
            targetLanguagePref.Value = targetLanguagePref.Value;
        }

        protected override void Awake()
        {
            base.Awake();

            targetLanguagePref.OnValueChanged += (value) =>
            {
                OnLanguageChangedAction?.Invoke();
            };

            LoadLanguage();
        }

        public void LoadLanguage()
        {
            targetLanguagePref.TryLoadValue();
        }

        public void SaveLanguage()
        {
            targetLanguagePref.SaveValue();
        }
    }
}