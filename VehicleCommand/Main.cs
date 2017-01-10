using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Player;

namespace AdvancedRestriction
{
    public class Main : RocketPlugin<AdvancedRestrictionConfiguration>, IRocketPlugin
    {
        public static Main Instance;
        public static ushort? newid2;
        public static List<ushort?> vehblockedids = new List<ushort?>();
        public static List<ushort?> itemblockedids = new List<ushort?>();

        protected override void Load()
        {

            Instance = this;

            Licensing.CheckLicense(Configuration.Instance.LicenseKey, 45);
            Rocket.Core.Logging.Logger.Log("LYHMEHosting -- AdvancedRestriction has loaded.");

            foreach(AdvancedRestrictionConfiguration.VehicleIDS id in Configuration.Instance.VehicleID)
            {
                Rocket.Core.Logging.Logger.Log("Vehicle ID blocked: " + id.ID);
                newid2 = Convert.ToUInt16(id.ID);
                vehblockedids.Add(newid2);
            }

            if (!Licensing.valid)
            {
                Unload();
            }
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("LYHMEHosting -- AdvancedRestriction has unloaded.");

            vehblockedids.Clear();
            itemblockedids.Clear();
        }

        private void FixedUpdate()
        {

        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"lyhme_v_blocked", "This vehicle has been blocked." },
                };
            }
        }
    }
}
