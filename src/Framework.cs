using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;

namespace TaserFramework
{
    // Licence: London Studios 2020
    public class Framework : BaseScript
    {
        private static string serialNumber = "X" + new Random().Next(21202574, 49202574);
        private static string batteryStatus = Convert.ToString(new Random().Next(95, 99));
        public static bool logEnabled = true;

        public static void DisplayStatus(int cartridges, string mode, int transportCartridges)
        {
            switch (cartridges)
            {
                case 0:
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~MODE: ~w~" + mode + "\n\n~r~RELOAD REQUIRED");
                    break;
                case 1:
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~MODE: ~w~" + mode + "\n\n~y~        |25|\n\n~r~SPARE CARTRIDGES: ~y~" + transportCartridges);
                    break;
                case 2:
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~MODE: ~w~" + mode + "\n\n~y~|25|     |25|\n\n~r~SPARE CARTRIDGES: ~y~" + transportCartridges);
                    break;
                default:
                    Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~MODE: ~w~" + mode + "\n\n~y~|25|     |25|\n\n~r~SPARE CARTRIDGES: ~y~" + cartridges);
                    break;
            }
        }
        public static void DriveStun(int cartridges, int transportCartridges)
        {
            int netid = Game.Player.Character.NetworkId;
            int handle = PlayerPedId();
            uint hash = (uint)GetHashKey("Ballistic");
            SetPlayerSimulateAiming(handle, true);
            uint motionhash = 0x3f67c6af;
            ForcePedMotionState(handle, motionhash, false, false, false);
            SetWeaponAnimationOverride(handle, hash);
            DisplayStatus(cartridges, "ARC", transportCartridges);
            TriggerServerEvent("Server:SoundToRadius", netid, 30.0f, "arcsound", 0.95f);
            UpdateLog("Arc", cartridges, DateTime.Now, 1672140);
        }

        public static void TaserLock(bool locked)
        {
            DisablePlayerFiring(PlayerPedId(), locked);
            SetPlayerCanDoDriveBy(PlayerPedId(), !locked);
        }

        public static void LogStatus(bool logStatus)
        {
            if (logStatus == true)
            {
                logEnabled = true;
            }
            if (logStatus == false)
            {
                logEnabled = false;
            }
        }

        public static void UpdateLog(string action, int cartridges, DateTime time, int colour)
        {
            if (logEnabled == true)
            {
                Vector3 position = Game.Player.Character.Position;
                uint streetName = 0;
                uint crossingRoad = 0;
                GetStreetNameAtCoord(position.X, position.Y, position.Z, ref streetName, ref crossingRoad);
                var eventId = Convert.ToString(new Random().Next(1020263, 8029263));
                TriggerServerEvent("submitLog", GetPlayerName(PlayerId()), action, cartridges, Convert.ToString(time) + " - LondonStudios 2020", "Taser X2", serialNumber, batteryStatus, colour, GetStreetNameFromHashKey(streetName), eventId);
            }
        }

        public static void OverrideLog(string action, int cartridges, DateTime time, int colour)
        {
            Vector3 position = Game.Player.Character.Position;
            uint streetName = 0;
            uint crossingRoad = 0;
            GetStreetNameAtCoord(position.X, position.Y, position.Z, ref streetName, ref crossingRoad);
            TriggerServerEvent("submitLog", GetPlayerName(PlayerId()), action, cartridges, Convert.ToString(time) + " - LondonStudios 2020", "Taser X2", serialNumber, batteryStatus, colour, GetStreetNameFromHashKey(streetName));
        }

        public static bool TaserSafety(int cartridges, bool taserSafetyStatus)
        {
            if (taserSafetyStatus == false)
            {
                taserSafetyStatus = true;
                UpdateLog("Safety Activated", cartridges, DateTime.Now, 3257928);
                Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~SAFETY: ~y~ACTIVATED");
                return taserSafetyStatus;
            }
            else if (taserSafetyStatus == true)
            {
                taserSafetyStatus = false;
                UpdateLog("Safety Deactivated", cartridges, DateTime.Now, 15158332);
                Screen.ShowNotification("~r~~h~TASER X2~h~~s~ - ~y~CID\n\n~r~SAFETY: ~y~DEACTIVATED");
                return taserSafetyStatus;
            }
            return taserSafetyStatus;
        }

        public static string GetTaserInformation()
        {
            return serialNumber;
        }

        public static void ReactivateSuspect(int loadedCartridges, int suspectPlayerId, int cartridgenumber)
        {
            TriggerServerEvent("Server:ReactivateSuspect", suspectPlayerId);
            UpdateLog("Reactivation - Cartridge " + cartridgenumber, loadedCartridges, DateTime.Now, 16734208);
            TriggerServerEvent("Server:SoundToRadius", Game.Player.Character.NetworkId, 30.0f, "reactivate", 0.95f);
        }
    }
    /*  © 2020 - London Studios - Do not modify/change source code obtained permission. 
            This may be used on public/private FiveM servers and used in videos published to websites, 
            This is for non-commercial use. */
}
