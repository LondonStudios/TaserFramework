![enter image description here](https://i.imgur.com/IIyr58s.jpg)

# Taser Framework - London Studios
TaserFramework is a C# FiveM resource providing a host of functions to enhance the reality of the Taser X2 device. Features include custom external sounds, taser interface (CID) notifications, taser cartridge limit, taser reload functionality, taser safety mode, taser arc mode, taser cartridge reactivation, barb removal and distance based "ripping out". This plugin also replicates the Axon Audit trail through Discord integration, logging every usage of the taser.

Join our Discord [here](https://discord.gg/AtPt9ND).
Member/Usage Documentation [here](https://bit.ly/2zMsKHY).
## Installation
1. To stream the external taser sounds such as the drive stun, trigger sound and reactivate, another one of our resource is required - **PlayCustomSounds.**
This can be downloaded [here](https://github.com/LondonStudios/PlayCustomSounds). Please ensure you read the documentation on installing our custom sounds plugin correctly.

2. Once you have installed **PlayCustomSounds**, place the **three .ogg** **files** from the "sounds" folder into **html/sounds/** in PlayCustomSounds resource. These must **not** be renamed, and are not placed in the **TaserFramework** resource.
3. Next, install the **TaserFramework** files as a resource on your server. This comes with an **fxmanifest.lua** setup. If your server has issues with this type of file due to having an outdated CitizenFX server, you'll need a **resource.lua**.
Inside the **fxmanifest.lua** you have the ability to change the **DriveStun** key, currently set to E, however this can conflict with other plugins so it is up to you to select a key if you wish to change it. This can be changed on the line stating:

    **driveStunKey** '38'
    
	Please see FiveM Documentation for keybinds and their relevant numbers [here](https://docs.fivem.net/docs/game-references/controls/).
4. Moreover, setup a **webhook** on your **Discord server** and select the channel for your CEW-Audit log. Please find a Discord tutorial [here](https://support.discord.com/hc/en-us/articles/228383668-Intro-to-Webhooks) if you require support. We recommend setting your **webhook image** to [this](https://imgur.com/a/KkZZcif).
5. Next, open **"transmission.lua"** and edit Line 2 to include your Webhook URL, these should be inside the speech marks.

    **local webhookid** = "URL".
    
    If you do not wish to enable this, please change **"enabled"** to false. Do not edit anything **below** this line.
    
 6. Finally, you are now setup to start using TaserFramework by London Studios. We have created member documentation for you to share, please provide them the link [here](https://bit.ly/2zMsKHY).

