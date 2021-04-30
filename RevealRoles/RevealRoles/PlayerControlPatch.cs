
using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using UnityEngine;
using Hazel;
using TheOtherRoles;

// using SystemTypes = BCPJLGGNHBC;
// using Palette = BLMBFIODBKL;
// using Constants = LNCOKMACBKP;
// using PhysicsHelpers = FJFJIDCFLDJ;
// using DeathReason = EGHDCAKGMKI;
// using GameOptionsData = CEIOGGEDKAN;
// using Effects = AEOEPNHOJDP;

namespace RevealRoles {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerControlFixedUpdatePatch{
        public static void Prefix(PlayerControl __instance) {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Joined) return;
            if(RR.GetInstance().players.Count >= 0){
                RR.SendMessage();
            }
        }
    }
}