using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityPackageMaker.Editor.GuiConstants;

namespace UnityPackageMaker.Editor 
{
    public class PackageMakerWindow : EditorWindow
    {
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
            
            // Declaration
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
            var readmeToggle = leftPanel.Q<Toggle>(ReadmeToggleName);
            var changelogToggle = leftPanel.Q<Toggle>(ChangelogToggleName);
            var licenseToggle = leftPanel.Q<Toggle>(LicenceToggleName);

            // Package Manifest
            var packageManifestVisualTree = Resources.Load<VisualTreeAsset>(PackageManifestUxmlPath);
            var packageManifestVisualElement = new VisualElement();
            packageManifestVisualTree.CloneTree(packageManifestVisualElement);
            _contentsView.Add(packageManifestVisualElement);
            var dependenciesListView = packageManifestVisualElement.Q<ListView>(DependenciesListViewName);
            var dependencyEntryVisualTree = Resources.Load<VisualTreeAsset>(DependencyEntryUxmlPath);
            var addDependencyButton = packageManifestVisualElement.Q<Button>(AddDependencyButtonName);
            addDependencyButton.clickable.clicked += () =>
                AddEntryToDependencies(dependencyEntryVisualTree, dependenciesListView);
            var keywordsListView = packageManifestVisualElement.Q<ListView>(KeywordsListViewName);
            var keywordsEntryVisualTree = Resources.Load<VisualTreeAsset>(KeywordEntryUxmlPath);
            var addKeywordButton = packageManifestVisualElement.Q<Button>(AddKeywordButtonName);
            addKeywordButton.clickable.clicked += () =>
                AddEntryToKeywords(keywordsEntryVisualTree, keywordsListView);

            // Readme
            var readmeVisualTree = Resources.Load<VisualTreeAsset>(ReadmeUxmlPath);
            var readmeVisualElement = new VisualElement();
            readmeVisualTree.CloneTree(readmeVisualElement);
            _contentsView.Add(readmeVisualElement);
            var readmeTextField = readmeVisualElement.Q<TextField>(ReadmeTextFieldName);
            
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

            // Add console to root
            var consoleLogVisualTree = Resources.Load<VisualTreeAsset>(ConsoleLogUxmlPath);
            consoleLogVisualTree.CloneTree(_root);

            // Update Package Button
            var updatePackageButtonVisualTree = Resources.Load<VisualTreeAsset>(UpdatePackageButtonUxmlPath);
            updatePackageButtonVisualTree.CloneTree(_root);
            var updatePackageButton = _root.Q<Button>(UpdatePackageButtonName);
            updatePackageButton.SetEnabled(false);
            
            // Create Package Button
            var createPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(CreatePackageButtonUxmlPath);
            createPackageButtonVisualTree.CloneTree(_root);

            // Behavior
            // Readme
            readmeVisualElement.SetEnabled(readmeToggle.value);
            readmeToggle.RegisterValueChangedCallback(evt =>
            {
                readmeVisualElement.SetEnabled(readmeToggle.value);
            });
            
            // Changelog
            changelogVisualElement.SetEnabled(changelogToggle.value);
            changelogToggle.RegisterValueChangedCallback(evt =>
            {
                changelogVisualElement.SetEnabled(changelogToggle.value);
            });
            
            // Licence
            licenseVisualElement.SetEnabled(licenseToggle.value);
            licenseToggle.RegisterValueChangedCallback(evt =>
            {
                licenseVisualElement.SetEnabled(licenseToggle.value);
            });
        }

        private void AddEntryToDependencies(VisualTreeAsset vta, VisualElement ve)
        {
            var customVisualElement = new VisualElement();
            vta.CloneTree(customVisualElement);
            ve.Add(customVisualElement);

            var entryNameTextField = customVisualElement.Q<TextField>(DependencyEntryNameTextFieldName);
            entryNameTextField.value = String.Empty;
            var entryVersionTextField = customVisualElement.Q<TextField>(DependencyEntryVersionTextFieldName);
            entryVersionTextField.value = String.Empty;
            var removeButton = customVisualElement.Q<Button>(DependencyEntryRemoveButtonName);
            removeButton.clickable.clicked += () => ve.Remove(customVisualElement);
        }

        private void AddEntryToKeywords(VisualTreeAsset vta, VisualElement ve)
        {
            var customVisualElement = new VisualElement();
            vta.CloneTree(customVisualElement);
            ve.Add(customVisualElement);

            var entryNameTextField = customVisualElement.Q<TextField>(KeywordEntryNameTextFieldName);
            entryNameTextField.value = String.Empty;
            var removeButton = customVisualElement.Q<Button>(RemoveKeywordButtonName);
            removeButton.clickable.clicked += () => ve.Remove(customVisualElement);
        }
    }
}