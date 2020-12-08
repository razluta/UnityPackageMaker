using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityPackageMaker.Editor.SystemUtilities;
using static UnityPackageMaker.Editor.GuiConstants;
using Object = UnityEngine.Object;

namespace UnityPackageMaker.Editor 
{
    public class PackageMakerWindow : EditorWindow
    {
        private static readonly Vector2 PackageMakerWindowSize = new Vector2(1400, 900);

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

            // Main Package
            var mainPackageVisualTree = Resources.Load<VisualTreeAsset>(MainPanelUxmlPath);
            mainPackageVisualTree.CloneTree(_root);
            var leftPanel = _root.Q<VisualElement>(LeftPanelName);
            var centerPanel = _root.Q<VisualElement>(CenterPanelName);
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
            readmeVisualElement.style.flexGrow = 1;
            rightPanel.Add(readmeVisualElement);
            var readmeTextField = readmeVisualElement.Q<TextField>(ReadmeTextFieldName);

            // Changelog
            var changelogVisualTree = Resources.Load<VisualTreeAsset>(ChangelogUxmlPath);
            var changelogVisualElement = new VisualElement();
            changelogVisualTree.CloneTree(changelogVisualElement);
            changelogVisualElement.style.flexGrow = 1;
            rightPanel.Add(changelogVisualElement);
            var changelogTextField = changelogVisualElement.Q<TextField>(ChangelogTextFieldName);

            // License
            var licenseVisualTree = Resources.Load<VisualTreeAsset>(LicenseUxmlPath);
            var licenseVisualElement = new VisualElement();
            licenseVisualTree.CloneTree(licenseVisualElement);
            licenseVisualElement.style.flexGrow = 1;
            rightPanel.Add(licenseVisualElement);
            var licenseTextField = licenseVisualElement.Q<TextField>(LicenseTextFieldName);

            // Add contents to root
            _contentsView.style.flexGrow = 1.0f;
            centerPanel.Add(_contentsView);

            // Clear All Button
            var clearAllButtonVisualTree = Resources.Load<VisualTreeAsset>(ClearALlButtonUxmlPath);
            clearAllButtonVisualTree.CloneTree(_root);
            var clearAllButton = _root.Q<Button>(ClearAllButtonName);
            
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
            // Load Button
            loadPackageButton.clickable.clicked += () => LoadPackage(updatePackageButton);
            
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
            // The user can't change the folder name in the Update Mode - they have to click Create New instead.
            rootFolderNameTextField.RegisterValueChangedCallback(evt =>
            {
                if (!Directory.Exists(_packageManifest.PackageAbsolutePath))
                {
                    return;
                }
                
                var directoryName = new DirectoryInfo(_packageManifest.PackageAbsolutePath).Name;
                updatePackageButton.SetEnabled(rootFolderNameTextField.value == directoryName);
            });
            
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
            
            // Clear All Button
            clearAllButton.clickable.clicked += _packageManifest.ResetToDefault;
            
            // Update Package Button
            updatePackageButton.clickable.clicked += () => 
                TryCreateNewUnityPackage(_packageManifest, _packageManifest.PackageAbsolutePath);
            
            // Create Package Button
            createPackageButton.clickable.clicked += () => 
                TryCreateNewUnityPackage(_packageManifest);
            #endregion
        }

        private void LoadPackage(Button updatePackageButton)
        {
            // Clear the package manifest
            _packageManifest.ResetToDefault();
            
            // Get path
            var parentDirectoryPath = EditorUtility.OpenFolderPanel(LoadPackageWindowTitle, "", "");
            if(String.IsNullOrWhiteSpace(parentDirectoryPath))
            {
                return;
            }
            _packageManifest.PackageAbsolutePath = parentDirectoryPath;
            
            // package.json
            #region package.json
            // Package Reading
            var packageJsonPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.JsonFileName);

            if (!File.Exists(packageJsonPath))
            {
                EditorUtility.DisplayDialog(NoPackageManifestTitle, NoPackageManifestMessage, NoPackageManifestOk);
                return;
            }
            
            var dictionary = JsonUtilities.GetData(packageJsonPath);
            if (dictionary == null)
            {
                return;
            }

            // Display Name
            var displayName = string.Empty;
            if (dictionary.ContainsKey(PackageManifestConstants.JsonDisplayName))
            {
                displayName = (string) dictionary[PackageManifestConstants.JsonDisplayName];
            }
            _packageManifest.DisplayName = displayName;

            // Root Folder Name
            var rootFolderName = new DirectoryInfo(parentDirectoryPath).Name;
            _packageManifest.RootFolderName = rootFolderName;
            _packageManifest.IsUseDisplayNameAsRootFolderName = displayName == rootFolderName;

            // Name
            var fullName = string.Empty;
            var nameExtension = string.Empty;
            var nameCompany = string.Empty;
            var namePackage = string.Empty;
            
            if (dictionary.ContainsKey(PackageManifestConstants.JsonName))
            {
                fullName = (string) dictionary[PackageManifestConstants.JsonName];
            }
            
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var namePieces = fullName.Split(Period);
                
                // TODO: Validate contents
                
                nameExtension = namePieces[0];
                nameCompany = namePieces[1];
                namePackage = namePieces[2];
            }

            _packageManifest.NameExtension = nameExtension;
            _packageManifest.NameCompany = nameCompany;
            _packageManifest.NamePackage = namePackage;

            // Version
            var fullVersion = string.Empty;
            var versionMajor = string.Empty;
            var versionMinor = string.Empty;
            var versionPatch = string.Empty;

            if (dictionary.ContainsKey(PackageManifestConstants.JsonVersion))
            {
                fullVersion = (string) dictionary[PackageManifestConstants.JsonVersion];
            }

            if (!string.IsNullOrWhiteSpace(fullVersion))
            {
                var versionPieces = fullVersion.Split(Period);
                
                // TODO: Validate contents

                versionMajor = versionPieces[0];
                versionMinor = versionPieces[1];
                versionPatch = versionPieces[2];
            }

            _packageManifest.VersionMajor = int.Parse(versionMajor);
            _packageManifest.VersionMinor = int.Parse(versionMinor);
            _packageManifest.VersionPatch = int.Parse(versionPatch);

            // Unity Version
            var fullUnityVersion = string.Empty;
            var unityVersionMajor = string.Empty;
            var unityVersionMinor = string.Empty;

            if (dictionary.ContainsKey(PackageManifestConstants.JsonUnity))
            {
                fullUnityVersion = (string) dictionary[PackageManifestConstants.JsonUnity];
            }
            
            if (!string.IsNullOrWhiteSpace(fullUnityVersion))
            {
                var unityVersionPieces = fullUnityVersion.Split(Period);
                
                // TODO: Validate contents

                unityVersionMajor = unityVersionPieces[0];
                unityVersionMinor = unityVersionPieces[1];
            }

            _packageManifest.UnityVersionMajor = int.Parse(unityVersionMajor);
            _packageManifest.UnityVersionMinor = int.Parse(unityVersionMinor);

            // Description
            var description = string.Empty;
            if (dictionary.ContainsKey(PackageManifestConstants.JsonDescription))
            {
                description = (string) dictionary[PackageManifestConstants.JsonDescription];
            }

            _packageManifest.Description = description;

            // Author
            if (dictionary.ContainsKey(PackageManifestConstants.JsonAuthor))
            {
                var authorContents = dictionary[PackageManifestConstants.JsonAuthor];

                var authorDictionary = new Dictionary<string, string>();
                try
                {
                    var serializedAuthorContents = JsonConvert.SerializeObject(authorContents);
                    authorDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedAuthorContents);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

                if (authorDictionary != null)
                {
                    // Author Name
                    if (authorDictionary.ContainsKey(PackageManifestConstants.JsonAuthorName))
                    {
                        var authorName = authorDictionary[PackageManifestConstants.JsonAuthorName];
                        _packageManifest.HasAuthorName = true;
                        _packageManifest.AuthorName = authorName;
                    }
                    
                    // Author Email
                    if (authorDictionary.ContainsKey(PackageManifestConstants.JsonAuthorEmail))
                    {
                        var authorEmail = authorDictionary[PackageManifestConstants.JsonAuthorEmail];
                        _packageManifest.HasAuthorEmail = true;
                        _packageManifest.AuthorEmail = authorEmail;
                    }
                    
                    // Author Url
                    if (authorDictionary.ContainsKey(PackageManifestConstants.JsonAuthorUrl))
                    {
                        var authorUrl = authorDictionary[PackageManifestConstants.JsonAuthorUrl];
                        _packageManifest.HasAuthorUrl = true;
                        _packageManifest.AuthorUrl = authorUrl;
                    }
                }
            }

            // Unity Release
            if (dictionary.ContainsKey(PackageManifestConstants.JsonUnityRelease))
            {
                var unityRelease = (string) dictionary[PackageManifestConstants.JsonUnityRelease];
                _packageManifest.HasUnityRelease = true;
                _packageManifest.UnityRelease = unityRelease;
            }
            
            // Dependencies
            if (dictionary.ContainsKey(PackageManifestConstants.JsonDependencies))
            {
                // var dependencies = (PackageDependency) dictionary[PackageManifestConstants.JsonDependencies];
                _packageManifest.HasDependencies = true;
                // TODO: Figure out dependencies de-serialization after the serialization part is done.
                //_packageManifest.Dependencies = dependencies;
            }

            // Keywords
            if (dictionary.ContainsKey(PackageManifestConstants.JsonKeywords))
            {
                // var keywords = ...
                _packageManifest.HasKeywords = true;
                // TODO: Figure out keywords de-serialization after the serialization part is done
                //_packageManifest.Keywords = keywords;
            }
            #endregion

            // README.MD
            var readmeMdPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.ReadmeMdFileName);
            if (File.Exists(readmeMdPath))
            {
                var streamReader = new StreamReader(readmeMdPath);
                var readme = streamReader.ReadToEnd();
                _packageManifest.HasReadme = true;
                _packageManifest.Readme = readme;
            }
            else
            {
                _packageManifest.HasReadme = false;
            }

            // CHANGELOG.MD
            var changelogMdPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.ChangelogMdFileName);
            if (File.Exists(changelogMdPath))
            {
                var streamReader = new StreamReader(changelogMdPath);
                var changelog = streamReader.ReadToEnd();
                _packageManifest.HasChangelog = true;
                _packageManifest.Changelog = changelog;
            }
            else
            {
                _packageManifest.HasChangelog = false;
            }

            // LICENSE
            var licensePath = Path.Combine(parentDirectoryPath, PackageManifestConstants.LicenseFileName);
            if (File.Exists(licensePath))
            {
                var streamReader = new StreamReader(licensePath);
                var license = streamReader.ReadToEnd();
                _packageManifest.HasLicense = true;
                _packageManifest.License = license;
            }
            else
            {
                _packageManifest.HasLicense = false;
            }

            // Editor Folder
            var editorFolderPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.EditorFolderName);
            _packageManifest.HasEditorFolder = Directory.Exists(editorFolderPath);

            // Runtime Folder
            var runtimeFolderPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.RuntimeFolderName);
            _packageManifest.HasRuntimeFolder = Directory.Exists(runtimeFolderPath);

            // Tests Folder
            var testsFolderPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.TestsFolderName);
            if (Directory.Exists(testsFolderPath))
            {
                _packageManifest.HasTestsFolder = true;
                
                // Tests Editor Folder
                var testsEditorFolderPath =
                    Path.Combine(testsFolderPath, PackageManifestConstants.TestsEditorFolderName);
                _packageManifest.HasTestsEditorFolder = Directory.Exists(testsEditorFolderPath);

                // Tests Runtime Folder
                var testsRuntimeFolderPath =
                    Path.Combine(testsFolderPath, PackageManifestConstants.TestsRuntimeFolderName);
                _packageManifest.HasTestsRuntimeFolder = Directory.Exists(testsRuntimeFolderPath);
            }
            
            // Documentation Folder
            var documentationFolderPath =
                Path.Combine(parentDirectoryPath, PackageManifestConstants.DocumentationFolderName);
            _packageManifest.HasDocumentationFolder = Directory.Exists(documentationFolderPath);

            // Samples Folder
            var samplesFolderPath = Path.Combine(parentDirectoryPath, PackageManifestConstants.SamplesFolderName);
            _packageManifest.HasSamplesFolder = Directory.Exists(samplesFolderPath);

            // Screenshots Folder
            var screenshotsFolderPath =
                Path.Combine(parentDirectoryPath, PackageManifestConstants.ScreenshotsFolderName);
            _packageManifest.HasScreenshotsFolder = Directory.Exists(screenshotsFolderPath);
            
            // Enable Update Button
            updatePackageButton.SetEnabled(true);
        }

        private static void AddEntryToDependencies(VisualTreeAsset vta, VisualElement ve)
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

        private static void AddEntryToKeywords(VisualTreeAsset vta, VisualElement ve)
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

        private static void TryCreateNewUnityPackage(PackageManifest packageManifest, string rootFolderPath="")
        {
            // Validate
            if (!packageManifest.IsValidPackageManifest())
            {
                EditorUtility.DisplayDialog(InvalidPackageErrorTitle, InvalidPackageErrorMessage, InvalidPackageOk);
                return;
            }

            // Get path if it doesn't exists
            if (string.IsNullOrWhiteSpace(rootFolderPath))
            {
                var parentDirectoryPath = EditorUtility.OpenFolderPanel(CreatePackagesWindowTitle, "", "");
                if(String.IsNullOrWhiteSpace(parentDirectoryPath))
                {
                    return;
                }
                
                // Create Root Folder
                rootFolderPath = Path.GetFullPath(Path.Combine(parentDirectoryPath, packageManifest.RootFolderName));
                if (Directory.Exists(rootFolderPath))
                {
                    var isOverride = EditorUtility.DisplayDialog(OverridePackageTitle, OverridePackageMessage, 
                        OverrideYes,
                        OverrideCancel);
                    if (!isOverride)
                    {
                        return; 
                    }
                }
                Directory.CreateDirectory(rootFolderPath);
            }

            // package.json
            var packageJsonFilePath = Path.Combine(rootFolderPath, PackageManifestConstants.JsonFileName);
            var packageDictionary = new Dictionary<string, object>();
            
            var packageName = 
                packageManifest.NameExtension + Period + 
                packageManifest.NameCompany + Period + 
                packageManifest.NamePackage;
            packageDictionary[PackageManifestConstants.JsonName] = packageName;

            var packageVersion =
                packageManifest.VersionMajor.ToString() + Period +
                packageManifest.VersionMinor.ToString() + Period +
                packageManifest.VersionPatch.ToString();
            packageDictionary[PackageManifestConstants.JsonVersion] = packageVersion;

            var packageDisplayName = packageManifest.DisplayName;
            packageDictionary[PackageManifestConstants.JsonDisplayName] = packageDisplayName;

            var packageDescription = packageManifest.Description;
            packageDictionary[PackageManifestConstants.JsonDescription] = packageDescription;

            var packageUnity =
                packageManifest.UnityVersionMajor.ToString() + Period +
                packageManifest.UnityVersionMinor.ToString();
            packageDictionary[PackageManifestConstants.JsonUnity] = packageUnity;

            var packageUnityRelease = packageManifest.UnityRelease;
            packageDictionary[PackageManifestConstants.JsonUnityRelease] = packageUnityRelease;

            var dependencies = packageManifest.Dependencies;
            packageDictionary[PackageManifestConstants.JsonDependencies] = dependencies;

            var keywords = packageManifest.Keywords;
            packageDictionary[PackageManifestConstants.JsonKeywords] = keywords;

            var author = new Dictionary<string, string>
            {
                [PackageManifestConstants.JsonAuthorName] = packageManifest.AuthorName,
                [PackageManifestConstants.JsonAuthorEmail] = packageManifest.AuthorEmail,
                [PackageManifestConstants.JsonAuthorUrl] = packageManifest.AuthorUrl
            };
            packageDictionary[PackageManifestConstants.JsonAuthor] = author;
            
            JsonUtilities.SetData(packageDictionary, packageJsonFilePath);
            
            // README.MD
            if (packageManifest.HasReadme)
            {
                var readmeMdFilePath = Path.Combine(rootFolderPath, PackageManifestConstants.ReadmeMdFileName);
                var readmeWriter = File.CreateText(readmeMdFilePath);
                readmeWriter.Write(packageManifest.Readme);
                readmeWriter.Close();
            }

            // CHANGELOG.MD
            if (packageManifest.HasChangelog)
            {
                var changelogMdFilePath = Path.Combine(rootFolderPath, PackageManifestConstants.ChangelogMdFileName);
                var changelogWriter = File.CreateText(changelogMdFilePath);
                changelogWriter.Write(packageManifest.Changelog);
                changelogWriter.Close();
            }

            // LICENSE
            if (packageManifest.HasLicense)
            {
                var licenseFilePath = Path.Combine(rootFolderPath, PackageManifestConstants.LicenseFileName);
                var licenseWriter = File.CreateText(licenseFilePath);
                licenseWriter.Write(packageManifest.License);
                licenseWriter.Close();
            }
            
            // Folders
            if (packageManifest.HasEditorFolder)
            {
                Directory.CreateDirectory(Path.Combine(rootFolderPath, PackageManifestConstants.EditorFolderName));
                var editorFolderNullFile = File.Create(Path.Combine(rootFolderPath, PackageManifestConstants.EditorFolderName,
                    PackageManifestConstants.EmptyFileName));
                editorFolderNullFile.Close();
            }
            
            if (packageManifest.HasRuntimeFolder)
            {
                Directory.CreateDirectory(Path.Combine(rootFolderPath, PackageManifestConstants.RuntimeFolderName));
                var runtimeFolderNullFile = File.Create(Path.Combine(rootFolderPath, PackageManifestConstants.RuntimeFolderName,
                    PackageManifestConstants.EmptyFileName));
                runtimeFolderNullFile.Close();
            }
            
            if (packageManifest.HasTestsFolder)
            {
                var testsFolderPath = Path.Combine(rootFolderPath, PackageManifestConstants.TestsFolderName);
                Directory.CreateDirectory(testsFolderPath);
                var testsFolderNullFile = File.Create(Path.Combine(testsFolderPath, PackageManifestConstants.EmptyFileName));
                testsFolderNullFile.Close();
                
                if (packageManifest.HasTestsEditorFolder)
                {
                    var testsEditorFolderPath =
                        Path.Combine(testsFolderPath, PackageManifestConstants.TestsEditorFolderName);
                    Directory.CreateDirectory(testsEditorFolderPath);
                    var testsEditorFolderNullFile = 
                        File.Create(Path.Combine(testsEditorFolderPath, PackageManifestConstants.EmptyFileName));
                    testsEditorFolderNullFile.Close();
                }
                
                if (packageManifest.HasTestsRuntimeFolder)
                {
                    var testsRuntimeFolderPath =
                        Path.Combine(rootFolderPath, PackageManifestConstants.TestsRuntimeFolderName);
                    Directory.CreateDirectory(testsRuntimeFolderPath);
                    var testsRuntimeFolderNullFile = 
                        File.Create(Path.Combine(testsRuntimeFolderPath, PackageManifestConstants.EmptyFileName));
                    testsRuntimeFolderNullFile.Close();
                }
            }
            
            if (packageManifest.HasDocumentationFolder)
            {
                var documentationFolderPath =
                    Path.Combine(rootFolderPath, PackageManifestConstants.DocumentationFolderName);
                Directory.CreateDirectory(documentationFolderPath);
                var documentationFolderNullFile = 
                    File.Create(Path.Combine(documentationFolderPath, PackageManifestConstants.EmptyFileName));
                documentationFolderNullFile.Close();
            }
            
            if (packageManifest.HasSamplesFolder)
            {
                var samplesFolderPath = Path.Combine(rootFolderPath, PackageManifestConstants.SamplesFolderName);
                Directory.CreateDirectory(samplesFolderPath);
                var samplesFolderNullFile = 
                    File.Create(Path.Combine(samplesFolderPath, PackageManifestConstants.EmptyFileName));
                samplesFolderNullFile.Close();
            }
            
            if (packageManifest.HasScreenshotsFolder)
            {
                Directory.CreateDirectory(Path.Combine(rootFolderPath, PackageManifestConstants.ScreenshotsFolderName));
            }

            // Success prompt
            EditorUtility.DisplayDialog(SuccessCreatePackageTitle, SuccessCreatePackageMessage, SuccessCreatePackageOk);
        }
    }
}