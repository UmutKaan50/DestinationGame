# TopDungeonGame
Fixed resolution: 800x600
Sprite renderer, sorting layer;
Project settings, pyhsics 2d, queries start in colliders, 
We have to use late update for everything camerawise.
Selecting multiple tiles can be easiliy using ctrl + lmb
Make sure you implemented tutorial correctly in every aspect of it.
He says make bigger level.
Class + F12 | ctrl + lmb class
GameManager will be matched the one in the instance, first GameManager that it finds. (?)
In coding there is always multiple solutions to go about(?)
---
Oyuncunun elindeki silahın boxcollider2d bileşeninin boyutları oyunun zorluk seviyesi ile doğrudan ilişkili.
Silahımız ile isabet edeceğimiz yer objenin bir etiketi olması gerekli. Etiket her biri için ayrı olmalıdır.
Anlatılana iyi kulak ver. Dinlemediğim bir anda belki de en kritik cümleyi kaçırabilirim.
3:27:21 esnasında weapon_0 objesinin isminin görünmesinin nedeni playerin çocuğu olması ve kodda gereken şartları playerın sağlaması sanırım.
We can't have multi inheritence with C#
3:50:00'de kaldım.
Kayan metnin boyutundan kaynaklı olarak +1xp yazısını görüntüleyememe sorunuyla karşılaştım.
Push recovery speed değişkeni benim düşündüğümün tam tersi şekilde tesir ediyor, bu durumu çözümlemeliyim.
Turn on the collider only when we are swinging the sword.
Animator penceresinde hareket için tekerleğe bastırma seçeneği sunulmuş.
We don't want exit time, we want something extremely fast.
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