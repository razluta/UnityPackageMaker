using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityPackageMaker.Editor 
{
    public class PackageMakerWindow : EditorWindow
    {
        private const string PackageMakerName = "Package Maker";
        private const string PackageMakerMenuItemPath = "Raz's Tools/Package Maker";
        private const string PackageMakerToolsUrl = "https://github.com/razluta/UnityPackageMaker";
        
        private const string HeaderUxmlPath = "BT_Header";
        private const string LoadPackageButtonUxmlPath = "BT_LoadPackage";
        private const string MainPanelUxmlPath = "CS_PackageMakerCore";
        private const string IncludedPackageContentsUxmlPath = "CS_IncludedPackageContents";
        private const string AllPackagesUxmlPath = "CS_AllPackages";
        private const string PackageManifestUxmlPath = "CS_PackageManifest";
        private const string ReadmeUxmlPath = "CS_Readme";
        private const string ChangelogUxmlPath = "CS_Changelog";
        private const string LicenseUxmlPath = "CS_License";

        private const string UpdatePackageButtonUxmlPath = "BT_UpdatePackage";
        private const string CreatePackageButtonUxmlPath = "BT_CreatePackage";

        private const string PackMakButtonName = "BT_Logo";
        private const string LeftPanelName = "VE_PmLeftPanel";
        private const string RightPanelName = "VE_PmRightPanel";
        private const string AllPackagesVisualElementName = "VE_AllPackages";
        private const string UpdatePackageButtonName = "BT_UpdatePackage";
        private const string CreatePackageButtonName = "BT_CreatePackage";

        private static readonly Vector2 PackageMakerWindowSize = new Vector2(900, 900);

        private VisualElement _root;
        private ScrollView _contentsView;
        
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
            _contentsView = new ScrollView();
            
            // Header
            var headerVisualTree = Resources.Load<VisualTreeAsset>(HeaderUxmlPath);
            headerVisualTree.CloneTree(_root);
            var logoButton = _root.Q<Button>(PackMakButtonName);
            logoButton.clickable.clicked += () => Application.OpenURL(PackageMakerToolsUrl);
            
            // Load Package Button
            var loadPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(LoadPackageButtonUxmlPath);
            loadPackageButtonVisualTree.CloneTree(_root);
            
            // Main Package
            var mainPackageVisualTree = Resources.Load<VisualTreeAsset>(MainPanelUxmlPath);
            mainPackageVisualTree.CloneTree(_root);
            var leftPanel = _root.Q<VisualElement>(LeftPanelName);
            var rightPanel = _root.Q<VisualElement>(RightPanelName);

            // Included Package Contents
            var includedPackageContentsVisualTree = Resources.Load<VisualTreeAsset>(IncludedPackageContentsUxmlPath);
            includedPackageContentsVisualTree.CloneTree(leftPanel);

            // Package Manifest
            var packageManifestVisualTree = Resources.Load<VisualTreeAsset>(PackageManifestUxmlPath);
            var packageManifestVisualElement = new VisualElement();
            packageManifestVisualTree.CloneTree(packageManifestVisualElement);
            _contentsView.Add(packageManifestVisualElement);
            
            // Readme
            var readmeVisualTree = Resources.Load<VisualTreeAsset>(ReadmeUxmlPath);
            var readmeVisualElement = new VisualElement();
            readmeVisualTree.CloneTree(readmeVisualElement);
            _contentsView.Add(readmeVisualElement);
            
            // Changelog
            var changelogVisualTree = Resources.Load<VisualTreeAsset>(ChangelogUxmlPath);
            var changelogVisualElement = new VisualElement();
            changelogVisualTree.CloneTree(changelogVisualElement);
            _contentsView.Add(changelogVisualElement);
            
            // License
            var licenseVisualTree = Resources.Load<VisualTreeAsset>(LicenseUxmlPath);
            var licenseVisualElement = new VisualElement();
            licenseVisualTree.CloneTree(licenseVisualElement);
            _contentsView.Add(licenseVisualElement);

            // Add contents to root
            _contentsView.style.flexGrow = 1.0f;
            rightPanel.Add(_contentsView);

            // Update Package Button
            var updatePackageButtonVisualTree = Resources.Load<VisualTreeAsset>(UpdatePackageButtonUxmlPath);
            updatePackageButtonVisualTree.CloneTree(_root);
            var updatePackageButton = _root.Q<Button>(UpdatePackageButtonName);
            updatePackageButton.SetEnabled(false);
            
            // Create Package Button
            var createPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(CreatePackageButtonUxmlPath);
            createPackageButtonVisualTree.CloneTree(_root);
        }
    }
}