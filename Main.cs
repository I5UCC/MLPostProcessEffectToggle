using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace WorldEffectToggle
{
    public class Main : MelonMod
    {
        internal static readonly MelonLogger.Instance mlog = new MelonLogger.Instance("WorldEffectToggle", ConsoleColor.DarkGreen);

        public static MelonPreferences_Category Category;

        public static MelonPreferences_Entry<bool> PostprocessingState;

        public static MelonPreferences_Entry<bool> AOState;
        public static MelonPreferences_Entry<bool> AutoExposureState;
        public static MelonPreferences_Entry<bool> BloomState;
        public static MelonPreferences_Entry<bool> ChromaticAberrationState;
        public static MelonPreferences_Entry<bool> ColorGradingState;
        public static MelonPreferences_Entry<bool> DOFState;
        public static MelonPreferences_Entry<bool> GrainState;
        public static MelonPreferences_Entry<bool> LensDistortionState;
        public static MelonPreferences_Entry<bool> MotionBlurState;
        public static MelonPreferences_Entry<bool> ScreenSpaceReflectionsState;
        public static MelonPreferences_Entry<bool> VignetteState;

        public static bool SettingChanged = false;
        public static bool IsComponentToggleInstalled = false;

        public override void OnApplicationStart()
        {
            Category = MelonPreferences.CreateCategory("WorldEffectToggle");

            foreach (var m in MelonHandler.Mods)
            {
                if (m.Info.Name == "ComponentToggle")
                {
                    mlog.Msg(ConsoleColor.Yellow, "Found ComponentToggle Mod. Disabling Post Processing Toggle.");
                    IsComponentToggleInstalled = true;
                    break;
                }
            }

            CreateMelonPreferences();
        }

        private void CreateMelonPreferences()
        {
            if (!IsComponentToggleInstalled)
            {
                PostprocessingState = Category.CreateEntry("Disable PostProcessing", false);
                PostprocessingState.OnValueChanged += TogglePP;
            }

            AOState = Category.CreateEntry("Disable Ambient Occlusion", false);
            AOState.OnValueChanged += AOState_OnValueChanged;

            AutoExposureState = Category.CreateEntry("Disable Auto-Exposure", false);
            AutoExposureState.OnValueChanged += AutoExposureState_OnValueChanged;

            BloomState = Category.CreateEntry("Disable Bloom", false);
            BloomState.OnValueChanged += BloomState_OnValueChanged;

            ChromaticAberrationState = Category.CreateEntry("Disable Chromatic Aberration", false);
            ChromaticAberrationState.OnValueChanged += ChromaticAberrationState_OnValueChanged;

            ColorGradingState = Category.CreateEntry("Disable Color Grading", false);
            ColorGradingState.OnValueChanged += ColorGradingState_OnValueChanged;

            DOFState = Category.CreateEntry("Disable Depth Of Field", false);
            DOFState.OnValueChanged += DOFState_OnValueChanged;

            GrainState = Category.CreateEntry("Disable Grain", false);
            GrainState.OnValueChanged += GrainState_OnValueChanged;

            LensDistortionState = Category.CreateEntry("Disable Lens Distortion", false);
            LensDistortionState.OnValueChanged += LensDistortionState_OnValueChanged;

            MotionBlurState = Category.CreateEntry("Disable Motion Blur", false);
            MotionBlurState.OnValueChanged += MotionBlurState_OnValueChanged;

            ScreenSpaceReflectionsState = Category.CreateEntry("Disable Screen Space Reflections", false);
            ScreenSpaceReflectionsState.OnValueChanged += ScreenSpaceReflectionsState_OnValueChanged;

            VignetteState = Category.CreateEntry("Disable Vignette", false);
            VignetteState.OnValueChanged += VignetteState_OnValueChanged;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == -1)
            {
                if (!IsComponentToggleInstalled && PostprocessingState.Value)
                    TogglePP();

                AOState_OnValueChanged();
                AutoExposureState_OnValueChanged();
                BloomState_OnValueChanged();
                ChromaticAberrationState_OnValueChanged();
                ColorGradingState_OnValueChanged();
                DOFState_OnValueChanged();
                GrainState_OnValueChanged();
                LensDistortionState_OnValueChanged();
                MotionBlurState_OnValueChanged();
                ScreenSpaceReflectionsState_OnValueChanged();
                VignetteState_OnValueChanged();
            }
        }

        public override void OnPreferencesSaved()
        {
            if (!SettingChanged) return;

            string msg = "Re-enabled some Postprocessing effects. Rejoin or change world for changes to take effect.";
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(msg);
            mlog.Msg(ConsoleColor.Yellow, msg);

            SettingChanged = false;
        }

        private void VignetteState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (VignetteState.Value)
            {
                mlog.Msg("Vignette OFF");
                foreach (Vignette x in Resources.FindObjectsOfTypeAll<Vignette>())
                    x.enabled.Override(false);
            }
        }

        private void ScreenSpaceReflectionsState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (ScreenSpaceReflectionsState.Value)
            {
                mlog.Msg("Screen Space Reflections OFF");
                foreach (ScreenSpaceReflections x in Resources.FindObjectsOfTypeAll<ScreenSpaceReflections>())
                    x.enabled.Override(false);
            }
        }

        private void MotionBlurState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (MotionBlurState.Value)
            {
                mlog.Msg("Lens Distortion OFF");
                foreach (LensDistortion x in Resources.FindObjectsOfTypeAll<LensDistortion>())
                    x.enabled.Override(false);
            }
        }

        private void LensDistortionState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (LensDistortionState.Value)
            {
                mlog.Msg("Lens Distortion OFF");
                foreach (LensDistortion x in Resources.FindObjectsOfTypeAll<LensDistortion>())
                    x.enabled.Override(false);
            }
        }

        private void GrainState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (GrainState.Value)
            {
                mlog.Msg("Grain OFF");
                foreach (Grain x in Resources.FindObjectsOfTypeAll<Grain>())
                    x.enabled.Override(false);
            }
        }

        private void DOFState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (DOFState.Value)
            {
                mlog.Msg("Depth Of Field OFF");
                foreach (DepthOfField x in Resources.FindObjectsOfTypeAll<DepthOfField>())
                    x.enabled.Override(false);
            }
        }

        private void ColorGradingState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (ColorGradingState.Value)
            {
                mlog.Msg("Color Grading OFF");
                foreach (ColorGrading x in Resources.FindObjectsOfTypeAll<ColorGrading>())
                    x.enabled.Override(false);
            }
        }

        private void ChromaticAberrationState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (ChromaticAberrationState.Value)
            {
                mlog.Msg("Chromatic Aberration OFF");
                foreach (ChromaticAberration x in Resources.FindObjectsOfTypeAll<ChromaticAberration>())
                    x.enabled.Override(false);
            }
        }

        private void BloomState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (BloomState.Value)
            {
                mlog.Msg("Bloom OFF");
                foreach (Bloom x in Resources.FindObjectsOfTypeAll<Bloom>())
                    x.enabled.Override(false);
            }
        }

        private void AutoExposureState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (AutoExposureState.Value)
            {
                mlog.Msg("AutoExposure OFF");
                foreach (AutoExposure x in Resources.FindObjectsOfTypeAll<AutoExposure>())
                    x.enabled.Override(false);
            }
        }

        private void AOState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            SettingChanged = arg1;
            if (AOState.Value)
            {
                mlog.Msg("AO set to OFF");
                foreach (AmbientOcclusion x in Resources.FindObjectsOfTypeAll<AmbientOcclusion>())
                    x.enabled.Override(false);
            }
        }

        private void TogglePP(bool arg1 = false, bool arg2 = false)
        {
            foreach (Camera cam in Camera.allCameras)
            {
                if (cam.GetComponent<PostProcessLayer>() != null)
                {
                    cam.GetComponent<PostProcessLayer>().enabled = !PostprocessingState.Value;
                    mlog.Msg(String.Format("PostProcessing set to {0}", PostprocessingState.Value ? "OFF" : "ON"));
                }
            }
        }
    }
}
