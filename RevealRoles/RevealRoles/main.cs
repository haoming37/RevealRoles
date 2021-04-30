using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hazel;
using Logger = BepInEx.Logging.Logger;

namespace RevealRoles
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency("me.eisbison.theotherroles")]
    public class TemplatePlugin : BasePlugin
    {
        public const string Id = "jp.haoming.revealroles";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }
        // カスタムサーバー接続先設定
        public static ConfigEntry<string> Ip { get; set; }
        public static ConfigEntry<ushort> Port { get; set; }

        public override void Load()
        {
            // カスタムサーバーへの接続先を追加
            Ip = Config.Bind("Custom", "Custom Server IP", "18.177.110.86");
            Port = Config.Bind("Custom", "Custom Server Port", (ushort)22023);

            IRegionInfo customRegion = new DnsRegionInfo(Ip.Value, "HaomingAWS", StringNames.NoTranslation, Ip.Value, Port.Value).Cast<IRegionInfo>();
            ServerManager serverManager = DestroyableSingleton<ServerManager>.Instance;
            IRegionInfo[] regions = ServerManager.DefaultRegions;

            regions = regions.Concat(new IRegionInfo[] { customRegion }).ToArray();
            ServerManager.DefaultRegions = regions;
            serverManager.AvailableRegions = regions;
            serverManager.SaveServers();

            // RevealRolesにLoggerを登録
            RR.GetInstance().logger = Log;

            Harmony.PatchAll();
        }
    }
}
