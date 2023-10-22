public class AssetUpdater : EditorWindow
{
    [MenuItem("Custom/UpdateAssetsInFolder")]
    public static void UpdateAssetsInFolder()
    {
        string folderPath = "Assets/YourFolderPathHere";
        string[] assetGuids = AssetDatabase.FindAssets("t:YourAssetType", new[] { folderPath });

        foreach (string assetGuid in assetGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            YourAssetType asset = AssetDatabase.LoadAssetAtPath<YourAssetType>(assetPath);

            if (asset != null)
            {
                // Perform your updates on 'asset' here.
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
