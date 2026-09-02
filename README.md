![London Studios](https://i.ibb.co/1mwSS1q/Untitled-design.png)

# This resource is archived

**Taser Framework** is a free FiveM resource we released in 2020. It is now **archived** and **no longer supported**.

If you want a modern, maintained taser system with a clearer CID, synced laser and flashlight, 3D sounds, cartridge items and framework support, get **Smart Taser** from our store:

### **[Get Smart Taser](https://store.londonstudios.net/package/smart-taser)**

---

# Upgrade to Smart Taser

Since forming London Studios in April 2020 we've created a number of **high quality** and **premium** resources for the FiveM project, focusing on the emergency services and aiming to bring your server to the next level.

Although we made a number of free resources such as this one in the first year, we've now switched to creating paid content, keeping them constantly updated and working along with providing the best possible support to our customers.

With **thousands** of **happy customers** we are confident you'll love our resources and our active support team are on hand to help if you have any questions!

**Smart Taser** is the successor to Taser Framework (the “V2” we mentioned years ago). It keeps the core CEW features you know, and builds on them with better immersion, clearer feedback and proper framework / inventory support.

### What’s improved in Smart Taser

* **CID interface** - Cartridge status and safety shown clearly with custom icons
* **Synced red laser** - Accurate aiming, visible to other players
* **Synced flashlight** - Toggleable beam fully synced for everyone nearby
* **3D custom sounds** - Proper positional audio for a more realistic CEW experience
* **Two cartridges** - Full reload flow, with item support for QBCore, ESX and vRP (standalone has unlimited cartridges)
* **Taser safety** - Toggle safety on the device, with status on the CID
* **Taser arc mode** - Drive stun when close enough, without firing a cartridge
* **Cartridge reactivation** - Re-energise barbs already attached to a suspect
* **Barb removal** - Manual removal by the firing officer, or automatic rip-out past the distance limit
* **Axon Audit Trail** - Discord logging of taser usage, including device and cartridge IDs
* **Framework support** - Works standalone, with integrations for common frameworks and inventories (including OX Inventory)
* **Multiple taser models** - Configure additional stun guns beyond the base GTA stungun

![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser1.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser2.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser3.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser4.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser5.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser6.png)
![Smart Taser](https://img.londonstudios.net/cdn-cgi/image/width=2048,format=auto,quality=95/smart-taser/Taser7.png)

Video demo: [Smart Taser on YouTube](https://youtu.be/xQcdUfkoBDU)

# Our store: https://store.londonstudios.net/package/smart-taser
# Our discord: https://discord.gg/nC2krpN

Documentation: [Smart Taser docs](https://docs.londonstudios.net/london-studios-documentation/resources/smart-taser)

---

# Taser Framework (legacy) - London Studios

> **Archived / unsupported.** This free resource is likely **out of date** and is **no longer supported**. The full source code remains available if you wish to maintain it yourself. For an updated CEW system with ongoing updates and support, use **[Smart Taser](https://store.londonstudios.net/package/smart-taser)**.

TaserFramework is a C# FiveM resource providing a host of functions to enhance the reality of the Taser X2 device. Features include custom external sounds, taser interface (CID) notifications, taser cartridge limit, taser reload functionality, taser safety mode, taser arc mode, taser cartridge reactivation, barb removal and distance based "ripping out". This plugin also replicates the Axon Audit trail through Discord integration, logging every usage of the taser.

![enter image description here](https://i.imgur.com/IIyr58s.jpg)

Latest legacy version: **1.1** - 08/06/2020

If you like our work and want to support us, buy us a coffee [here](https://www.buymeacoffee.com/londonstudios)

Join our Discord [here](https://discord.gg/nC2krpN).

Legacy member/usage documentation [here](https://bit.ly/2zMsKHY).

Changelog:
-
1/06/2020 - Update pushed, increased efficiency, bugs fixed and customisable keybinds! Easy installation and no need to install another sound plugin.

## Installation (legacy)

1. Firstly, install the **TaserFramework** files as a resource on your server. This comes with an **fxmanifest.lua** setup. If your server has issues with this type of file due to having an outdated CitizenFX server, you'll need a **__resource.lua**. Please do not edit the fxmanifest.lua. Next, "ensure" the resource in your server.cfg.
   
2. Secondly setup a **webhook** on your **Discord server** and select the channel for your CEW-Audit log. Please find a Discord tutorial [here](https://support.discord.com/hc/en-us/articles/228383668-Intro-to-Webhooks) if you require support. We recommend setting your **webhook image** to [this](https://imgur.com/a/KkZZcif).

3. Next, open **"transmission.lua"** and edit Line 2 to include your Webhook URL, these should be inside the speech marks. Please add your full URL from the webhook.

    **local webhookid** = "HERE".
    
    If you do not wish to enable this, please change **"enabled"** to false. Do not edit anything **below** this line.
    
4. Finally, you are now setup to start using TaserFramework by London Studios. We have created member documentation for you to share, please provide them the link [here](https://bit.ly/2zMsKHY).

This is how your folder should look:
![enter image description here](https://i.imgur.com/foCPYd9.png)

If you require any support on our **current** products, join the [Discord](https://discord.gg/nC2krpN) and request assistance. This legacy resource itself is no longer supported.
