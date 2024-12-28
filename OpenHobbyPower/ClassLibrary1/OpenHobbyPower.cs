//欢迎来到 我的项目 OpenHobbyPower 本人第一次写C#的插件 如有问题 请多多包涵谢谢
//Email：scpslgame@dingtalk.com

using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Interfaces;
using Exiled.Loader;
using Exiled.API.Features.Items;
using MEC;

namespace OpenHobbyPower
{
    public class OpenHobbyPower : Plugin<Config>
    {
        public override string Name => "OpenHobbyPower";
        public override string Author => "Lively-Xuan";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
            LogMessage();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;
            base.OnDisabled();
        }

        private void OnRoundStarted()
        {
            Map.TurnOffAllLights(42f);

            foreach (var player in Player.List)
            {
                var flashlight = (Flashlight)Item.Create(ItemType.Flashlight, player);
                player.AddItem(flashlight);
                player.ShowHint("<color=#E4080A>因SCP突破收容 正在切换到备用电源 请使用手电筒！ ", 10);
                Timing.CallDelayed(42f, () => player.ShowHint("<color=#7DDA58>已切换到备用电源 ", 5));
            }
        }

        private void OnWaitingForPlayers()
        {
        }

        private void LogMessage()
        {
            Log.Info("开局掐灯已开启 From.Lively-Xuan");

        }
    }

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
