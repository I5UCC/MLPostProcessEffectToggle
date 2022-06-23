using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace WorldPostProcessToggle
{
    public class Main : MelonMod
    {
        internal static readonly MelonLogger.Instance mlog = new MelonLogger.Instance("WorldPostProcessToggle", ConsoleColor.DarkGreen);

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

        public override void OnApplicationStart()
        {
            Category = MelonPreferences.CreateCategory("WorldPostProcessToggle");

            PostprocessingState = Category.CreateEntry("Disable PostProcessing", false);
            PostprocessingState.OnValueChanged += PostprocessingState_OnValueChanged;

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

        private void PostprocessingState_OnValueChanged(bool arg1 = false, bool arg2 = false) => TogglePP();

        private void Changed(bool arg1 = false, bool arg2 = false)
        {
            if (arg1) SettingChanged = true;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == -1)
            {
                PostprocessingState_OnValueChanged();
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

            string msg = "Re-enabled some Postprocessing effects. Rejoin the world for changes to take effect.";
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(msg);
            mlog.Warning(msg);

            SettingChanged = false;
        }

        private void VignetteState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (VignetteState.Value)
            {
                mlog.Msg("Vignette OFF");
                foreach (Vignette x in Resources.FindObjectsOfTypeAll<Vignette>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void ScreenSpaceReflectionsState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (ScreenSpaceReflectionsState.Value)
            {
                mlog.Msg("Screen Space Reflections OFF");
                foreach (ScreenSpaceReflections x in Resources.FindObjectsOfTypeAll<ScreenSpaceReflections>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void MotionBlurState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (MotionBlurState.Value)
            {
                mlog.Msg("Lens Distortion OFF");
                foreach (LensDistortion x in Resources.FindObjectsOfTypeAll<LensDistortion>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void LensDistortionState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (LensDistortionState.Value)
            {
                mlog.Msg("Lens Distortion OFF");
                foreach (LensDistortion x in Resources.FindObjectsOfTypeAll<LensDistortion>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void GrainState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (GrainState.Value)
            {
                mlog.Msg("Grain OFF");
                foreach (Grain x in Resources.FindObjectsOfTypeAll<Grain>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void DOFState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (DOFState.Value)
            {
                mlog.Msg("Depth Of Field OFF");
                foreach (DepthOfField x in Resources.FindObjectsOfTypeAll<DepthOfField>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void ColorGradingState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (ColorGradingState.Value)
            {
                mlog.Msg("Color Grading OFF");
                foreach (ColorGrading x in Resources.FindObjectsOfTypeAll<ColorGrading>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void ChromaticAberrationState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (ChromaticAberrationState.Value)
            {
                mlog.Msg("Chromatic Aberration OFF");
                foreach (ChromaticAberration x in Resources.FindObjectsOfTypeAll<ChromaticAberration>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void BloomState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (BloomState.Value)
            {
                mlog.Msg("Bloom OFF");
                foreach (Bloom x in Resources.FindObjectsOfTypeAll<Bloom>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void AutoExposureState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (AutoExposureState.Value)
            {
                mlog.Msg("AutoExposure OFF");
                foreach (AutoExposure x in Resources.FindObjectsOfTypeAll<AutoExposure>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void AOState_OnValueChanged(bool arg1 = false, bool arg2 = false)
        {
            if (AOState.Value)
            {
                mlog.Msg("AO set to OFF");
                foreach (AmbientOcclusion x in Resources.FindObjectsOfTypeAll<AmbientOcclusion>())
                {
                    x.enabled.Override(arg1);
                }
            }
            Changed(arg1, arg2);
        }

        private void TogglePP()
        {
            foreach (Camera cam in Camera.allCameras)
            {
                if (cam.GetComponent<PostProcessLayer>() != null)
                    cam.GetComponent<PostProcessLayer>().enabled = !PostprocessingState.Value;
            }
        }
    }
}
