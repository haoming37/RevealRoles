using HarmonyLib;
using System.Threading.Tasks;

namespace RevealRoles {
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class ShipStatusStartPatch{
        public static void Prefix(){
            RR.LogInfo("ShipStatusStartPatch");
            RR.SetPlayers();
            return;
        }

    }
}