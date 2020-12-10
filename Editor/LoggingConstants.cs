namespace UnityPackageMaker.Editor
{
    public static class LoggingConstants
    {
        public const string NameExtensionErrorEmpty = "Name extension cannot be empty.";
        public const string NameExtensionErrorContainsNumber = "Name extention cannot contain numbers.";

        public const string NameCompanyErrorEmpty = "Company name cannot be empty.";

        public const string NamePackageErrorEmpty = "Package name cannot be empty.";

        public const string DisplayNameErrorEmpty = "Display name cannot be empty.";

        public const string RootFolderNameErrorEmpty = "Root Folder Name cannot be empty.";

        public const string UnityVersionMajorErrorMinimum = "Unity major version cannot be earlier than ";

        public const string UnityVersionMinorErrorMinimum = "Unity minor version cannot be smaller than: ";
        public const string UnityVersionMinorErrorMaximum = "Unity minor version cannot be larger than: ";

        public const string DescriptionErrorEmpty = "Description cannot be empty.";

        public const string AuthorNameErrorEmpty = "Author name cannot be empty if enabled.";
        public const string AuthorEmailErrorEmpty = "Author email cannot be emptyif enabled.";
        public const string AuthorEmailErrorSymbol = "Author email must contain @ symbol.";
        public const string AuthorUrlErrorEmpty = "Author url cannot be empty if enabled.";

        public const string UnityReleaseErrorEmpty = "Unity Release cannot be empty.";
        public const string UnityReleaseErrorTooMany = "Unity Release max character count is: ";
        public const string UnityReleaseErrorTooFew = "Unity Release min character count is: ";
        public const string UnityReleaseErrorAllDigits = "Unity Release must contains letters, not just numbers - see example.";
        public const string UnityReleaseErrorNoDigits = "Unity Release must contain numbers, not just letters - see example.";

        public const string DependenciesErrorEmpty = "Dependencies are enabled, but none are active. Disable them " +
                                                     "or add new fields";

        public const string KeywordsErrorEmpty = "Keywords are enabled, but none are active. Disable them or add " +
                                                 "new fields";

        public const string ReadmeErrorEmpty = "Readme cannot be empty if enabled.";

        public const string ChangelogErrorEmpty = "Changelog cannot be empty if enabled.";

        public const string LicenceErrorEmpty = "Licence cannot be empty if enabled.";

        public const string ThirdPartyNoticesErrorEmpty = "Third Party Notices cannot be empty if enabled";

    }
}