using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityPackageMaker.Editor
{
    public class PackageManifest
    {
        // Public Properties
        public bool HasReadme { get; set; }
        public bool HasChangelog  { get; set; }
        public bool HasLicense { get; set; }
        public bool HasEditorFolder { get; set; }
        public bool HasRuntimeFolder { get; set; }
        public bool HasTestsFolder { get; set; }
        public bool HasDocumentationFolder { get; set; }
        public bool HasSamplesFolder { get; set; }
        public bool HasScreenshotsFolder { get; set; }
        
        public string NameExtension { get; set; }
        public string NameCompany { get; set; }
        public string Name { get; set; }

        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionPatch { get; set; }

        public string DisplayName { get; set; }
        public int UnityVersionMajor { get; set; }
        public int UnityVersionMinor { get; set; }
        public string Description { get; set; }

        public bool HasAuthorName { get; set; }
        public string AuthorName { get; set; }
        public bool HasAuthorEmail { get; set; }
        public string AuthorEmail { get; set; }
        public bool HasAuthorUrl { get; set; }
        public string AuthorUrl { get; set; }

        public bool HasUnityRelease { get; set; }
        public string UnityRelease { get; set; }
        public bool HasDependencies { get; set; }
        public List<string> Dependencies { get; set; }
        public bool HasKeywords { get; set; }
        public List<string> Keywords { get; set; }
        
        public string Readme { get; set; }
        public string Changelog  { get; set; }
        public string License { get; set; }
        
        // Private Defaults
        private const bool HasReadmeDefault = false;
        private const bool HasChangelogDefault = false;
        private const bool HasLicenseDefault = false;
        private const bool HasEditorFolderDefault = false;
        private const bool HasRuntimeFolderDefault = false;
        private const bool HasTestsFolderDefault = false;
        private const bool HasDocumentationFolderDefault = false;
        private const bool HasSamplesFolderDefault = false;
        private const bool HasScreenshotsDefault = false;

        private const string NameExtensionDefault = "";
        private const string NameCompanyDefault = "";
        private const string NameDefault = "";
        private const int VersionMajorDefault = 0;
        private const int VersionMinorDefault = 0;
        private const int VersionPathDefault = 0;

        private const string DisplayNameDefault = "";
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
        private const bool HasKeywordsDefault = false;

        private const string ReadmeDefault = "";
        private const string ChangelogDefault = "";
        private const string LicenseDefault = "";

        // Validation Defaults
        private const int MinimumUnityVersionMajor = 2017;
        private const int MaxUnityReleaseCharCount = 4;
        
        public PackageManifest()
        {
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            HasReadme = HasReadmeDefault;
            HasChangelog = HasChangelogDefault;
            HasLicense = HasLicenseDefault;
            HasEditorFolder = HasEditorFolderDefault;
            HasRuntimeFolder = HasRuntimeFolderDefault;
            HasTestsFolder = HasTestsFolderDefault;
            HasDocumentationFolder = HasDocumentationFolderDefault;
            HasSamplesFolder = HasSamplesFolderDefault;
            HasScreenshotsFolder = HasScreenshotsDefault;
            
            NameExtension = NameExtensionDefault;
            NameCompany = NameCompanyDefault;
            Name = NameDefault;

            VersionMajor = VersionMajorDefault;
            VersionMinor = VersionMinorDefault;
            VersionPatch = VersionPathDefault;

            DisplayName = DisplayNameDefault;
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
            Dependencies = new List<string>(); 
            HasKeywords = HasKeywordsDefault;
            Keywords = new List<string>();

            HasReadme = HasReadmeDefault;
            Readme = ReadmeDefault;
            HasChangelog = HasChangelogDefault;
            Changelog = ChangelogDefault;
            HasLicense = HasLicenseDefault;
            License = LicenseDefault;
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
            if (String.IsNullOrWhiteSpace(Name))
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
                if (unityReleaseCharCount > MaxUnityReleaseCharCount)
                {
                    return false;
                }
                
                var isFirstCharNumeric = UnityRelease[0].ToString().All(char.IsDigit);
                var isSecondCharNumeric = UnityRelease[1].ToString().All(char.IsDigit);
                var isThirdCharNumeric = UnityRelease[2].ToString().All(char.IsDigit);

                // Four count validation
                if (unityReleaseCharCount == MaxUnityReleaseCharCount)
                {
                    var isFourthCharNumeric = UnityRelease[3].ToString().All(char.IsDigit);

                    if (!isFirstCharNumeric || !isSecondCharNumeric || isThirdCharNumeric || !isFourthCharNumeric)
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

            return true;
        }
    }
}