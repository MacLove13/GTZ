using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class ZombiePed : ControllablePed {

        public ZombiePed() : base("ZOMBIE_PED", 100f,
            new PedProperties {

                IsFriendly = false,
                IsZombie = true,

                SpawnRandomWeapons = true,
                RandomWeapons =
                    new[] {
                        WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat,
                        WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub
                    },

                Weapons = null,
                PreferredWeapon = WeaponHash.Unarmed,

                Armor = 0,
                Accuracy = 5,
                MaxHealth = 74,
                Health = 74,

                AttachBlip = true,
                BlipColor = BlipColor.Red,

                RecordKeys = true,

                Teleport = false,
                HasMenu = true,
                MenuKey = Keys.E

            }) {
         
            SetInteractionDistance(1f);
            Initialize += OnInitialize;
            Dead += OnDead;

        }

        private void OnDead(object sender, EventArgs eventArgs) {
            // DropInventoryLootbag(Entity.Position);
        }

        private void OnInitialize(object sender, EventArgs eventArgs) {
            Ped.AlwaysKeepTask = true;
            Ped.Task.FightAgainst(Main.Player.Character);
        }

    }

}
