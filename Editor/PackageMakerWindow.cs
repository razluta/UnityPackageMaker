using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityPackageMaker.Editor 
{
    public class PackageMakerWindow : EditorWindow
    {
        private const string PackageMakerName = "Package Maker";
        private const string PackageMakerMenuItemPath = "Raz's Tools/Package Maker";
        
        private const string IncludedPackageContentsUxmlPath = "CS_IncludedPackageContents";
        private const string PackageManifestUxmlPath = "CS_PackageManifest";
        private const string CreatePackageButtonUxmlPath = "BT_CreatePackage";

        private static readonly Vector2 PackageMakerWindowSize = new Vector2(350, 650);

        private VisualElement _root;
        
        [MenuItem(PackageMakerMenuItemPath)]
        public static void ShowWindow()
        {
            var window = GetWindow<PackageMakerWindow>();
            window.titleContent = new GUIContent(PackageMakerName);
            window.minSize = PackageMakerWindowSize;
        }

        private void OnEnable()
        {
            _root = rootVisualElement;
            
            // Included Package Contents
            var includedPackageContentsVisualTree = Resources.Load<VisualTreeAsset>(IncludedPackageContentsUxmlPath);
            includedPackageContentsVisualTree.CloneTree(_root);
            
            // Create Package Button
            var packageManifestVisualTree = Resources.Load<VisualTreeAsset>(PackageManifestUxmlPath);
            packageManifestVisualTree.CloneTree(_root);

            // Create Package Button
            var createPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(CreatePackageButtonUxmlPath);
            createPackageButtonVisualTree.CloneTree(_root);
        }
    }
}

