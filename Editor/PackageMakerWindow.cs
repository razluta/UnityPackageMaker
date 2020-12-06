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
        private PackageManifest _packageManifest;
        
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

            #region INIT AND QUERY

            // Header
            var headerVisualTree = Resources.Load<VisualTreeAsset>(HeaderUxmlPath);
            headerVisualTree.CloneTree(_root);
            var logoButton = _root.Q<Button>(PackMakButtonName);
            logoButton.clickable.clicked += () => Application.OpenURL(PackageMakerToolsUrl);

            // Load Package Button
            var loadPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(LoadPackageButtonUxmlPath);
            loadPackageButtonVisualTree.CloneTree(_root);
            var loadPackageButton = _root.Q<Button>(LoadPackageButtonName);
            loadPackageButton.clickable.clicked += LoadPackage;

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

            // Display Name
            var displayNameTextField = leftPanel.Q<TextField>(DisplayNameTextFieldName);
            var useDisplayNameAsRootFolderNameToggle = leftPanel.Q<Toggle>(UseDisplayNameAsRootFolderNameToggleName);

            // Root Folder Name
            var rootFolderNameContentsVisualElement =
                leftPanel.Q<VisualElement>(RootFolderNameContentsVisualElementName);
            var rootFolderNameTextField = leftPanel.Q<TextField>(RootFolderNameTextFieldName);

            // Readme
            var readmeToggle = leftPanel.Q<Toggle>(ReadmeToggleName);

            // Changelog
            var changelogToggle = leftPanel.Q<Toggle>(ChangelogToggleName);

            // License
            var licenseToggle = leftPanel.Q<Toggle>(LicenceToggleName);

            // Editor Folder
            var editorFolderToggle = leftPanel.Q<Toggle>(EditorFolderToggleName);

            // Runtime Folder
            var runtimeFolderToggle = leftPanel.Q<Toggle>(RuntimeFolderToggleName);

            // Tests Folder
            var testsFolderToggle = leftPanel.Q<Toggle>(TestsFolderToggleName);

            // Tests Editor Folder
            var testsEditorFolderToggle = leftPanel.Q<Toggle>(TestsEditorFolderToggleName);

            // Tests Runtime Folder
            var testsRuntimeFolderToggle = leftPanel.Q<Toggle>(TestsRuntimeFolderToggleName);

            // Documentation
            var documentationFolderToggle = leftPanel.Q<Toggle>(DocumentationFolderToggleName);

            // Samples
            var samplesFolderToggle = leftPanel.Q<Toggle>(SamplesFolderToggleName);

            // Screenshots
            var screenshotsFolderToggle = leftPanel.Q<Toggle>(ScreenshotsFolderToggleName);

            // Name
            var packageNameExtensionTextField =
                packageManifestVisualElement.Q<TextField>(PackageNameExtensionTextFieldName);
            var packageNameCompanyTextField =
                packageManifestVisualElement.Q<TextField>(PackageNameCompanyTextFieldName);
            var packageNameTextField =
                packageManifestVisualElement.Q<TextField>(PackageNameTextFieldName);

            // Version
            var packageVersionMajorIntegerField =
                packageManifestVisualElement.Q<IntegerField>(PackageVersionMajorIntegerFieldName);
            var packageVersionMinorIntegerField =
                packageManifestVisualElement.Q<IntegerField>(PackageVersionMinorIntegerFieldName);
            var packageVersionPatchIntegerField =
                packageManifestVisualElement.Q<IntegerField>(PackageVersionPatchIntegerFieldName);

            // Unity 
            var packageUnityVersionMajorIntegerField =
                packageManifestVisualElement.Q<IntegerField>(PackageUnityVersionMajorIntegerFieldName);
            var packageUnityVersionMinorIntegerField =
                packageManifestVisualElement.Q<IntegerField>(PackageUnityVersionMinorIntegerFieldName);

            // Description
            var packageUnityDescriptionTextField =
                packageManifestVisualElement.Q<TextField>(PackageDescriptionTextFieldName);

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
            var dependenciesContent =
                packageManifestVisualElement.Q<VisualElement>(DependenciesContentsVisualElementName);
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
            keywordsToggle.RegisterValueChangedCallback(evt => { keywordsContent.SetEnabled(keywordsToggle.value); });
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
            var changelogTextField = changelogVisualElement.Q<TextField>(ChangelogTextFieldName);

            // License
            var licenseVisualTree = Resources.Load<VisualTreeAsset>(LicenseUxmlPath);
            var licenseVisualElement = new VisualElement();
            licenseVisualTree.CloneTree(licenseVisualElement);
            _contentsView.Add(licenseVisualElement);
            var licenseTextField = licenseVisualElement.Q<TextField>(LicenseTextFieldName);

            // Add contents to root
            _contentsView.style.flexGrow = 1.0f;
            rightPanel.Add(_contentsView);

            // Add console to root
            // var consoleLogVisualTree = Resources.Load<VisualTreeAsset>(ConsoleLogUxmlPath);
            // consoleLogVisualTree.CloneTree(_root);

            // Update Package Button
            var updatePackageButtonVisualTree = Resources.Load<VisualTreeAsset>(UpdatePackageButtonUxmlPath);
            updatePackageButtonVisualTree.CloneTree(_root);
            var updatePackageButton = _root.Q<Button>(UpdatePackageButtonName);
            updatePackageButton.SetEnabled(false);

            // Create Package Button
            var createPackageButtonVisualTree = Resources.Load<VisualTreeAsset>(CreatePackageButtonUxmlPath);
            createPackageButtonVisualTree.CloneTree(_root);
            var createPackageButton = _root.Q<Button>(CreatePackageButtonName);

            #endregion

            #region BINDINGS

            // Setup
            _packageManifest = ScriptableObject.CreateInstance<PackageManifest>();
            var pmSerObj = new UnityEditor.SerializedObject(_packageManifest);

            // Display Name
            var displayNameProperty = pmSerObj.FindProperty(PackageManifestConstants.DisplayNamePropName);
            if (displayNameProperty != null)
            {
                displayNameTextField.BindProperty(displayNameProperty);
            }

            var isUseDisplayNameAsRootFolderNameProperty =
                pmSerObj.FindProperty(PackageManifestConstants.IsUseDisplayNameAsRootFolderNamePropName);
            if (isUseDisplayNameAsRootFolderNameProperty != null)
            {
                useDisplayNameAsRootFolderNameToggle.BindProperty(isUseDisplayNameAsRootFolderNameProperty);
            }

            // Root Folder Name
            var rootFolderNameProperty = pmSerObj.FindProperty(PackageManifestConstants.RootFolderNamePropName);
            if (rootFolderNameProperty != null)
            {
                rootFolderNameTextField.BindProperty(rootFolderNameProperty);
            }

            // Readme
            var hasReadmeProperty = pmSerObj.FindProperty(PackageManifestConstants.HasReadmePropName);
            if (hasReadmeProperty != null)
            {
                readmeToggle.BindProperty(hasReadmeProperty);
            }

            // Changelog
            var hasChangelogProperty = pmSerObj.FindProperty(PackageManifestConstants.HasChangelogPropName);
            if (hasChangelogProperty != null)
            {
                changelogToggle.BindProperty(hasChangelogProperty);
            }

            // License
            var hasLicenseProperty = pmSerObj.FindProperty(PackageManifestConstants.HasLicensePropName);
            if (hasLicenseProperty != null)
            {
                licenseToggle.BindProperty(hasLicenseProperty);
            }

            // Editor Folder
            var hasEditorFolderProperty = pmSerObj.FindProperty(PackageManifestConstants.HasEditorFolderPropName);
            if (hasEditorFolderProperty != null)
            {
                editorFolderToggle.BindProperty(hasEditorFolderProperty);
            }

            // Runtime Folder
            var hasRuntimeFolderProperty = pmSerObj.FindProperty(PackageManifestConstants.HasRuntimeFolderPropName);
            if (hasRuntimeFolderProperty != null)
            {
                runtimeFolderToggle.BindProperty(hasRuntimeFolderProperty);
            }

            // Tests Folder
            var hasTestsFolderProperty = pmSerObj.FindProperty(PackageManifestConstants.HasTestsFolderPropName);
            if (hasTestsFolderProperty != null)
            {
                testsFolderToggle.BindProperty(hasTestsFolderProperty);
            }

            // Tests Editor Folder
            var hasTestsEditorFolderProperty =
                pmSerObj.FindProperty(PackageManifestConstants.HasTestsEditorFolderPropName);
            if (hasTestsEditorFolderProperty != null)
            {
                testsEditorFolderToggle.BindProperty(hasTestsEditorFolderProperty);
            }

            // Tests Runtime Folder
            var hasTestsRuntimeFolderProperty =
                pmSerObj.FindProperty(PackageManifestConstants.HasTestsRuntimeFolderPropName);
            if (hasTestsRuntimeFolderProperty != null)
            {
                testsRuntimeFolderToggle.BindProperty(hasTestsRuntimeFolderProperty);
            }

            // Documentation Folder
            var hasDocumentationFolderProperty =
                pmSerObj.FindProperty(PackageManifestConstants.HasDocumentationFolderPropName);
            if (hasDocumentationFolderProperty != null)
            {
                documentationFolderToggle.BindProperty(hasDocumentationFolderProperty);
            }

            // Samples Folder
            var hasSamplesFolderProperty = pmSerObj.FindProperty(PackageManifestConstants.HasSamplesFolderPropName);
            if (hasSamplesFolderProperty != null)
            {
                samplesFolderToggle.BindProperty(hasSamplesFolderProperty);
            }
            
            // Screenshots Folder
            var hasScreenshotsFolderProperty =
                pmSerObj.FindProperty(PackageManifestConstants.HasScreenshotsFolderPropName);
            if (hasScreenshotsFolderProperty != null)
            {
                screenshotsFolderToggle.BindProperty(hasScreenshotsFolderProperty);
            }
            
            // Name
            var nameExtensionProperty = pmSerObj.FindProperty(PackageManifestConstants.NameExtensionPropName);
            if (nameExtensionProperty != null)
            {
                packageNameExtensionTextField.BindProperty(nameExtensionProperty);
            }

            var nameCompanyProperty = pmSerObj.FindProperty(PackageManifestConstants.NameCompanyPropName);
            if (nameCompanyProperty != null)
            {
                packageNameCompanyTextField.BindProperty(nameCompanyProperty);
            }

            var namePackageProperty = pmSerObj.FindProperty(PackageManifestConstants.NamePackagePropName);
            if (namePackageProperty != null)
            {
                packageNameTextField.BindProperty(namePackageProperty);
            }
            
            // Version
            var versionMajorProperty = pmSerObj.FindProperty(PackageManifestConstants.VersionMajorPropName);
            if (versionMajorProperty != null)
            {
                packageVersionMajorIntegerField.BindProperty(versionMajorProperty);
            }

            var versionMinorProperty = pmSerObj.FindProperty(PackageManifestConstants.VersionMinorPropName);
            if (versionMinorProperty != null)
            {
                packageVersionMinorIntegerField.BindProperty(versionMinorProperty);
            }

            var versionPatchProperty = pmSerObj.FindProperty(PackageManifestConstants.VersionPatchPropName);
            if (versionPatchProperty != null)
            {
                packageVersionPatchIntegerField.BindProperty(versionPatchProperty);
            }
            
            // Unity Version
            var unityVersionMajorProperty = pmSerObj.FindProperty(PackageManifestConstants.UnityVersionMajorPropName);
            if (unityVersionMajorProperty != null)
            {
                packageUnityVersionMajorIntegerField.BindProperty(unityVersionMajorProperty);
            }

            var unityVersionMinorProperty = pmSerObj.FindProperty(PackageManifestConstants.UnityVersionMinorPropName);
            if (unityVersionMinorProperty != null)
            {
                packageUnityVersionMinorIntegerField.BindProperty(unityVersionMinorProperty);
            }
            
            // Description
            var descriptionProperty = pmSerObj.FindProperty(PackageManifestConstants.DescriptionPropName);
            if (descriptionProperty != null)
            {
                packageUnityDescriptionTextField.BindProperty(descriptionProperty);
            }
            
            // Author Name
            var hasAuthorNameProperty = pmSerObj.FindProperty(PackageManifestConstants.HasAuthorNamePropName);
            if (hasAuthorNameProperty != null)
            {
                authorNameToggle.BindProperty(hasAuthorNameProperty);
            }
            
            var authorNameProperty = pmSerObj.FindProperty(PackageManifestConstants.AuthorNamePropName);
            if (authorNameProperty != null)
            {
                authorNameTextField.BindProperty(authorNameProperty);
            }
            
            // Author Email
            var hasAuthorEmailProperty = pmSerObj.FindProperty(PackageManifestConstants.HasAuthorEmailPropName);
            if (hasAuthorEmailProperty != null)
            {
                authorEmailToggle.BindProperty(hasAuthorEmailProperty);
            }

            var authorEmailProperty = pmSerObj.FindProperty(PackageManifestConstants.AuthorEmailPropName);
            if (authorEmailProperty != null)
            {
                authorEmailTextField.BindProperty(authorEmailProperty);
            }
            
            // Author Url
            var hasAuthorUrlProperty = pmSerObj.FindProperty(PackageManifestConstants.HasAuthorUrlPropName);
            if (hasAuthorUrlProperty != null)
            {
                authorUrlToggle.BindProperty(hasAuthorUrlProperty);
            }

            var authorUrlProperty = pmSerObj.FindProperty(PackageManifestConstants.AuthorUrlPropName);
            if (authorUrlProperty != null)
            {
                authorUrlTextField.BindProperty(authorUrlProperty);
            }

            // Unity Release
            var hasUnityReleaseProperty = pmSerObj.FindProperty(PackageManifestConstants.HasUnityReleasePropName);
            if (hasUnityReleaseProperty != null)
            {
                unityReleaseToggle.BindProperty(hasUnityReleaseProperty);
            }

            var unityReleaseProperty = pmSerObj.FindProperty(PackageManifestConstants.UnityReleasePropName);
            if (unityReleaseProperty != null)
            {
                unityReleaseTextField.BindProperty(unityReleaseProperty);
            }
            
            // Dependencies
            var hasDependenciesProperty = pmSerObj.FindProperty(PackageManifestConstants.HasDependenciesPropName);
            if (hasDependenciesProperty != null)
            {
                dependenciesToggle.BindProperty(hasDependenciesProperty);
            }

            var dependenciesProperty = pmSerObj.FindProperty(PackageManifestConstants.DependenciesPropName);
            if (dependenciesProperty != null)
            {
                // TODO: bind
                // dependenciesListView.BindProperty(dependenciesProperty);
            }

            // Keywords
            var hasKeywordsProperty = pmSerObj.FindProperty(PackageManifestConstants.HasKeywordsPropName);
            if (hasKeywordsProperty != null)
            {
                keywordsToggle.BindProperty(hasKeywordsProperty);
            }

            var keywordsProperty = pmSerObj.FindProperty(PackageManifestConstants.KeywordsPropName);
            if (keywordsProperty != null)
            {
                // TODO: bind
                // keywordsListView.BindProperty(keywordsProperty);
            }
            
            // Readme Contents
            var readmeProperty = pmSerObj.FindProperty(PackageManifestConstants.ReadmePropName);
            if (readmeProperty != null)
            {
                readmeTextField.BindProperty(readmeProperty);
            }
            
            // Changelog Contents
            var changelogProperty = pmSerObj.FindProperty(PackageManifestConstants.ChangelogPropName);
            if (changelogProperty != null)
            {
                changelogTextField.BindProperty(changelogProperty);
            }
            
            // Licence Contents
            var licenseProperty = pmSerObj.FindProperty(PackageManifestConstants.LicensePropName);
            if (licenseProperty != null)
            {
                licenseTextField.BindProperty(licenseProperty);
            }
            #endregion

            #region BEHAVIOR
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
            #endregion
        }

        private void LoadPackage()
        {
            // Get path
            var parentDirectoryPath = EditorUtility.OpenFolderPanel(LoadPackageWindowTitle, "", "");
            if(String.IsNullOrWhiteSpace(parentDirectoryPath))
            {
                return;
            }
        }

        private void AddEntryToDependencies(VisualTreeAsset vta, VisualElement ve)
        {
            var customVisualElement = new VisualElement();
            vta.CloneTree(customVisualElement);
            ve.Add(customVisualElement);

            var entryNameTextField = customVisualElement.Q<TextField>(DependencyEntryNameTextFieldName);
            entryNameTextField.value = String.Empty;
            // TODO: bind
            
            var entryVersionTextField = customVisualElement.Q<TextField>(DependencyEntryVersionTextFieldName);
            entryVersionTextField.value = String.Empty;
            // TODO: bind
            
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
            // bind
            
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