using NightOwl.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace NightOwl.Scripts.Editor
{
    public class EditorSkinController : UnityEditor.Editor
    {
        public static EditorSkinType CurrentSkinType
        {
            get
            {
                return EditorGUIUtility.isProSkin
                    ? EditorSkinType.Dark
                    : EditorSkinType.Light;
            }
        }

        [MenuItem("Edit/Switch Editor Skin %t")]
        public static void SwitchEditorSkin()
        {
            System.Reflection.Assembly.GetAssembly(typeof(UnityEditorInternal.AssetStore))
                .GetType("UnityEditorInternal.InternalEditorUtility", true).GetMethod("SwitchSkinAndRepaintAllViews")
                ?.Invoke(null, null);
            EditorPrefs.SetInt(Constant.Constant.EditorPrefsKey.EditorSkin,
                EditorPrefs.GetInt(Constant.Constant.EditorPrefsKey.EditorSkin) == 0 ? 1 : 0);
            var switchSkinSound = AssetDatabase.LoadAssetAtPath<AudioClip>(CurrentSkinType == EditorSkinType.Dark
                ? Constant.Constant.AssetPath.NightOwlDarkAudio
                : Constant.Constant.AssetPath.NightOwlLightAudio);
            if (switchSkinSound && !AudioUtility.IsClipPlaying(switchSkinSound))
                AudioUtility.PlayClip(switchSkinSound);
        }
    }
}