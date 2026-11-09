namespace ACE.Mods.TreeStats
{
    [HarmonyPatch]
    public class PatchClass(BasicMod mod, string settingsName = "Settings.json") : BasicPatch<Settings>(mod, settingsName)
    {
        private static IServerHost? serverHost;

        public override void Init()
        {
            base.Init();
        }

        public override Task OnStartSuccess()
        {
            Settings = SettingsContainer?.Settings ?? new();
            StartServices();

            return Task.CompletedTask;
        }

        //public override Task OnWorldOpen()
        //{
        //    Settings = SettingsContainer?.Settings ?? new();
        //    StartServices();

        //    return Task.CompletedTask;
        //}

        protected override void SettingsChanged(object? sender, EventArgs e)
        {
            StopServices();
            base.SettingsChanged(sender, e);
            Settings = SettingsContainer?.Settings ?? new();
            StartServices();
        }

        public override void Stop()
        {
            StopServices();
            base.Stop();
        }

        public static void StartServices()
        {
            try
            {
                serverHost = Host.Create();

                var api = Layout.Create();

                api.AddController<WellKnownController>("/");

                if (Settings.EnableCharacterService)
                    api.AddService<CharacterService>("characters");

                //if (Settings.EnableOpenAPI)
                //    api.AddOpenApi();

                //if (Settings.EnableSwaggerUI)
                //    api.AddSwaggerUI();

                //if (Settings.EnableRedoc)
                //    api.AddRedoc();

                //if (Settings.EnableScalar)
                //    api.AddScalar();

                var app = Layout.Create().Add(Settings.APIBasePath, api);

                app.Add(CorsPolicy.Permissive());

                serverHost?.Handler(app);
                //.Defaults()
                //.Development()
                //.Console()
                //.StartAsync();

                serverHost?.Defaults();

                //var host = IPAddress.Parse(Settings.Host);
                //var port = Settings.Port;
                var host = IPAddress.Parse(ConfigManager.Config.Server.Network.Host);
                var port = (ushort)ConfigManager.Config.Server.Network.Port;

                serverHost?.Bind(host, port);

                if (Settings.OutputToConsole)
                    serverHost?.Console();

                var server = serverHost?.StartAsync();

                Mod.Log($"TreeStats Server Online and listening to requests at:\n http://{host}:{port}" + $"{Settings.APIBasePath}");
                //if (Settings.EnableSwaggerUI)
                //    Mod.Log($"SwaggerUI API Browser available at:\n http://{host}:{port}" + $"{Settings.APIBasePath}/swagger/");
                //if (Settings.EnableRedoc)
                //    Mod.Log($"Redoc API Browser available at:\n\t http://{host}:{port}" + $"{Settings.APIBasePath}/redoc/");
                //if (Settings.EnableScalar)
                //    Mod.Log($"Scalar API Browser available at:\n http://{host}:{port}" + $"{Settings.APIBasePath}/scalar/");
            }
            catch (Exception ex)
            {
                Mod.Log($"ERROR during initialization - {ex.Message}", ModManager.LogLevel.Error);
            }
        }

        public static void StopServices()
        {
            serverHost?.StopAsync();

            serverHost = null;

            Mod.Log("TreeStats Server Offline");
        }

        public static bool EnableEmulatorInfoOutput => Settings.EnableEmulatorInfoOutput;
    }
}
