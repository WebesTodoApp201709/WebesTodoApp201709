﻿# Webes TODO alkalmazás készítése
- Markdown állomány fogalma [github markdown doksi](https://guides.github.com/features/mastering-markdown/)
- a webes fejlesztés fogalma
  - html állományok előállítása és eljuttatása a felhasználó (Internet) böngészőjébe, ami megjeleníti és a felhasználó számára választási lehetőségeket ad.
  - amikor a html állományt előállító gép és a böngészőt futtató gép nem ugyanaz, akkor kell valami, ami az egyikről segít a másik gépet elérni. Ez a http protokoll [wikipédia link](https://hu.wikipedia.org/wiki/HTTP)
    - Négy eszköz: kérés, tartalom, fejléc, végállapot
- MVC alkalmazásfejlesztés: Model-View-Controller
  - a fejlesztőkörnyezetünk három fontos szereplőt azonosít és különböztet meg.
  - vezérlő (Controller): Minden kérés hozzá érkezik, a feladata a kérés kiszolgálása, vagy a kiszolgáláshoz szükséges feladatok delegálása, majd a feladatok elvégzésével a végeredmény továbbítása a kérő felé. 
  - adatok (Model) adatok köre, létrehozása, kiszámolása, előállítása, transzformációja, a kész adatok szolgáltatása a másik két szereplőnek
  - megjelenítés (View) a feladata a kinézethez szükséges elemek meghatározása, létrehozása, előállítása, módosítása
- ASP.NET MVC: névkonvenció alapú, a könyvtárak nevei és az állományok nevei egy elpőre meghatározott konvenciót követnek, a működésük ebből következik.
- Egy megjelenítőoldal készítése
- követelmények meghatározása (specifikáció)
- A Model bevezetése
  - szükségünk van egy felsorolásra, amit a programunk valahol előállít (változó)
  - az adatokat "el kell juttatni" a nézetre
  - majd az adatokat a nézetünk megfelelő helyén meg kell jeleníteni.

## Alkalmazás futtatása
### IIS (Internet Information Server)
- ha élesben akarjuk az alkalmazásunkat futtatni, akkor a szervergépre telepíteni kell.
- a fejlesztő gépre a Visual Studio telepítése automatikusan telepít egy IIS Express nevű alkalmazást, ami az IIS fejlesztési célokra átalakított/lebutított egy felhasználós változata.

## Egy HTTP kérés kiszolgálása (adatok szolgáltatása a felhasználó felé)

```

                                              +-------------------------------------------------+
   +-------------------+                      |  IIS (TodoApp)                                  |
   | Böngésző          |                      |-------------------------------------------------|
   |-------------------|                      |                                                 |
   |                   |   HTTP GET           |  Alkalmazás címe:http://localhost:52409/        |
   |                   | +------------------> |                                                 |
   |                   |                      |  A kérés az alkalmazáson belül: /Home/Index     |
   |                   |   HTML állomány      |                                                 |
   |                   |<--------------------+|                                                 |
   |                   |                     ||   Az alkalmazáson belüli /Home jelenti az       |
   |                   |                     ||   Home vezérlőt (HomeController)                |
   |                   |                     ||                                                 |
   |                   |                     ||   A második Index jelenti a vezérlő megfelelő   |
   +-------------------+                     ||   függvényének a nevét                          |
                                             ||                                                 |
                                             ||   A munkavégző függvény megnevezése: Action     |
                                             ||                                                 |
                                             ||   Az Action a munka elvégzője, létrehozza az    |
                                             ||   adatokat, megkeresi a megfelelő nézetet (View)|
                                             ||   és az adatokat és a végrehajtást átadja ennek |
                                             ||   a nézetnek                                    |
                                             ||                                                 |
                                             ||   A nézet az adatok segítségével generálja a    |
                                             +|   HTML állományt, amit visszaküld a böngészőnek |
                                              |   A nézet állománya cshtml, ami egy keveréke    |
                                              |   a c# és a html nyelvnek, és a neve: RAZOR     |
                                              +-------------------------------------------------+ 
```

Ez a kódban a következő utat járja be:

```




         Kérés      +--------------------------------------+                   +------------------+
       +----------> |  Controller                          |                   |  View            |
                  + |--------------------------------------|  +----------+     |------------------|
                  | |                                      |  |Model     |     |                  |
    +-----------+ | |  +-------------------+               |  |----------|     |                  |
    |Útválasztás| +--->| Action            |               |  |          |     |                  |
    +-----------+   |  |-------------------|               |  +----------+     |                  |
                    |  |                   | +-------------------------------> |                  |
                    |  |                   |               |                   |                  |
                    |  +-------------------+               |                   |                  |
                    |                                      |                   |                  |
                    |                                      |                   |                  |
                    |                                      |                   |                  |
                    |                                      |                   |                  |
                    |                                      |                   |                  |
                    +--------------------------------------+                   +------------------+


```

## Adatok szállítása a felhasználótól az alkalmazásig

- Az adatok szállítására a felhasználótól az alkalmazásunkig a kérésben van lehetőségünk
- A vizsgálathoz létrehozzuk a világ legegyszerűbb űrlapját

![beviteli űrlap](kepernyo01.png)

A feladat pedig, hogy hozzunk létre egy beviteli mezőt és egy gombot, amivel egy feladatot fel tudunk küldeni a szerverre

## Querystring-gel
Ha egyszerűen egy html űrlapot készítünk, és a form tag-nek nem adunk meg paramétereket és az űrlapon vannak:
- input mezők
- és egy gomb

```
<form>
    <input type="text" name="Megnevezes" value="" />
    <input name="Elvegezve" />
    <button>Felvitel</button>
</form>
```

Akkor a gomb megnyomásakor a böngésző az url mögé un. QueryString paramétereket "ragaszt" és így küldi a következő kérését a szerver felé GET kéréssel.

### Problémák:
- ugyanarra az action-re megy a kérés, mint az előző, ezt meg tudjuk oldani a *form* tag **action** paraméterével:

```
  <form action="/Home/Add">
    ...
  </form>
```

- webes konvenció, hogy a GET típusú kérés nem változtat adatot a szerveren, me tudom oldani a *form* tag **method** paraméterével:

```
  <form action="/Home/Add" method="post">
    ...
  </form>
```

- Új Probléma: Post esetén az adatok NEM QueryString-ként mennek a szerver felé, hanem Form Data formában. Ezt megoldja az ASP.NET MVC: az Action-nél megadhatjuk a paramétert, amibe automatikusan bekerül a böngészőből érkező adat.

- a bevitt adatok nem őrződnek meg a szerveren két kérés között, (nincs un. perzisztens adatunk)


## Alkalmazás váz
- A listaoldalon a következő lehetőségek elérhetőek
  - új elem felvitele
  - meglévő elem módosítása
  - meglévő elem törlése
  - meglévő elem elvégzésének a jelzése

- Az egyes lehetőségek linkek segítségével elérhetőek
- Ha egy-egy linkra kattintunk, akkor az adott feladattal kapcsolatos képernyőre/oldalra kerülünk

### Adatbevitel az ASP.NET MVC-ben
- Az általános adatbeviteli megoldás ASP.NET MVC alkalmazásoknál: Két Action és a hozzájuk tartozó 1 db View
- Az adatbevitelhez szükséges képernyőt egy Action szolgáltatja, ami a GET kérésekre válaszol.
- Az adatbevitelt követő POST egy ugyanolyan nevű, de a POST-okra szakosodott Action-re küldi az adatokat.

### Adatok ellenőrzése (Validálás)
Követeljük meg, hogy a feladat megnevezése ne lehessen üres.

Ehhez az ASP.NET beépített szolgáltatásait használjuk.

- Az adatmodell-en annotációval tudjuk a feltételeket megadni [doksi](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-6)
- Az adatmodellben property-t kell az adatok tárolására használni
- így a modell köré tudjuk építeni a nézetünket **@Html.TextBoxFor(m=>m.Megnevezes)**, **@Html.LabelFor(m=>m.Megnevezes)**
- a modell-t át tudjuk venni a POST-ra váró Action paraméterlistájában
- a modell állapotát ellenőrizni tudjuk az Action-ben: **ModelState.IsValid**
- ha nem jók az adatok, akkor vissza tudjuk küldeni a felhasználónak egy **return View(model);** sorral
- a validációs hibaüzenetet ki tudjuk iratni: **@Html.ValidationSummary()**, **@Html.ValidationMessageFor(m=>m.Megnevezes)**

## Adatok perzisztens tárolása
Az egyes böngésző kérések között megőrzött adattárolást hívják perzisztens adatoknak.
Erre nagyágyúval fogunk lőni: SQL szerveren tároljuk majd az adatainkat

Ehhez telepíteni kell egy MS SQL szerver express-t:

```
choco install sql-server-express
```

vagy 

```
cinst sql-server-express
```

Ha pedig management felületet szeretnénk a gépünkre, akkor chocolatey-vel a következő parancsot kell kiadni:

```
choco install sql-server-management-studio
```

```
cinst sql-server-management-studio
```

# Házi feladat:
módosítás és törlés képernyők létrehozása

# Hibajelenségek
## elindítás után megáll azonnal a webalkalmazás, ha Ctrl+F5-tel indítom, akkor pedig azt írja ki, hogy nem lehet elindítani az IIS Express-t
Megoldás: a solution mappában (az .sln-t tartalmazó mappa) van egy rejtett **.vs** könyvtár. Ebben van egy **config** mappa, és ebben található egy **applicationhost.config** állomány. Ezt kell törölni, a visual studio-ból kilépni majd visszalépni.

# Adatbázis kezelés EntityFramework Code First segítségével
## Megközelítések
- Data First
- Model First 
- Code First

Ha nem adunk meg neki semmilyen egyéb paramétert, akkor a helyi gépen fogja létrehozni
- vagy a *default instance*-on, 
- vagy az sqlexpress nevű szerveren (.\sqlexpress)
- vagy localdb-ként hozza létre

TodoApp.Models.TodoAppContext néven, ami a Context osztályunk neve kiegészítve a névtér névvel.


# Házi feladat
A Feladat model-ben az **Elvégezve** mezőt alakítsuk át property-vé, és
- töröljük az adatbázist, majd indítsuk el a programot és nézzük meg, hogy milyen adatbázist generál
- a varázsló segítségével generáljunk Controllert View-kkal, és nézzük meg a létrejövő kódot.

## Adatbázis folyamatos módosítása
Code First Migrations

Ha Code First módon építem az adatbázist, akkor nem tudok már az adatbázisban saját módosításokat végezni.


## Nézetek általi generálás
- először lefut a _ViewStart.cshtml
- ő beállítja az alapértelmezett layout-ot: _Layout.cshtml
- akárhány layout-om lehet, a nézet elején beállíthatom vagy letilthatom
- megjelenítéskor 
  - először lefut a nézet
  - majd a layout (ha =null, akkor nem történik semmi)
- és ezt a két végeredményt összekombinálja az ASP.NET (@RenderBody() a layout állományban)

## Bootstrap
http://getbootstrap.com/