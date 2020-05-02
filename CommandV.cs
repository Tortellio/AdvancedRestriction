using Rocket.API;
using Rocket.API.Extensions;
using Rocket.Core.Logging;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedRestriction
{
    class CommandV : IRocketCommand
    {
        public string Help
        {
            get { return "Spawn a Vehicle"; }
        }

        public string Name
        {
            get { return "v"; }
        }

        public string Syntax
        {
            get { return "id"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "LYHME.vehicle" };
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            
            var newclass = new AdvancedRestrictionConfiguration.VehicleIDS();
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, U.Translate("command_generic_invalid_parameter"));
                throw new WrongUsageOfCommandException(caller, this);
            }

            ushort? id = command.GetUInt16Parameter(0);

            if (!id.HasValue)
            {
                string itemString = command.GetStringParameter(0);

                if (itemString == null)
                {
                    UnturnedChat.Say(caller, U.Translate("command_generic_invalid_parameter"));
                    throw new WrongUsageOfCommandException(caller, this);
                }

                Asset[] assets = SDG.Unturned.Assets.find(EAssetType.VEHICLE);
                foreach (VehicleAsset ia in assets)
                {
                    if (ia != null && ia.vehicleName != null && ia.vehicleName.ToLower().Contains(itemString.ToLower()))
                    {
                        id = ia.id;
                        break;
                    }
                }
                if (!id.HasValue)
                {
                    UnturnedChat.Say(caller, U.Translate("command_generic_invalid_parameter"));
                    throw new WrongUsageOfCommandException(caller, this);
                }
            }

            Asset a = SDG.Unturned.Assets.find(EAssetType.VEHICLE, id.Value);
            string assetName = ((VehicleAsset)a).vehicleName;

            IRocketPlayer plr = (IRocketPlayer)player;
            if (Main.vehblockedids.Contains(id.Value))
            {
                if (!plr.HasPermission(Main.Instance.Configuration.Instance.WhitelistPermission))
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("lyhme_v_blocked"));
                }
                else
                {
                    VehicleTool.giveVehicle(player.Player, id.Value);
                    Logger.Log(U.Translate("command_v_giving_console", player.CharacterName, id));
                    UnturnedChat.Say(caller, U.Translate("command_v_giving_private", assetName, id));
                }
            }
            else
            {
                VehicleTool.giveVehicle(player.Player, id.Value);
                Logger.Log(U.Translate("command_v_giving_console", player.CharacterName, id));
                UnturnedChat.Say(caller, U.Translate("command_v_giving_private", assetName, id));
            }
        }
    }
}
