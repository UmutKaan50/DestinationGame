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
<<<<<<< HEAD
Canavarın saldırısında bir terslik var, ilgilenmeliyim.
Sandık resimli butona bastığımız miktar kadar envanter görevi gören ekran kapansa bile bu miktardan bir eksik defa tekrardan açılıyor. 
Gerekirse bu durumu halledebilirim.
Sprite ismini kodda değiştirip tekrar kodda düzeltmem :D
Xp'nin kaldığı yerden devam etmesi meselesini 6:00:00'dan hemen öncesini çözdüm mü çözemedim mi emin değilim.
6:02:14 esnasında meydana gelen hata durumu bana oynadığım bir oyundan tanıdık geldi.
IndexOutOfBounds hatası Weapon classındaki damagePoint ve pushForce dizilerini public yapmam ve editörde bir şekilde bu dizilerin içlerinin boş olmasından
kaynaklıymış. Private'a dönüştürmem sorunu ortadan kaldırdı. :)
Event system git gel sonrasında kayboluyor.
Health bar azalmıyor.
Coroutinelerin uygulanışını bir nebze dahi olsa hatırladım.
Hem parent hem de child objenin içindeki componentlara erişimimiz mümkün.
İskeletin ölme sesi işi çözülemedi henüz.
1 sn hasar almama durumu söz konusu.
Animasyonda has exit time'ı nereye konumlandırdığın önemli. Şu durumdan şu duruma geçerken animasyonun tamamını oynat manasına geliyor sanırım.
Uyguladığım durumda alt alta iki trigger çağırmıştım. 
Assurance panelindeki "+" ve "-" butonlarını değiştirebilirim.
Game view'da ekran çözünürlüğünü değiştirdiğimizde scene view'da da değişiklik oluyor.
HUD ismini verdiğim canvasın sort orderını 0, QuestCanvas ismini verdiğim canvasın sort orderının 1 yaptım ki click locking ismini verdiğim paneli kullanabileyim.
=======

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



>>>>>>> a7bd08a6641f2cb115d7cbb6ccd6ddd7089f4b03
