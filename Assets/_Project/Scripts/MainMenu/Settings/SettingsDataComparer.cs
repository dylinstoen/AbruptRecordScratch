namespace _Project.Scripts.MainMenu {
    public static class SettingsDataComparer {
        public static bool AreEqual(
            SettingsData first,
            SettingsData second) {

            if (ReferenceEquals(first, second))
                return true;

            if (first == null || second == null)
                return false;

            return
                first.InvertLook == second.InvertLook &&
                first.LookSensitivity == second.LookSensitivity &&
                first.VerticalFOV == second.VerticalFOV &&
                first.ResolutionWidth == second.ResolutionWidth &&
                first.ResolutionHeight == second.ResolutionHeight &&
                first.RefreshRate == second.RefreshRate &&
                first.WindowMode == second.WindowMode &&
                first.Volume == second.Volume &&
                first.VSync == second.VSync;
        }
    }
}