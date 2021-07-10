# My-scripts-for-Unity
This repository contains my C# scripts for games on Unity.
I present to your attention some of the scripts used in the development of the game Forest Revenge.

The WaterFlow.cs script allows you to adjust the buoyancy force, the force of the water flow if it is a river, the speed of surface displacement. In addition, when the wheels interact with the trigger, a water spray particle system is triggered.

The script Inventory.cs is designed to organize inventory with the ability to increase the maximum number of slots for items.

The Chunk.cs and ChunkPlacer.cs scripts are designed to randomly generate level plots as the player approaches the last piece of EndPoint. The likelihood of individual areas appearing is configured using AnimationCurve. Checking the distance traveled is not carried out in the update function, but with the help of a coroutine every 0.5 seconds.
There is an implementation of removing already passed sections and moving the stub (dead end) to the last section.
 
You can download the game "Forest Revenge" from the link: https://play.google.com/store/apps/details?id=com.TappaniaGames.ForestRevenge
And also support us on the portals:
https://www.youtube.com/channel/UCeyUPEpeGocpQ_MiM4pwl_A
https://www.instagram.com/tappaniagames/
https://www.tiktok.com/@tappania_games?lang=en
https://vk.com/forest_revenge

Contact with me:
tappaniagames@gmail.com
