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

namespace RevealRoles
{
    public class Player
    {
        public string name{get; set;}
        public string role{get; set;}
        public Player(){
            name = "";
            role = "";
        }
    }
    public sealed class RR
    {
        public bool msgFlag = false;
        public bool waitFlag = false;
        Timer timer;
        public List<Player> players = new List<Player>();
        private static RR _instance = new RR();
        public static RR GetInstance() { return _instance;}
        public BepInEx.Logging.ManualLogSource logger = null;
        public static void LogInfo(string msg){
            if(_instance.logger != null){
                _instance.logger.LogInfo(msg);
            }
        }
        public static void SetPlayers(){
            // プレイヤー一覧取得
            LogInfo("SetPlayers");
            foreach(PlayerControl pc in PlayerControl.AllPlayerControls)
            {
                Player p = new Player();
                p.name = pc.name;
                LogInfo(p.name);
                List<RoleInfo> roles = RoleInfo.getRoleInfoForPlayer(pc);
                foreach(RoleInfo rol in roles){
                    if(p.role.Length != 0){
                        p.role +=", ";
                    }
                    p.role += rol.name;
                }
                _instance.players.Add(p);
                _instance.msgFlag = true;
            }
        }
        private static void OnTimer(object o){
            LogInfo("OnTimer");
            _instance.waitFlag = true;
            _instance.timer.Dispose();
            _instance.timer = null;
        }
        public static void SendMessage(){
            // if (AmongUsClient.Instance.AmClient && DestroyableSingleton<HudManager>.Instance)
            if (DestroyableSingleton<HudManager>.Instance)
            {
                if(_instance.msgFlag){
                    // 時限式タイマーを有効化する
                    if(_instance.timer == null && !_instance.waitFlag)
                    {
                        LogInfo("SetTimer");
                        TimerCallback timerDelegate = new TimerCallback(OnTimer);
                        _instance.timer = new Timer(timerDelegate, null , 3000, 3000);
                    }
                    if(_instance.waitFlag){
                        LogInfo("SendMessage");
                        foreach(Player player in _instance.players){
                            string msg = $"{player.name} was {player.role}";
                            LogInfo(msg);
                            DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, msg);
                        }
                        _instance.waitFlag = false;
                        _instance.msgFlag = false;
                        _instance.players = new List<Player>();
                    }
                }
            }
        }
    }
}