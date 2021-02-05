using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Sh1ch.Plugins
{
    static internal class MenuItemSettings
    {
        private const int SCRIPT_PRIOPRITY = 30;
        private const int FOLDER_PRIOPRITY = 31;

        private const string PLUGIN_PATH = "Assets/Plugins/ScriptFilesFromTemplate/";
        private const string NEW_FILENAME = "NewFile.cs";

        private const string MENU_ITEM_ROOT_SCRIPT = "Assets/Create/Script Files/";
        private const string MENU_ITEM_ROOT_FOLDER = "Assets/Create/Special Folders/";

        private const string SCRIPT_SIMPLE = "Simple Script";
        private const string SCRIPT_MONO = "MonoBehaviour";
        private const string SCRIPT_MONO_WITH_UNIRX = "MonoBehaviour (UniRx)";

        private const string FOLDER_EDITOR = "Editor";
        private const string FOLDER_EDITOR_DEFAULT_RESOURCES = "Editor Default Resources";
        private const string FOLDER_GIZMOS = "Gizmos";
        private const string FOLDER_PLUGINS = "Plugins";
        private const string FOLDER_RESOURCES = "Resources";
        private const string FOLDER_STANDARD_ASSETS = "Standard Assets";
        private const string FOLDER_STREAMING_ASSETS = "StreamingAssets";

        #region Script Files

        [MenuItem(MENU_ITEM_ROOT_SCRIPT + SCRIPT_SIMPLE, priority = SCRIPT_PRIOPRITY)]
        private static void CreateScript_SimpleScript() => CreateScriptFile("SimpleScript.txt", NEW_FILENAME);

        [MenuItem(MENU_ITEM_ROOT_SCRIPT + SCRIPT_MONO, priority = SCRIPT_PRIOPRITY)]
        private static void CreateScript_MonoBehaviour() => CreateScriptFile("TemplateMonoBehaviour.txt", NEW_FILENAME);

        [MenuItem(MENU_ITEM_ROOT_SCRIPT + SCRIPT_MONO_WITH_UNIRX, priority = SCRIPT_PRIOPRITY)]
        private static void CreateScript_MonoBehaviourWithUniRx() => CreateScriptFile("TemplateMonoBehaviourWithUniRx.txt", NEW_FILENAME);

        #endregion

        #region Special Folders

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_EDITOR, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_Editor() => CreateFolder(FOLDER_EDITOR);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_EDITOR_DEFAULT_RESOURCES, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_EditorDefaultResources() => CreateFolder(FOLDER_EDITOR_DEFAULT_RESOURCES);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_GIZMOS, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_Gizmos() => CreateFolder(FOLDER_GIZMOS);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_PLUGINS, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_Plugins() => CreateFolder(FOLDER_PLUGINS);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_RESOURCES, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_Resources() => CreateFolder(FOLDER_RESOURCES);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_STANDARD_ASSETS, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_StandardAssets() => CreateFolder(FOLDER_STANDARD_ASSETS);

        [MenuItem(MENU_ITEM_ROOT_FOLDER + FOLDER_STREAMING_ASSETS, priority = FOLDER_PRIOPRITY)]
        private static void CreateFolder_StreamingAssets() => CreateFolder(FOLDER_STREAMING_ASSETS);

        #endregion

        /// <summary>
        /// 指定したテンプレートファイルからスクリプトファイルを Assets に追加します。
        /// </summary>
        /// <param name="templateFileName">テンプレートファイルの名前。</param>
        /// <param name="newFileName">初期ファイルの名前。</param>
        private static void CreateScriptFile(string templateFileName, string newFileName)
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(System.IO.Path.Combine(PLUGIN_PATH, $"Templates/{templateFileName}"), newFileName);
        }

        /// <summary>
        /// 指定した名前のフォルダーを Assets に追加します。
        /// </summary>
        /// <param name="folderName">フォルダーの名前。</param>
        private static void CreateFolder(string folderName)
        {
            var selectedFolder = AssetDatabase.GetAssetPath(
                Selection
                .GetFiltered<DefaultAsset>(SelectionMode.Assets)
                .FirstOrDefault());

            var newFolder = System.IO.Path.Combine(selectedFolder, folderName);

            if (System.IO.Directory.Exists(System.IO.Path.Combine(selectedFolder, folderName)))
            {
                Debug.Log($"Selected folder is already exists. path = {newFolder}");
                return;
            }

            var createdFolderGuid = AssetDatabase.CreateFolder(selectedFolder, folderName);
            var createdFolderPath = AssetDatabase.GUIDToAssetPath(createdFolderGuid);
            var createdFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(createdFolderPath);

            EditorGUIUtility.PingObject(createdFolder);
        }
    }
}