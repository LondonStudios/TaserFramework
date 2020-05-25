using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using static TaserFramework.Framework;

namespace TaserFramework
{
    public class Detection : BaseScript
    {
        public int loadedCartridges = 2;
        private static int transportCartridges = 2;
        public static int driveStunKey = 38;
        public int taserHash = GetHashKey("WEAPON_STUNGUN");
        Ped player = Game.Player.Character;
        public int netid = Game.Player.Character.NetworkId;
        private DateTime drivestuntime;
        public bool taserSafetyStatus = false;
        private bool awaitingReactivation1 = false;
        private bool awaitingReactivation2 = false;
        public int suspect1 = 0;
        public int suspect2 = 0;
        private bool detectedAiming = false;
        private bool displayedInitial = false;

        // Licence: London Studios 2020
        public Detection()
        {
            Tick += OnTick;
            var getDriveStunKey = int.TryParse(GetResourceMetadata(GetCurrentResourceName(), "driveStunKey", 0), out driveStunKey);
            SetPedMinGroundTimeForStungun(PlayerPedId(), 5000);
            EventHandlers["Client:DetectSuspect"] += new Action<int, Vector3>((carrierNetworkId, carrierLocation) =>
            {
                if (IsPedRagdoll(PlayerPedId()) || IsPedBeingStunned(PlayerPedId(), 0))
                {
                    Vector3 suspectLocation = Game.Player.Character.Position;
                    var compare = Vdist(suspectLocation.X, suspectLocation.Y, suspectLocation.Z, carrierLocation.X, carrierLocation.Y, carrierLocation.Z);
                    if (compare < 30.0f)
                    {
                        TriggerServerEvent("Server:SuspectDetected", carrierNetworkId, netid);
                    }
                }
            });

            EventHandlers["Client:SuspectDetected"] += new Action<int, int>((carrierNetworkId, suspectPlayerId) =>
            {
                if (netid == carrierNetworkId)
                {
                    if (suspect1 == 0 & loadedCartridges == 1)
                    {
                        suspect1 = suspectPlayerId;
                        awaitingReactivation1 = true;
                    }
                    else if (suspect2 == 0 & loadedCartridges == 0)
                    {
                        suspect2 = suspectPlayerId;
                        awaitingReactivation2 = true;
                    }
                }
            });

            EventHandlers["Client:ReactivateSuspect"] += new Action<int>((suspectPlayerId) =>
            {
                if (netid == suspectPlayerId)
                {
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~: You are being ~y~reactivated~w~.");
                    SetPedToRagdoll(PlayerPedId(), 5000, 5000, 0, true, true, false);
                }
            });

            EventHandlers["Client:CheckLocation"] += new Action<int, int, Vector3, int>((suspectPlayerId, carrierNetId, carrierLocation, suspectNumber) =>
            {
                if (netid == suspectPlayerId)
                {
                    Vector3 suspectLocation = Game.Player.Character.Position;
                    var compareDistance = Vdist(suspectLocation.X, suspectLocation.Y, suspectLocation.Z, carrierLocation.X, carrierLocation.Y, carrierLocation.Z);
                    if (compareDistance > 30.0f)
                    {
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: Your barbs have been ~y~disconnected~w~.");
                        TriggerServerEvent("Server:BarbsInvalidated", carrierNetId, suspectNumber);
                    }
                }
            });

            EventHandlers["Client:BarbsInvalidated"] += new Action<int, int>((carrierNetId, suspectNumber) =>
            {
                if (netid == carrierNetId)
                {
                    if (suspectNumber == 1)
                    {
                        awaitingReactivation1 = false;
                        suspect1 = 0;
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: \n~y~Barbs Disconnected - ~b~Cartridge 1");
                    }
                    else if (suspectNumber == 2)
                    {
                        awaitingReactivation2 = false;
                        suspect2 = 0;
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: \n~y~Barbs Disconnected - ~b~Cartridge 2");
                    }
                }
            });

            EventHandlers["Client:NotifyBarbsRemoved"] += new Action<int>((suspectPlayerId) =>
            {
                if (netid == suspectPlayerId)
                {
                    Screen.ShowNotification("~h~~r~TASER X2~s~~h~: \nYour barbs have been ~y~removed~w~.");
                }
            });

            EventHandlers["Client:DisplayNotification"] += new Action<string>((messageContents) =>
            {
                DisplayTopNotification(messageContents);
            });

            EventHandlers["Client:UpdateLog"] += new Action<bool>((logStatus) =>
            {
                LogStatus(logStatus);
            });
        }

        async Task OnTick()
        {
            if (HudWeaponWheelGetSelectedHash() == taserHash)
            {
                if (displayedInitial == false)
                {
                    var taserID = GetTaserInformation();
                    Debug.WriteLine($"Taser issued - Serial: {taserID}");
                    Screen.ShowNotification($"~r~~h~TASER X2~h~~s~: Issued:\nSerial Number: ~y~{taserID}\n~p~Made by London Studios");
                    displayedInitial = true;
                }

                if ((API.IsControlJustReleased(1, driveStunKey) && ((GetSelectedPedWeapon(PlayerPedId()) == taserHash)) && !IsPedInAnyVehicle(PlayerPedId(), false) & (taserSafetyStatus == false)))
                {
                    if ((DateTime.Now - drivestuntime).TotalMilliseconds > (double)5000)
                    {
                        DriveStun(loadedCartridges, transportCartridges);
                        drivestuntime = DateTime.Now;
                    }
                    else
                    {
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~y~Drive Stun Cooldown - ~w~5 Seconds");
                    }
                }

                if (player.IsAiming && ((GetSelectedPedWeapon(PlayerPedId()) == taserHash) && (detectedAiming == false)))
                {
                    UpdateLog("Armed - Red Dot", loadedCartridges, DateTime.Now, 16722944);
                    detectedAiming = true;
                }

                if (awaitingReactivation1 == true || awaitingReactivation2 == true)
                {
                    if (API.IsControlJustReleased(1, 10) && awaitingReactivation1 == true)
                    {
                        DisplayTopNotification("Reactivated cartridge: ~INPUT_SELECT_WEAPON_UNARMED~");
                        Screen.ShowNotification("~h~~r~TASER X2~s~~h~: \nUse ~y~/rb1 (Remove Barbs)");
                        ReactivateSuspect(loadedCartridges, suspect1, 1);
                    }
                    else if (API.IsControlJustReleased(1, 11) & awaitingReactivation2 == true)
                    {
                        DisplayTopNotification("Reactivated cartridge: 	~INPUT_SELECT_WEAPON_MELEE~");
                        Screen.ShowNotification("~h~~r~TASER X2~s~~h~: \nUse ~y~/rb2 (Remove Barbs)");
                        ReactivateSuspect(loadedCartridges, suspect2, 2);
                    }
                    if (awaitingReactivation1 == true)
                    {
                        if (IsPedDeadOrDying(PlayerPedId(), true))
                        {
                            awaitingReactivation1 = false;
                        }
                        Vector3 carrierLocation = Game.Player.Character.Position;
                        TriggerServerEvent("Server:CheckLocation", suspect1, netid, carrierLocation, 1);
                    }
                    if (awaitingReactivation2 == true)
                    {
                        if (IsPedDeadOrDying(PlayerPedId(), true))
                        {
                            awaitingReactivation2 = false;
                        }
                        Vector3 carrierLocation = Game.Player.Character.Position;
                        TriggerServerEvent("Server:CheckLocation", suspect2, netid, carrierLocation, 2);
                    }
                }
                if (HudWeaponWheelGetSelectedHash() == taserHash && (loadedCartridges == 0 || taserSafetyStatus == true))
                {
                    TaserLock(false);
                }

                if (IsPedShooting(PlayerPedId()) && (GetSelectedPedWeapon(PlayerPedId()) == taserHash))
                {
                    Vector3 carrierLocation = Game.Player.Character.Position;
                    TriggerServerEvent("Server:DetectSuspect", netid, carrierLocation);
                    var cartridge = 0;
                    loadedCartridges--;
                    TriggerServerEvent("Server:SoundToRadius", netid, 30.0f, "taser", 0.95f);
                    DisplayStatus(loadedCartridges, "MANUAL", transportCartridges);
                    if (loadedCartridges == 1)
                    {
                        cartridge = 1;
                    }
                    else if (loadedCartridges == 0)
                    {
                        cartridge = 2;
                    }
                    UpdateLog("Trigger - Cartridge " + cartridge, loadedCartridges, DateTime.Now, 16753920);
                }
            }
        }

        private void DisplayTopNotification(string text)
        {
            BeginTextCommandDisplayHelp ("STRING");
            AddTextComponentSubstringPlayerName(text);
            SetNotificationTextEntry("STRING");
            EndTextCommandDisplayHelp(0, false, true, -1);
        }

        [Command("rt")]
        private void taserReloadHandler()
        {
            switch (loadedCartridges)
            {
                case 0:
                    if (transportCartridges == 2)
                      {
                        transportCartridges = 0;
                        loadedCartridges = 2;
                        TaserLock(false);
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RELOADED:\n~y~SPARE CARTRIDGES: ~w~0");
                        UpdateLog("Reload - 2 Cartridges", loadedCartridges, DateTime.Now, 589883);
                        detectedAiming = false;
                    }
                    else if (transportCartridges == 1)
                    {
                        transportCartridges = 0;
                        loadedCartridges = 1;
                        TaserLock(false);
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RELOADED:\n~y~SPARE CARTRIDGES: ~w~0");
                        UpdateLog("Reload - Cartridge 1", loadedCartridges, DateTime.Now, 589883);
                        detectedAiming = false;
                    }
                    else if (transportCartridges == 0)
                    {
                        Screen.ShowNotification("~h~~r~TASER X2~s~:\n~s~~h~NO SPARE CARTRIDGES");
                    }    
                    break;
                case 1:
                    if (transportCartridges == 2)
                    {
                        transportCartridges = 1;
                        loadedCartridges = 2;
                        TaserLock(false);
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RELOADED:\n~y~SPARE CARTRIDGES: ~w~1");
                        UpdateLog("Reload - Cartridge 1", loadedCartridges, DateTime.Now, 589883);
                        detectedAiming = false;
                    }
                    else if (transportCartridges == 1)
                    {
                        transportCartridges = 0;
                        loadedCartridges = 2;
                        TaserLock(false);
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RELOADED:\n~y~SPARE CARTRIDGES: ~w~0");
                        UpdateLog("Reload - Cartridge 1", loadedCartridges, DateTime.Now, 589883);
                        detectedAiming = false;
                    }
                    break;
                case 2:
                    if (transportCartridges == 2)
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~SLOTS FULL:\n~y~SPARE CARTRIDGES: ~w~2");
                    if (transportCartridges == 1)
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~SLOTS FULL:\n~y~SPARE CARTRIDGES: ~w~1");
                    if (transportCartridges == 0)
                        Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~SLOTS FULL:\n~y~SPARE CARTRIDGES: ~w~0");
                    break;
                default:
                    loadedCartridges = 2;
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RELOADED");
                    detectedAiming = false;
                    break;
            }
        }

        [Command("resettaser")]
        private void resetTaser()
        {
            loadedCartridges = 2;
            transportCartridges = 2;
            detectedAiming = false;
            awaitingReactivation1 = false;
            awaitingReactivation2 = false;
            suspect1 = 0;
            suspect2 = 0;
            TaserLock(false);
            Screen.ShowNotification("~r~~h~TASER X2~h~~s~: ~g~RESET COMPLETE");
        }

        [Command("ts")]
        private void taserSafety()
        {
            taserSafetyStatus = TaserSafety(loadedCartridges, taserSafetyStatus);
        }

        [Command("rb1")]
        private void removeBarbs1()
        {
            DisplayTopNotification("Barbs Removed - Cartridge: ~INPUT_SELECT_WEAPON_UNARMED~");
            awaitingReactivation1 = false;
            suspect2 = 0;
        }
        [Command("rb2")]
        private void removeBarbs2()
        {
            DisplayTopNotification("Barbs Removed - Cartridge: ~INPUT_SELECT_WEAPON_UNARMED~");
            awaitingReactivation2 = false;
            suspect2 = 0;
        }

        [Command("taserlogging")]
        private void taserLogging()
        {
            if (logEnabled == true)
            {
                TriggerServerEvent("Server:UpdateLog", false);
                TriggerServerEvent("Server:DisplayNotification", "Taser - Tracking Disabled");
                Screen.ShowNotification("~r~~h~TASER X2~h~~s~: \n~y~FORCEWIDE LOGGING - ~r~DISABLED");
                OverrideLog("Forcewide Tracking - Disabled", loadedCartridges, DateTime.Now, 10093824);
            }
            else if (logEnabled == false)
            {
                TriggerServerEvent("Server:UpdateLog", true);
                TriggerServerEvent("Server:DisplayNotification", "Taser - Tracking Enabled");
                Screen.ShowNotification("~r~~h~TASER X2~h~~s~: \n~y~FORCEWIDE LOGGING - ~g~ENABLED");
                OverrideLog("Forcewide Tracking - Enabled", loadedCartridges, DateTime.Now, 10093824);
            }
        }
    }
    /*  © 2020 - London Studios - Do not modify/change source code obtained permission. 
            This may be used on public/private FiveM servers and used in videos published to websites, 
            This is for non-commercial use. */
}
