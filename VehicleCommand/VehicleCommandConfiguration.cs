using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AdvancedRestriction
{
    public class AdvancedRestrictionConfiguration : IRocketPluginConfiguration
    {
        public string LicenseKey;
        public string Permission;
        public string WhitelistPermission;

        public class VehicleIDS
        {
            public string ID;
        }

        public class ItemIDS
        {
            public string ID;
        }

        [XmlArrayItem("IDS")]
        [XmlArray(ElementName = "BlacklistedVehicles")]
        public List<VehicleIDS> VehicleID;

        [XmlArrayItem("IDS")]
        [XmlArray(ElementName = "BlacklistedItems")]
        public List<ItemIDS> ItemID;

        public void LoadDefaults()
        {
            LicenseKey = "License-Key-Here";
            Permission = "LYHME.Restriction";
            WhitelistPermission = "LYHME.RestrictionBypass";



            VehicleID = new List<VehicleIDS>()
            {
                new VehicleIDS() {ID="25" },
                new VehicleIDS() {ID="54" },
                new VehicleIDS() {ID="12" }
            };


            ItemID = new List<ItemIDS>()
            {
                new ItemIDS() {ID="22" },
                new ItemIDS() {ID="4" }
            };
        }
    }
}
