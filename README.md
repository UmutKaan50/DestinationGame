# Top Dungeon Game notes
### Concepts:

Sprite renderer, sorting layer; Project settings, pyhsics 2d, queries start in colliders, 

We have to use late update for everything camerawise.

Selecting multiple tiles can be easiliy using ctrl + lmb

Make sure you implemented tutorial correctly in every aspect of it.

Class + F12 | ctrl + lmb class

GameManager will be matched the one in the instance, first GameManager that it finds. (?)

In coding there is always multiple solutions to go about(?)

The size of the weapon's boxcollider2d component is directly related to game's difficulty.

The hit with our weapon should be a tag of the object. Each object has to have different tag.

You must be full ears while watching/listening something. Otherwise you may miss a critical part of the topic.

The reason why in 32721 weapon_0's name is shown is that it's child of player object and player object satisfies the prerequisites I guess.

We can't have multi inheritence with C#

I had an viewing issue of "+1xp" text about the size of floating text. 

Push recovery speed variable has reverse effect of what I've thought. I should analyze it. 

Turn on the collider only when we are swinging the sword.

Animator window has tire mouse click for movement.

We don't want exit time, we want something extremely fast.

Final boss attack in reverse rotation, I should analyze it.

Button with chest sprite acts as how many times we've pressed it even the closed animation triggers. One less than this amount as I see.

I did changed sprite's name and fixed it in code as well. :D

I'm not sure if I've solved xp's continue issue in 60000.

60214's problem felt similar to me from another game I've played.

IndexOutOfBound error happened because I made Weapon class's damagePoint and pushForce arrays public and in the editor and this arrays was empty. 
Making them private solved the issue. :)

Event system disappears after enter and leave.(?)

Had an issue about health bar, it didn't decrease.

I remembered how coroutines applicationn was made even a little.

Don't forget that we can reach both parent and child object's compenents.

Skeleton's death sfx issue didn't solved yet.

1 second immunity(?)

How you positioned has exit time is important. Play whole animation when transition happens in this place...

I used to call two consecutive triggers at the same time.

I can change "+" and "-" buttons in assurance panel if I'll continue using it.

Changing resolution in game view effects scene view.

I've changed canvas called "HUD"s sort order to 0, "QuestCanvas"s sort order to 1 so that I can use panel named "click locking".
