# SnakeClean

Refaktorovaná verze původní konzolové hry Snake v C#. Cíl refaktoringu byl: zachovat rozumnou velikost projektu, ale výrazně zlepšit čitelnost, oddělení odpovědností při zachování rozšiřitelnosti.

## Hlavní cíle refaktoringu

- odstranit nejasná a nekonzistentní jména
- oddělit doménovou logiku hry od konzolového GUI
- rozdělit velkou proceduru `Main` na malé, srozumitelné části
- nahradit „stringly typed“ směry silně typovaným enumem
- zapouzdřit stav hry do malých konzistentních tříd

## Struktura projektu

- `Core/` – čistá herní logika bez závislosti na `Console`
- `ConsoleUi/` – vykreslení a čtení vstupu z konzole
- `Application/` – orchestrace běhu aplikace
- `Program.cs` – pouze composition root

### 1. Rozpad monolitického `Main`
Původní `Main` dělal všechno najednou: inicializaci, správu stavu, vykreslování, čtení kláves, kolize i ukončení hry. To míchalo několik úrovní abstrakce v jedné metodě.

Refaktoring:
- `Program.cs` pouze skládá závislosti.
- `SnakeConsoleApplication` řídí životní cyklus aplikace.
- `SnakeGame` obsahuje pravidla hry.
- `ConsoleRenderer` řeší vykreslení.
- `ConsoleInputReader` řeší vstup.

### 2. Smysluplná jména
Původní názvy jako `xposlijf`, `yposlijf`, `tijd`, `tijd2`, `buttonpressed`, `hoofd`, `randomnummer` apod. snižovaly čitelnost.

Refaktoring:
- `pixel` → `Position`
- `hoofd` → `Head` jako součást `Snake`
- `xposlijf` + `yposlijf` → kolekce `Body`
- `movement` → `Direction`
- `berryx` + `berryy` → `Berry`
- `tijd` + `tijd2` → práce s timeoutem uvnitř `ReadDirection`
- `buttonpressed` → odstraněno, nahrazeno srozumitelnější logikou `directionChanged`

### 3. Nahrazení stringů enumem
Řetězce `"UP"`, `"DOWN"`, `"LEFT"`, `"RIGHT"` byly náchylné na překlepy a zbytečně komplikovaly práci s logikou směru.

Refaktoring:
- zavedl jsem `enum Direction`,
- přidána metoda `ChangeDirection`, která validuje zakázané otočení o 180°.

### 4. Oddělení dat, chování a reprezentace
Původní třída `pixel` byla ve skutečnosti spíš primitivní datový obal a zároveň byl zbytek stavu roztroušený po lokálních proměnných.

Refaktoring:
- `Position` je malý immutable value object.
- `Snake` drží hlavu, tělo a pravidla změny směru.
- `SnakeGame` drží stav jedné hry a rozhoduje o update cyklu.
- `GameState` slouží jako čtecí model pro renderer.

### 5. Decoupling logiky od GUI 
Tohle byl nejdůležitější zásah. V původním řešení byla herní logika přímo závislá na `Console`: kolize, pohyb, spawn berry i vykreslení běžely v jedné smyčce.

Refaktoring:
- `Core` projektová vrstva nezná `Console` vůbec,
- `SnakeGame` neřeší barvy, kurzor ani klávesnici,
- `ConsoleRenderer` pouze převádí `GameState` do konzolového výstupu,
- `ConsoleInputReader` pouze překládá klávesy na `Direction?`.

### 6. Menší a konzistentní třídy
Původní struktura měla v zásadě jen `Program` a vnořenou datovou třídu. 

Refaktoring:
- `Snake` řeší jen hada.
- `SnakeGame` řeší jen pravidla celé hry.
- `ConsoleRenderer` řeší jen render.
- `ConsoleInputReader` řeší jen input.
- `SnakeConsoleApplication` řeší jen běh aplikace.

### 7. Menší metody
Původní `Main` obsahoval dlouhé bloky pro kreslení hranic, těla, čtení vstupu a změnu polohy. Člověk musel pochopit mnoho řádků najednou.

Refaktoring:
- v rendereru jsou malé metody `DrawBorders`, `DrawBody`, `DrawBerry`, `DrawHead`, `DrawPixel`.
- v input části je izolovaná metoda `MapKeyToDirection`.
- v doméně jsou samostatné metody `HitsWall`, `GenerateBerryPosition`, `HitsItself`, `Move`, `ChangeDirection`.

Každá metoda dělá jen jednu věc a dá se přečíst velmi rychle.

### 8. Odstranění paralelních kolekcí
`xposlijf` a `yposlijf` tvořily klasický problém paralelních seznamů. Takové řešení je křehké, protože korektnost závisí na synchronním zacházení se dvěma kolekcemi.

Refaktoring:
- tělo hada je reprezentováno kolekcí `Position`.

### 9. Explicitní model konfigurace
Rozměry plochy, délka ticku a startovní délka byly v původním kódu rozeseté nepřehledně v `Main`.

Refaktoring:
- zavedena třída `GameSettings`.
- nastavení se předává explicitně do aplikace i herní logiky.

Změny parametrů hry jsou soustředěné na jedno místo.

### 10. Composition root místo ručního chaosu
Původní aplikace neměla jasné místo, kde by bylo vidět, z čeho se skládá.

Refaktoring:
- `Program.cs` je čistý composition root,
- vytváří settings, random, renderer, input reader, factory a aplikaci.

Závislosti jsou přehledné a případné rozšiřování je snadné.