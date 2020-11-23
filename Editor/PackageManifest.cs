using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityPackageMaker.Editor
{
    public class PackageManifest
    {
        // Public Properties
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
        
        // Private Defaults
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

        private const bool HasAuthorNameDefault = true;
        private const string AuthorNameDefault = "";
        private const bool HasAuthorEmailDefault = true;
        private const string AuthorEmailDefault = "";
        private const bool HasAuthorUrlDefault = true;
        private const string AuthorUrlDefault = "";
        private const bool HasUnityReleaseDefault = true;
        private const string UnityReleaseDefault = "";

        public PackageManifest()
        {
            ResetToDefault();
        }

        public void ResetToDefault()
        {
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

            return true;
        }
    }
}