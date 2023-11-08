using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TemporaryServerJoinFix
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class TempServerFix : BaseUnityPlugin
    {
        private const string modGUID = "Posiedon.SJF";
        private const string modName = "Lethal Company server join fix";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);
        public static ManualLogSource mls;

        void Awake()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("SJF");
            // Plugin startup logic
            mls.LogInfo("Loaded server join fix. Patching.");
            harmony.PatchAll(typeof(TempServerFix));
        }

        [HarmonyPatch(typeof(GameNetworkManager), "Singleton_OnClientDisconnectCallback")]
        [HarmonyPostfix]
        static void FixBadServerJoin()
        {
            SceneManager.LoadScene("MainMenu");
            mls.LogInfo("Loading Main Menu");
        }

    }
}
