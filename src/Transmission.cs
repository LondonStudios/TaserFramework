using System;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;

namespace Server
{
    // Licence: London Studios 2020
    public class Transmission : BaseScript
    {
        public Transmission()
        {
            EventHandlers["Server:DetectSuspect"] += new Action<int, Vector3>((carrierNetworkId, carrierLocation) =>
            {
                TriggerClientEvent("Client:DetectSuspect", carrierNetworkId, carrierLocation);
            });

            EventHandlers["Server:SuspectDetected"] += new Action<int, int>((carrierNetworkId, suspectPlayerId) =>
            {
                TriggerClientEvent("Client:SuspectDetected", carrierNetworkId, suspectPlayerId);
            });

            EventHandlers["Server:ReactivateSuspect"] += new Action<int>((suspectPlayerId) =>
            {
                TriggerClientEvent("Client:ReactivateSuspect", suspectPlayerId);
            });

            EventHandlers["Server:UpdateLog"] += new Action<bool>((logStatus) =>
            {
                TriggerClientEvent("Client:UpdateLog", logStatus);
            });

            EventHandlers["Server:CheckLocation"] += new Action<int, int, Vector3, int>((suspectPlayerId, carrierNetId, carrierLocation, suspectNumber) =>
            {
                TriggerClientEvent("Client:CheckLocation", suspectPlayerId, carrierNetId, carrierLocation, suspectNumber);
            });

            EventHandlers["Server:BarbsInvalidated"] += new Action<int, int>((carrierNetId, suspectNumber) =>
            {
                TriggerClientEvent("Client:BarbsInvalidated", carrierNetId, suspectNumber);
            });

            EventHandlers["Server:NotifyBarbsRemoved"] += new Action<int>((suspectPlayerId) =>
            {
                TriggerClientEvent("Client:NotifyBarbsRemoved", suspectPlayerId);
            });

            EventHandlers["Server:DisplayNotification"] += new Action<string>((messageContents) =>
            {
                TriggerClientEvent("Client:DisplayNotification", messageContents);
            });
        }

    }
    /*  © 2020 - London Studios - Do not modify/change source code obtained permission. 
            This may be used on public/private FiveM servers and used in videos published to websites, 
            This is for non-commercial use. */
}
