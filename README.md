

![enter image description here](https://i.imgur.com/IIyr58s.jpg)

# Taser Framework - London Studios
TaserFramework is a C# FiveM resource providing a host of functions to enhance the reality of the Taser X2 device. Features include custom external sounds, taser interface (CID) notifications, taser cartridge limit, taser reload functionality, taser safety mode, taser arc mode, taser cartridge reactivation, barb removal and distance based "ripping out". This plugin also replicates the Axon Audit trail through Discord integration, logging every usage of the taser.

Latest version: 1.1 - 08/06/2020

If you like our work and want to support us, buy us a coffee [here](https://www.buymeacoffee.com/londonstudios)

Join our Discord [here](https://discord.gg/AtPt9ND).
Member/Usage Documentation [here](https://bit.ly/2zMsKHY).

Changelog:
-
1/06/2020 - Update pushed, increased efficiency, bugs fixed and customisable keybinds! Easy installation and no need to install another sound plugin.
## Installation
1. Firstly, install the **TaserFramework** files as a resource on your server. This comes with an **fxmanifest.lua** setup. If your server has issues with this type of file due to having an outdated CitizenFX server, you'll need a **__resource.lua**. Please do not edit the fxmanifest.lua. Next, "ensure" the resource in your server.cfg.
   
4. Secondly setup a **webhook** on your **Discord server** and select the channel for your CEW-Audit log. Please find a Discord tutorial [here](https://support.discord.com/hc/en-us/articles/228383668-Intro-to-Webhooks) if you require support. We recommend setting your **webhook image** to [this](https://imgur.com/a/KkZZcif).
5. Next, open **"transmission.lua"** and edit Line 2 to include your Webhook URL, these should be inside the speech marks. Please add your full URL from the webhook.

    **local webhookid** = "HERE".
    
    If you do not wish to enable this, please change **"enabled"** to false. Do not edit anything **below** this line.
    
 6. Finally, you are now setup to start using TaserFramework by London Studios. We have created member documentation for you to share, please provide them the link [here](https://bit.ly/2zMsKHY).

This is how your folder should look:
![enter image description here](https://i.imgur.com/foCPYd9.png)

If you require any support, join the Discord and request assistance!
