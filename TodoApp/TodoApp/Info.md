# Webes TODO alkalmazás készítése
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

## Egy HTTP kérés kiszolgálása

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
                                             ||   A nézet az adatok segítvégével generálja a    |
                                             +|   HTML állományt, amit visszaküld a böngészőnek |
                                              |                                                 |
                                              |                                                 |
                                              +-------------------------------------------------+ 
```