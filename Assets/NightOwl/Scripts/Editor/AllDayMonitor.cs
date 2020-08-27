using System;
using UnityEditor;

namespace NightOwl.Scripts.Editor
{
    [InitializeOnLoad]
    class AllDayMonitor
    {
        static AllDayMonitor()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            if (!NightOwlPreference.Enable)
                return; //不启用直接返回
            float nowTime = DateTime.Now.Hour + DateTime.Now.Minute / 60f;
            float amTime = NightOwlPreference.AmHourTime + NightOwlPreference.AmMinuteTime / 60f;
            float pmTime = NightOwlPreference.PmHourTime + 12 + NightOwlPreference.PmMinuteTime / 60f;
            var isDay = nowTime >= amTime &&
                        nowTime < pmTime &&
                        EditorSkinController.CurrentSkinType != NightOwlPreference.AmSkin;
            var isNight =
                (nowTime >= pmTime ||
                 nowTime < amTime) &&
                EditorSkinController.CurrentSkinType != NightOwlPreference.PmSkin;
            if (isDay || isNight)
                EditorSkinController.SwitchEditorSkin();
        }
    }
}