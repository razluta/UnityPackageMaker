namespace UnityPackageMaker.Editor
{
    public static class GuiConstants
    {
        public const string PackageMakerName = "Package Maker";
        public const string PackageMakerMenuItemPath = "Raz's Tools/Package Maker";
        public const string PackageMakerToolsUrl = "https://github.com/razluta/UnityPackageMaker";
        
        public const string HeaderUxmlPath = "BT_Header";
        public const string LoadPackageButtonUxmlPath = "BT_LoadPackage";
        public const string MainPanelUxmlPath = "CS_PackageMakerCore";
        public const string IncludedPackageContentsUxmlPath = "CS_IncludedPackageContents";
        public const string AllPackagesUxmlPath = "CS_AllPackages";
        public const string PackageManifestUxmlPath = "CS_PackageManifest";
        public const string DependencyEntryUxmlPath = "CS_DependenciesEntry";
        public const string KeywordEntryUxmlPath = "CS_KeywordsEntry";
        public const string ReadmeUxmlPath = "CS_Readme";
        public const string ChangelogUxmlPath = "CS_Changelog";
        public const string LicenseUxmlPath = "CS_License";
        public const string ConsoleLogUxmlPath = "CS_ConsoleLog";

        public const string UpdatePackageButtonUxmlPath = "BT_UpdatePackage";
        public const string CreatePackageButtonUxmlPath = "BT_CreatePackage";

        public const string PackMakButtonName = "BT_Logo";
        public const string LeftPanelName = "VE_PmLeftPanel";
        public const string RightPanelName = "VE_PmRightPanel";
        
        public const string ReadmeToggleName = "TG_Readme";
        public const string ReadmeTextFieldName = "TF_ReadmeContents";
        public const string ChangelogToggleName = "TG_Changelog";
        public const string LicenceToggleName = "TG_License";
        public const string EditorFolderToggleName = "TG_Editor";
        public const string RuntimeFolderToggleName = "TG_Runtime";
        public const string TestsFolderToggleName = "TG_Tests";
        public const string TestsEditorFolderToggleName = "TG_TestEditor";
        public const string TestsRuntimeFolderToggleName = "TG_TestRuntime";
        public const string DocumentationFolderToggleName = "TG_Documentation";
        public const string SamplesFolderToggleName = "TG_Samples";
        public const string ScreenshotsFolderToggleName = "TG_Screenshots";

        public const string DisplayNameTextFieldName = "TF_PackageDisplayName";
        public const string UseDisplayNameAsRootFolderNameToggleName = "TG_UseDisplayNameAsRootFolderName";
        public const string RootFolderNameContentsVisualElementName =  "VE_RootFolderNameContents";
        public const string RootFolderNameTextFieldName = "TF_RootFolderNameContents";
        
        public const string AllPackagesVisualElementName = "VE_AllPackages";
        public const string LoadPackageButtonName = "BT_LoadPackage";

        public const string AuthorNameToggleName = "TG_AuthorName";
        public const string AuthorNameTextFieldName = "TF_AuthorName";

        public const string AuthorEmailToggleName = "TG_AuthorEmail";
        public const string AuthorEmailTextFieldName = "TF_AuthorEmail";

        public const string AuthorUrlToggleName = "TG_AuthorUrl";
        public const string AuthorUrlTextFieldName = "TF_AuthorUrl";

        public const string UnityReleaseToggleName = "TG_UnityRelease";
        public const string UnityReleaseTextFieldName = "TF_UnityRelease";

        public const string DependenciesToggleName = "TG_Dependencies";
        public const string DependenciesContentsVisualElementName = "VE_DependenciesContents";
        public const string DependenciesListViewName = "LV_Dependencies";
        public const string AddDependencyButtonName = "BT_DependenciesAdd";
        public const string DependencyEntryNameTextFieldName = "TF_DependenciesEntryName";
        public const string DependencyEntryVersionTextFieldName = "TF_DependenciesEntryVersion";
        public const string DependencyEntryRemoveButtonName = "BT_DependenciesEntryRemove";

        public const string KeywordsToggleName = "TG_Keywords";
        public const string KeywordsContentsVisualElementName = "VE_KeywordsContents";
        public const string KeywordsListViewName = "LV_Keywords";
        public const string AddKeywordButtonName = "BT_KeywordsAdd";
        public const string KeywordEntryNameTextFieldName = "TF_KeywordsEntryName";
        public const string RemoveKeywordButtonName = "BT_KeywordsEntryRemove";

        public const string UpdatePackageButtonName = "BT_UpdatePackage";
        public const string CreatePackageButtonName = "BT_CreatePackage";

        public const string CreatePackagesWindowTitle = "Browse to the parent folder where you would like to create " +
                                                        "the package folder.";
    }
}