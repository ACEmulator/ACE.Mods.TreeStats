namespace ACE.Mods.TreeStats.Controllers
{
    public class WellKnownController
    {
        public AsheronsCallServerDTO Index()
        {
            return new();
        }

        public class AsheronsCallServerDTO
        {
            public string Schema { get; } = "https://treestats.net/schemas/asherons-call-server/v1/schema.json";
            public int Version { get; } = 1;
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; } = DateTime.UtcNow;
            public string Name { get; } = ConfigManager.Config.Server.WorldName;
            public PopulationDTO Population { get; } = new();
            public EmulatorDTO? Emulator
            {
                get
                {
                    if (PatchClass.EnableEmulatorInfoOutput)
                        return new();
                    else
                        return null;
                }
            }
        }

        public class PopulationDTO
        {
            public int Count { get; } = PlayerManager.GetOnlineCount();
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; } = DateTime.UtcNow;
        }

        public class EmulatorDTO
        {
            public string Name { get; } = "ACE";
            [JsonPropertyName("server_version")]
            public string ServerVersion { get; } = ACE.Server.ServerBuildInfo.FullVersion;
            [JsonPropertyName("database_version")]
            public string DatabaseVersion { get; } = DatabaseManager.World.GetVersion().PatchVersion;
            [JsonPropertyName("dat_iterations")]
            public DATIterationsDTO DatIterations { get; } = new();
        }

        public class DATIterationsDTO
        {
            [JsonPropertyName("client_portal.dat")]
            public int PortalIteration { get; } = DatLoader.DatManager.PortalDat?.Iteration ?? 0;
            [JsonPropertyName("client_cell_1.dat")]
            public int CellIteration { get; } = DatLoader.DatManager.CellDat?.Iteration ?? 0;
            [JsonPropertyName("client_local_english.dat")]
            public int LanguageIteration { get; } = DatLoader.DatManager.LanguageDat?.Iteration ?? 0;
            [JsonPropertyName("client_hires.dat")]
            public int HighResIteration { get; } = DatLoader.DatManager.HighResDat?.Iteration ?? 0;
        }
    }
}
