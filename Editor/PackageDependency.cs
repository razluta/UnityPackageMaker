using UnityEngine;

namespace UnityPackageMaker.Editor
{
    public class PackageDependency
    {
        [SerializeField] private string _dependencyName;
        [SerializeField] private string _dependencyVersion;

        public PackageDependency()
        {
            DependencyName = "";
            DependencyVersion = "";
        }

        public string DependencyName
        {
            get => _dependencyName;
            set => _dependencyName = value;
        }

        public string DependencyVersion
        {
            get => _dependencyVersion;
            set => _dependencyVersion = value;
        }
    }
}