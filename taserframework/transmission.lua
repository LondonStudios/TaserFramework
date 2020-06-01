--Add your Discord Webhook ID below:
local webhookid = "HERE"
--Disable Discord Tracking below:
local enable = true

RegisterNetEvent("submitLog")

AddEventHandler("submitLog",function(identifier, event, cartridges, time, device, serial, battery, colour, streetname, eventID)
	if (enable == true) then
		sendToDiscord(identifier, event, cartridges, time, device, serial, battery, colour, streetname, eventID)
	end
end)

function sendToDiscord(identifier, event, cartridges, time, device, serial, battery, colour, streetname, eventID)
      local embed = {
            {
                ["fields"] = {
				    {
					    ["name"] = "**Event:**",
					    ["value"] = event,
					    ["inline"] = true
				    },
					{
					    ["name"] = "**Device:**",
					    ["value"] = device,
					    ["inline"] = true
				    },
				    {
					    ["name"] = "**Carrier:**",
					    ["value"] = identifier,
					    ["inline"] = true
				    },
                    {
					    ["name"] = "**Cartridges:**",
					    ["value"] = cartridges,
					    ["inline"] = true
				    },
					{
					    ["name"] = "**CEW Serial:**",
					    ["value"] = serial,
					    ["inline"] = true
				    },
					{
					    ["name"] = "**Battery:**",
					    ["value"] = battery .. "%",
					    ["inline"] = true
				    },
					{
					    ["name"] = "**Location:**",
					    ["value"] = streetname,
					    ["inline"] = true
				    },
					{
					    ["name"] = "**Event ID:**",
					    ["value"] = eventID,
					    ["inline"] = true
				    },
			    },
                ["color"] = colour,
                ["title"] = "**CEW - Event Log**",
                ["description"] = message,
                ["footer"] = {
                    ["text"] = "Timestamp: "..time,
                },
                ["thumbnail"] = {
                    ["url"] = "https://i.imgur.com/Q6W9wR1.png",
				},
			}
        }

  PerformHttpRequest(webhookid, function(err, text, headers) end, 'POST', json.encode({username = "Axon Audit Trail", embeds = embed}), { ['Content-Type'] = 'application/json' })
end

--            � 2020 - London Studios - Do not modify/change source code obtained permission. 
--            This may be used on public/private FiveM servers and used in videos published to websites, 
--            This is for non-commercial use.