using System;
using UnityEngine;

namespace UnityPackageMaker.Editor
{
    [Serializable]
    public class PackageKeyword : ScriptableObject
    {
        [SerializeField] private string keywordValue;

        public PackageKeyword()
        {
            KeywordValue = "";
        }

        public string KeywordValue
        {
            get => keywordValue;
            set => keywordValue = value;
        }
    }
}