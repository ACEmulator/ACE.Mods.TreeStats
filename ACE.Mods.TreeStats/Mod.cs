namespace ACE.Mods.TreeStats
{
    public class Mod : BasicMod
    {
        public Mod() : base() => Setup("ACE.Mods.TreeStats", new PatchClass(this));

        internal static void Log(string message, ModManager.LogLevel level = ModManager.LogLevel.Info)
        {
            ModManager.Log($"[ACE.Mods.TreeStats] {message}", level);
        }
    }
}
