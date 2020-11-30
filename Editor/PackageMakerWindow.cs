using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityPackageMaker.Editor.GuiConstants;
using Object = UnityEngine.Object;

namespace UnityPackageMaker.Editor 
{
    public class PackageMakerWindow : EditorWindow
    {
        private static readonly Vector2 PackageMakerWindowSize = new Vector2(900, 900);

        private VisualElement _root;
        private ScrollView _contentsView;
        private PackageManifest _packageManifest = new PackageManifest();
        
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
            
            // INIT
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
            
            // Readme
            // Changelog
            // License
            // Editor Folder
            // Runtime Folder
            // Tests Folder
            var testsFolderToggle = _root.Q<Toggle>(TestsFolderToggleName);

            // Tests Editor Folder
            var testsEditorFolderToggle = _root.Q<Toggle>(TestsEditorFolderToggleName);
            
            // Tests Runtime Folder
            var testsRuntimeFolderToggle = _root.Q<Toggle>(TestsRuntimeFolderToggleName);
            
            // Documentation
            // Samples
            // Screenshots
            
            // Display Name
            var displayNameTextField = _root.Q<TextField>(DisplayNameTextFieldName);
            var useDisplayNameAsRootFolderNameToggle = _root.Q<Toggle>(UseDisplayNameAsRootFolderNameToggleName);

            // Root Folder Name
            var rootFolderNameContentsVisualElement = _root.Q<VisualElement>(RootFolderNameContentsVisualElementName);
            var rootFolderNameTextField = _root.Q<TextField>(RootFolderNameTextFieldName);
            
            // Author Name
            var authorNameToggle = packageManifestVisualElement.Q<Toggle>(AuthorNameToggleName);
            var authorNameTextField = packageManifestVisualElement.Q<TextField>(AuthorNameTextFieldName);
            authorNameTextField.SetEnabled(authorNameToggle.value);
            authorNameToggle.RegisterValueChangedCallback(evt =>
            {
                authorNameTextField.SetEnabled(authorNameToggle.value);
            });
            
            // Author Email
            var authorEmailToggle = packageManifestVisualElement.Q<Toggle>(AuthorEmailToggleName);
            var authorEmailTextField = packageManifestVisualElement.Q<TextField>(AuthorEmailTextFieldName);
            authorEmailTextField.SetEnabled(authorEmailToggle.value);
            authorEmailToggle.RegisterValueChangedCallback(evt =>
            {
                authorEmailTextField.SetEnabled(authorEmailToggle.value);
            });
            
            // Author Url
            var authorUrlToggle = packageManifestVisualElement.Q<Toggle>(AuthorUrlToggleName);
            var authorUrlTextField = packageManifestVisualElement.Q<TextField>(AuthorUrlTextFieldName);
            authorUrlTextField.SetEnabled(authorUrlToggle.value);
            authorUrlToggle.RegisterValueChangedCallback(evt =>
            {
                authorUrlTextField.SetEnabled(authorUrlToggle.value);
            });
            
            // Unity Release
            var unityReleaseToggle = packageManifestVisualElement.Q<Toggle>(UnityReleaseToggleName);
            var unityReleaseTextField = packageManifestVisualElement.Q<TextField>(UnityReleaseTextFieldName);
            unityReleaseTextField.SetEnabled(unityReleaseToggle.value);
            unityReleaseToggle.RegisterValueChangedCallback(evt =>
            {
                unityReleaseTextField.SetEnabled(unityReleaseToggle.value);
            });
            
            // Dependencies
            var dependenciesToggle = packageManifestVisualElement.Q<Toggle>(DependenciesToggleName);
            var dependenciesContent = packageManifestVisualElement.Q<VisualElement>(DependenciesContentsVisualElementName);
            dependenciesContent.SetEnabled(dependenciesToggle.value);
            dependenciesToggle.RegisterValueChangedCallback(evt =>
            {
                dependenciesContent.SetEnabled(dependenciesToggle.value);
            });
            var dependenciesListView = packageManifestVisualElement.Q<ListView>(DependenciesListViewName);
            var dependencyEntryVisualTree = Resources.Load<VisualTreeAsset>(DependencyEntryUxmlPath);
            var addDependencyButton = packageManifestVisualElement.Q<Button>(AddDependencyButtonName);
            addDependencyButton.clickable.clicked += () =>
                AddEntryToDependencies(dependencyEntryVisualTree, dependenciesListView);
            
            // Keywords
            var keywordsToggle = packageManifestVisualElement.Q<Toggle>(KeywordsToggleName);
            var keywordsContent = packageManifestVisualElement.Q<VisualElement>(KeywordsContentsVisualElementName);
            keywordsContent.SetEnabled(keywordsToggle.value);
            keywordsToggle.RegisterValueChangedCallback(evt =>
            {
                keywordsContent.SetEnabled(keywordsToggle.value);
            });
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
            var createPackageButton = _root.Q<Button>(CreatePackageButtonName);

            // BINDING
            // Setup
            
            // Author Name
            authorNameTextField.RegisterCallback<ChangeEvent<string>>
                (e => _packageManifest.AuthorName = (e.target as TextField).value );

            // BEHAVIOR
            // Tests Folder
            testsFolderToggle.RegisterValueChangedCallback(evt =>
            {
                if (testsFolderToggle.value != false)
                {
                    return;
                }
                testsEditorFolderToggle.value = false;
                testsRuntimeFolderToggle.value = false;
            });

            // Tests Editor Folder
            testsEditorFolderToggle.RegisterValueChangedCallback(evt =>
            {
                if (testsEditorFolderToggle.value != true)
                {
                    return;
                }
                testsFolderToggle.value = true;
            });
            
            // Tests Runtime Folder
            testsRuntimeFolderToggle.RegisterValueChangedCallback(evt =>
            {
                if (testsRuntimeFolderToggle.value == true)
                {
                    testsFolderToggle.value = true;
                }
            });
            
            // Display Name
            rootFolderNameContentsVisualElement.SetEnabled(!useDisplayNameAsRootFolderNameToggle.value);
            useDisplayNameAsRootFolderNameToggle.RegisterValueChangedCallback(evt =>
            {
                if (useDisplayNameAsRootFolderNameToggle.value)
                {
                    rootFolderNameTextField.value = displayNameTextField.value;
                }
                else
                {
                    rootFolderNameTextField.value = String.Empty;
                }
                rootFolderNameContentsVisualElement.SetEnabled(!useDisplayNameAsRootFolderNameToggle.value);
            });
            
            displayNameTextField.RegisterValueChangedCallback(evt =>
            {
                if (useDisplayNameAsRootFolderNameToggle.value)
                {
                    rootFolderNameTextField.value = displayNameTextField.value;
                }
            });

            // Root Folder Name
            
            
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
            
            // Create Package Button
            createPackageButton.clickable.clicked += () => TryCreateNewUnityPackage(_packageManifest);
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

        private void TryCreateNewUnityPackage(PackageManifest packageManifest)
        {
            
            Debug.Log(packageManifest.AuthorName);
            
            return;
            // Validate
            if (!packageManifest.IsValidPackageManifest())
            {
                return;
            }
            
            // Get path
            var parentDirectoryPath = EditorUtility.OpenFolderPanel(CreatePackagesWindowTitle, "", "");
            if(String.IsNullOrWhiteSpace(parentDirectoryPath))
            {
                return;
            }
        }
    }
}