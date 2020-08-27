using System;
using NightOwl.Scripts.Editor.Constant;
using UnityEditor;
using UnityEngine;

public class NightOwlPreference : Editor
{
    #region Field

    private static int _amHourValue; //上午小时
    private static int _pmHourValue; //下午小时
    private static int _amMinuteValue; //上午分钟
    private static int _pmMinuteValue; //下午分钟

    #endregion

    #region Property

    public static bool Enable { get; private set; }

    public static int AmHourTime
    {
        get { return _amHourValue; }
        private set { _amHourValue = value > 12 ? 12 : value < 0 ? 0 : value; }
    }

    public static int PmHourTime
    {
        get { return _pmHourValue; }
        private set { _pmHourValue = value > 12 ? 12 : value < 0 ? 0 : value; }
    }

    public static int AmMinuteTime
    {
        get { return _amMinuteValue; }
        private set { _amMinuteValue = value > 59 ? 59 : value < 0 ? 0 : value; }
    }

    public static int PmMinuteTime
    {
        get { return _pmMinuteValue; }
        private set { _pmMinuteValue = value > 59 ? 59 : value < 0 ? 0 : value; }
    }

    public static EditorSkinType AmSkin { get; set; }
    public static EditorSkinType PmSkin { get; set; }

    #endregion

    [InitializeOnLoadMethod]
    public static void Init()
    {
        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.AmHourTime))
        {
            AmHourTime = 6;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmHourTime, AmHourTime);
        } //设置Am时钟

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.PmHourTime))
        {
            PmHourTime = 6;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmHourTime, PmHourTime);
        } //设置Pm时钟

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.AmMinuteTime))
        {
            AmMinuteTime = 0;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmMinuteTime, AmMinuteTime);
        } //设置Am分钟

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.PmMinuteTime))
        {
            PmMinuteTime = 0;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmMinuteTime, PmMinuteTime);
        } //设置Pm分钟

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.AmSkin))
        {
            AmSkin = EditorSkinType.Light;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmSkin, (int) AmSkin);
        } //设置Am皮肤

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.PmSkin))
        {
            PmSkin = EditorSkinType.Dark;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmSkin, (int) PmSkin);
        } //设置Am皮肤

        if (!EditorPrefs.HasKey(Constant.EditorPrefsKey.EnablePlugin))
        {
            Enable = true;
            EditorPrefs.SetBool(Constant.EditorPrefsKey.EnablePlugin, Enable);
        }

        Enable = EditorPrefs.GetBool(Constant.EditorPrefsKey.EnablePlugin);
        AmHourTime = EditorPrefs.GetInt(Constant.EditorPrefsKey.AmHourTime);
        PmHourTime = EditorPrefs.GetInt(Constant.EditorPrefsKey.PmHourTime);
        AmMinuteTime = EditorPrefs.GetInt(Constant.EditorPrefsKey.AmMinuteTime);
        PmMinuteTime = EditorPrefs.GetInt(Constant.EditorPrefsKey.PmMinuteTime);
        AmSkin = (EditorSkinType) EditorPrefs.GetInt(Constant.EditorPrefsKey.AmSkin);
        PmSkin = (EditorSkinType) EditorPrefs.GetInt(Constant.EditorPrefsKey.PmSkin);
    }

    [PreferenceItem("NightOwl")]
    static void PreferencesGUI()
    {
        EditorGUILayout.LabelField("NightOwl Settings", EditorStyles.boldLabel);
        var iconRect = new Vector2(0, 0);
        EditorGUILayout.BeginScrollView(iconRect, GUILayout.Width(100), GUILayout.Height(100));
        var iconTexture = AssetDatabase.LoadAssetAtPath<Texture>(EditorGUIUtility.isProSkin
            ? Constant.AssetPath.NightOwlDarkIcon
            : Constant.AssetPath.NightOwlLightIcon);
        if (iconTexture)
            GUI.DrawTexture(new Rect(0, 0, 100, 100), iconTexture);
        EditorGUILayout.EndScrollView();
        var tmpEnable = EditorGUILayout.Toggle("Enable", Enable);
        if (Enable != tmpEnable)
        {
            Enable = tmpEnable;
            EditorPrefs.SetBool(Constant.EditorPrefsKey.EnablePlugin, Enable);
        }

        if (!Enable)
            return;

        EditorGUILayout.BeginVertical(Constant.GUIStyle.Box);
        EditorGUILayout.LabelField("AmSetting", EditorStyles.boldLabel);
        var tmpAmHour = EditorGUILayout.IntSlider("Hour:", AmHourTime, 0, 12);
        if (AmHourTime != tmpAmHour)
        {
            AmHourTime = tmpAmHour;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmHourTime, AmHourTime);
        }

        var tmpAmMinute = EditorGUILayout.IntSlider("Minute:", AmMinuteTime, 0, 59);
        if (AmMinuteTime != tmpAmMinute)
        {
            AmMinuteTime = tmpAmMinute;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmMinuteTime, AmMinuteTime);
        }

        var tmpAmSkin = EditorGUILayout.EnumPopup("Skin", AmSkin);
        if (!Equals(tmpAmSkin, AmSkin))
        {
            AmSkin = (EditorSkinType) tmpAmSkin;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.AmSkin, (int) AmSkin);
        }

        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical(Constant.GUIStyle.Box);
        EditorGUILayout.LabelField("PmSetting", EditorStyles.boldLabel);
        var tmpPmHour = EditorGUILayout.IntSlider("Hour:", PmHourTime, 0, 12);
        if (PmHourTime != tmpPmHour)
        {
            PmHourTime = tmpPmHour;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmHourTime, PmHourTime);
        }

        var tmpPmMinute = EditorGUILayout.IntSlider("Minute:", PmMinuteTime, 0, 59);
        if (PmMinuteTime != tmpPmMinute)
        {
            PmMinuteTime = tmpPmMinute;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmMinuteTime, PmMinuteTime);
        }

        var tmpPmSkin = EditorGUILayout.EnumPopup("Skin", PmSkin);
        if (!Equals(tmpPmSkin, PmSkin))
        {
            PmSkin = (EditorSkinType) tmpPmSkin;
            EditorPrefs.SetInt(Constant.EditorPrefsKey.PmSkin, (int) PmSkin);
        }

        EditorGUILayout.EndVertical();

        EditorGUI.EndChangeCheck();
        // EditorValues.IsProjectOwner = EditorGUILayout.Toggle("Is Owner", EditorValues.IsProjectOwner);
    }
}