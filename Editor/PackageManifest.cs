using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityPackageMaker.Editor
{
    public class PackageManifest : ScriptableObject
    {
        #region PRIVATE FIELDS
        [SerializeField] private string packageAbsolutePath;
        [SerializeField] private string displayName;
        [SerializeField] private bool isUseDisplayNameAsRootFolderName;
        [SerializeField] private string rootFolderName;
        [SerializeField] private bool hasReadme;
        [SerializeField] private bool hasChangelog;
        [SerializeField] private bool hasLicense;
        [SerializeField] private bool hasThirdPartyNotices;
        [SerializeField] private bool hasEditorFolder;
        [SerializeField] private bool hasRuntimeFolder;
        [SerializeField] private bool hasTestsFolder;
        [SerializeField] private bool hasTestsEditorFolder;
        [SerializeField] private bool hasTestsRuntimeFolder;
        [SerializeField] private bool hasDocumentationFolder;
        [SerializeField] private bool hasSamplesFolder;
        [SerializeField] private bool hasScreenshotsFolder;
        [SerializeField] private string nameExtension;
        [SerializeField] private string nameCompany;
        [SerializeField] private string namePackage;
        [SerializeField] private int versionMajor;
        [SerializeField] private int versionMinor;
        [SerializeField] private int versionPatch;
        [SerializeField] private int unityVersionMajor;
        [SerializeField] private int unityVersionMinor;
        [SerializeField] private string description;
        [SerializeField] private bool hasAuthorName;
        [SerializeField] private string authorName;
        [SerializeField] private bool hasAuthorEmail;
        [SerializeField] private string authorEmail;
        [SerializeField] private bool hasAuthorUrl;
        [SerializeField] private string authorUrl;
        [SerializeField] private bool hasUnityRelease;
        [SerializeField] private string unityRelease;
        [SerializeField] private bool hasDependencies;
        [SerializeField] private List<PackageDependency> dependencies;
        [SerializeField] private bool hasKeywords;
        [SerializeField] private List<string> keywords;
        [SerializeField] private string readme;
        [SerializeField] private string changelog;
        [SerializeField] private string license;
        [SerializeField] private string thirdPartyNotices;
        #endregion
        
        #region PUBLIC PROPERTIES
        public string PackageAbsolutePath
        {
            get => packageAbsolutePath;
            set => packageAbsolutePath = value;
        }

        public string DisplayName 
        {
            get => displayName;
            set => displayName = value;
        }
        
        public bool IsUseDisplayNameAsRootFolderName
        {
            get => isUseDisplayNameAsRootFolderName;
            set => isUseDisplayNameAsRootFolderName = value;
        }
        
        public string RootFolderName  
        {
            get => rootFolderName;
            set => rootFolderName = value;
        }

        public bool HasReadme 
        {
            get => hasReadme;
            set => hasReadme = value;
        }
        
        public bool HasChangelog
        {
            get => hasChangelog;
            set => hasChangelog = value;
        }
        
        public bool HasLicense
        {
            get => hasLicense;
            set => hasLicense = value;
        }

        public bool HasThirdPartyNotices
        {
            get => hasThirdPartyNotices;
            set => hasThirdPartyNotices = value;
        }

        public bool HasEditorFolder
        {
            get => hasEditorFolder;
            set => hasEditorFolder = value;
        }
        
        public bool HasRuntimeFolder
        {
            get => hasRuntimeFolder;
            set => hasRuntimeFolder = value;
        }
        
        public bool HasTestsFolder
        {
            get => hasTestsFolder;
            set => hasTestsFolder = value;
        }
        
        public bool HasTestsEditorFolder
        {
            get => hasTestsEditorFolder;
            set => hasTestsEditorFolder = value;
        }
        
        public bool HasTestsRuntimeFolder
        {
            get => hasTestsRuntimeFolder;
            set => hasTestsRuntimeFolder = value;
        }
        
        public bool HasDocumentationFolder
        {
            get => hasDocumentationFolder;
            set => hasDocumentationFolder = value;
        }
        
        public bool HasSamplesFolder
        {
            get => hasSamplesFolder;
            set => hasSamplesFolder = value;
        }
        
        public bool HasScreenshotsFolder
        {
            get => hasScreenshotsFolder;
            set => hasScreenshotsFolder = value;
        }
        
        public string NameExtension
        {
            get => nameExtension;
            set => nameExtension = value;
        }
        
        public string NameCompany
        {
            get => nameCompany;
            set => nameCompany = value;
        }
        
        public string NamePackage
        {
            get => namePackage;
            set => namePackage = value;
        }
        
        public int VersionMajor
        {
            get => versionMajor;
            set => versionMajor = value;
        }
        
        public int VersionMinor
        {
            get => versionMinor;
            set => versionMinor = value;
        }
        
        public int VersionPatch
        {
            get => versionPatch;
            set => versionPatch = value;
        }
        
        public int UnityVersionMajor
        {
            get => unityVersionMajor;
            set => unityVersionMajor = value;
        }
        
        public int UnityVersionMinor
        {
            get => unityVersionMinor;
            set => unityVersionMinor = value;
        }
        
        public string Description
        {
            get => description;
            set => description = value;
        }
        
        public bool HasAuthorName
        {
            get => hasAuthorName;
            set => hasAuthorName = value;
        }
        
        public string AuthorName
        {
            get => authorName;
            set => authorName = value;
        }
        
        public bool HasAuthorEmail
        {
            get => hasAuthorEmail;
            set => hasAuthorEmail = value;
        }
        
        public string AuthorEmail
        {
            get => authorEmail;
            set => authorEmail = value;
        }
        
        public bool HasAuthorUrl
        {
            get => hasAuthorUrl;
            set => hasAuthorUrl = value;
        }
        
        public string AuthorUrl
        {
            get => authorUrl;
            set => authorUrl = value;
        }
        
        public bool HasUnityRelease
        {
            get => hasUnityRelease;
            set => hasUnityRelease = value;
        }
        
        public string UnityRelease
        {
            get => unityRelease;
            set => unityRelease = value;
        }
        
        public bool HasDependencies
        {
            get => hasDependencies;
            set => hasDependencies = value;
        }
        
        public List<PackageDependency> Dependencies
        {
            get => dependencies;
            set => dependencies = value;
        }
        
        public bool HasKeywords
        {
            get => hasKeywords;
            set => hasKeywords = value;
        }
        
        public List<string> Keywords
        {
            get => keywords;
            set => keywords = value;
        }
        
        public string Readme
        {
            get => readme;
            set => readme = value;
        }
        
        public string Changelog
        {
            get => changelog;
            set => changelog = value;
        }
        
        public string License
        {
            get => license;
            set => license = value;
        }
        
        public string ThirdPartyNotices
        {
            get => thirdPartyNotices;
            set => thirdPartyNotices = value;
        }
        #endregion

        // Private Defaults
        private const string DisplayNameDefault = "";
        private const bool IsUseDisplayNameAsRootFolderNameDefault = false;
        private const string RootFolderNameDefault = "";
        private const bool HasReadmeDefault = false;
        private const bool HasChangelogDefault = false;
        private const bool HasLicenseDefault = false;
        private const bool HasThirdPartyNoticesDefault = false;
        private const bool HasEditorFolderDefault = false;
        private const bool HasRuntimeFolderDefault = false;
        private const bool HasTestsFolderDefault = false;
        private const bool HasTestsEditorFolderDefault = false;
        private const bool HasTestsRuntimeFolderDefault = false;
        private const bool HasDocumentationFolderDefault = false;
        private const bool HasSamplesFolderDefault = false;
        private const bool HasScreenshotsDefault = false;

        private const string NameExtensionDefault = "";
        private const string NameCompanyDefault = "";
        private const string NameDefault = "";
        private const int VersionMajorDefault = 0;
        private const int VersionMinorDefault = 0;
        private const int VersionPathDefault = 0;
        
        private const int UnityVersionMajorDefault = 0;
        private const int UnityVersionMinorDefault = 0;
        private const string DescriptionDefault = "";

        private const bool HasAuthorNameDefault = false;
        private const string AuthorNameDefault = "";
        private const bool HasAuthorEmailDefault = false;
        private const string AuthorEmailDefault = "";
        private const bool HasAuthorUrlDefault = false;
        private const string AuthorUrlDefault = "";
        private const bool HasUnityReleaseDefault = false;
        private const string UnityReleaseDefault = "";

        private const bool HasDependenciesDefault = false;
        private static readonly List<PackageDependency> DependenciesDefault = new List<PackageDependency>();
        private const bool HasKeywordsDefault = false;
        private static readonly List<string> KeywordsDefault = new List<string>();

        private const string ReadmeDefault = "";
        private const string ChangelogDefault = "";
        private const string LicenseDefault = "";
        private const string ThirdPartyNoticesDefault = "";

        // Validation Defaults
        private const int MinimumUnityVersionMajor = 2017;
        private const int MaxUnityReleaseCharCount = 5;
        
        public PackageManifest()
        {
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            DisplayName = DisplayNameDefault;
            IsUseDisplayNameAsRootFolderName = IsUseDisplayNameAsRootFolderNameDefault;
            RootFolderName = RootFolderNameDefault;
            HasReadme = HasReadmeDefault;
            HasChangelog = HasChangelogDefault;
            HasLicense = HasLicenseDefault;
            HasThirdPartyNotices = HasThirdPartyNoticesDefault;
            HasEditorFolder = HasEditorFolderDefault;
            HasRuntimeFolder = HasRuntimeFolderDefault;
            HasTestsFolder = HasTestsFolderDefault;
            HasTestsEditorFolder = HasTestsEditorFolderDefault;
            HasTestsRuntimeFolder = HasTestsRuntimeFolderDefault;
            HasDocumentationFolder = HasDocumentationFolderDefault;
            HasSamplesFolder = HasSamplesFolderDefault;
            HasScreenshotsFolder = HasScreenshotsDefault;
            
            NameExtension = NameExtensionDefault;
            NameCompany = NameCompanyDefault;
            NamePackage = NameDefault;

            VersionMajor = VersionMajorDefault;
            VersionMinor = VersionMinorDefault;
            VersionPatch = VersionPathDefault;
            
            UnityVersionMajor = UnityVersionMajorDefault;
            UnityVersionMinor = UnityVersionMinorDefault;
            Description = DescriptionDefault;

            HasAuthorName = HasAuthorNameDefault;
            AuthorName = AuthorNameDefault;
            HasAuthorEmail = HasAuthorEmailDefault;
            AuthorEmail = AuthorEmailDefault;
            HasAuthorUrl = HasAuthorUrlDefault;
            AuthorUrl = AuthorUrlDefault;

            HasUnityRelease = HasUnityReleaseDefault;
            UnityRelease = UnityReleaseDefault;
            HasDependencies = HasDependenciesDefault;
            Dependencies = DependenciesDefault;
            HasKeywords = HasKeywordsDefault;
            Keywords = KeywordsDefault;

            HasReadme = HasReadmeDefault;
            Readme = ReadmeDefault;
            HasChangelog = HasChangelogDefault;
            Changelog = ChangelogDefault;
            License = LicenseDefault;
            ThirdPartyNotices = ThirdPartyNoticesDefault;
        }

        public bool IsValidPackageManifest()
        {
            // Name Extension
            if (String.IsNullOrWhiteSpace(NameExtension))
            {
                return false;
            }
            else
            {
                var containsNumbers = NameExtension.Any(char.IsDigit);
                if (containsNumbers)
                {
                    return false;
                }
            }

            // Name Company
            if (String.IsNullOrWhiteSpace(NameCompany))
            {
                return false;
            }
            
            // Name 
            if (String.IsNullOrWhiteSpace(NamePackage))
            {
                return false;
            }
            
            // Version
            // Version is always valid because it resets to 0
            
            // Display Name
            if (String.IsNullOrWhiteSpace(DisplayName))
            {
                return false;
            }
            
            // Unity Version
            if (UnityVersionMajor < MinimumUnityVersionMajor)
            {
                return false;
            }
            
            // Description
            if (String.IsNullOrWhiteSpace(Description))
            {
                return false;
            }
            
            // Author Name
            if (HasAuthorName && String.IsNullOrWhiteSpace(AuthorName))
            {
                return false;
            }
            
            // Author Email
            if (HasAuthorEmail && String.IsNullOrWhiteSpace(AuthorEmail))
            {
                return false;
            }
            
            // Author Url
            if (HasAuthorUrl && String.IsNullOrWhiteSpace(AuthorUrl))
            {
                return false;
            }
            
            // Unity Release
            if (HasUnityRelease && String.IsNullOrWhiteSpace(UnityRelease))
            {
                var unityReleaseCharCount = UnityRelease.Length;
                if (unityReleaseCharCount == 0)
                {
                    return false;
                }
                
                if (unityReleaseCharCount > MaxUnityReleaseCharCount)
                {
                    return false;
                }
                
                var isFirstCharNumeric = UnityRelease[0].ToString().All(char.IsDigit);
                var isSecondCharNumeric = UnityRelease[1].ToString().All(char.IsDigit);
                var isThirdCharNumeric = UnityRelease[2].ToString().All(char.IsDigit);

                // Five count validation
                if (unityReleaseCharCount == MaxUnityReleaseCharCount)
                {
                    var isFourthCharNumeric = UnityRelease[3].ToString().All(char.IsDigit);
                    var isFifthCharNumeric = UnityRelease[4].ToString().All(char.IsDigit);
                    
                    if (!isFirstCharNumeric || !isSecondCharNumeric || 
                        isThirdCharNumeric || 
                        !isFourthCharNumeric || !isFifthCharNumeric)
                    {
                        return false;
                    }
                }
                
                // Four count validation
                if (unityReleaseCharCount == MaxUnityReleaseCharCount - 1)
                {
                    var isFourthCharNumeric = UnityRelease[3].ToString().All(char.IsDigit);
                    var isValidOptionOne = false;
                    var isValidOptionTwo = false;
                    
                    if (isFirstCharNumeric || !isSecondCharNumeric || isThirdCharNumeric || isFourthCharNumeric)
                    {
                        isValidOptionOne = true;
                    }
                    
                    if (isFirstCharNumeric || isSecondCharNumeric || !isThirdCharNumeric || isFourthCharNumeric)
                    {
                        isValidOptionTwo = true;
                    }

                    if (!isValidOptionOne && !isValidOptionTwo)
                    {
                        return false;
                    }
                }
                
                // Three count validation
                if (!isFirstCharNumeric || isSecondCharNumeric || !isThirdCharNumeric)
                {
                    return false;
                }
            }
            
            // Dependencies
            if (HasDependencies && Dependencies.Count == 0)
            {
                return false;
            }
            
            // Keywords
            if (HasKeywords && Keywords.Count == 0)
            {
                return false;
            }
            
            // Readme
            if (HasReadme && String.IsNullOrWhiteSpace(Readme))
            {
                return false;
            }
            
            // Changelog
            if (HasChangelog && String.IsNullOrWhiteSpace(Changelog))
            {
                return false;
            }
            
            // License
            if (HasLicense && String.IsNullOrWhiteSpace(License))
            {
                return false;
            }
            
            // License
            if (HasThirdPartyNotices && String.IsNullOrWhiteSpace(ThirdPartyNotices))
            {
                return false;
            }

            return true;
        }
    }
}