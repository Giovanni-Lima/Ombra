# Ombra

App .NET MAUI per la gestione degli ombrelloni negli stabilimenti balneari.

## Stack

- .NET 10 (SDK 10.0.301)
- .NET MAUI, single project, target frameworks: `net10.0-android`, `net10.0-ios`, `net10.0-maccatalyst`, `net10.0-windows10.0.19041.0`
- Progetto alla radice: `Ombra.csproj` (namespace `Ombra`, app id `com.companyname.ombra`)

## Metodo di lavoro

Sviluppo agile a evolutive incrementali: procediamo per piccoli step buildabili, non per grandi blocchi monolitici.

## Identità di marca

Palette (nomi ufficiali, uso vincolato — non aggiungere colori non elencati qui senza motivo):

| Nome | Hex | Uso |
|---|---|---|
| Teal Profondo | `#0E3D3B` | Primario: header, icone, pulsanti principali |
| Notte Marina | `#0A2624` | Sfondo dark mode e splash screen |
| Terracotta Vela | `#D9552E` | Accento di brand: CTA, strisce del logo |
| Ocra Sole | `#E3A23C` | Accento secondario: badge "prenotato", perno del logo |
| Sabbia | `#F1E4CC` | Sfondo chiaro, superfici, card |
| Grigio Scoglio | `#6E7A76` | Testo secondario, bordi, stati disattivati |

Colori semantici per lo stato ombrellone (separati dai colori di brand): verde = libero, ocra = prenotato, rosso/terracotta scuro = occupato.

Logo: ombrellone visto dall'alto, 8 spicchi alternati (Terracotta Vela / Sabbia), perno centrale Ocra Sole, su fondo Notte Marina. Sorgenti vettoriali in `Resources/AppIcon/appicon.svg` (sfondo), `appiconfg.svg` (spicchi), `Resources/Splash/splash.svg` (splash screen, logo + wordmark "OMBRA" in Rockwell sotto il logo).

Font: Rockwell per display/wordmark, Trebuchet MS per il corpo dell'interfaccia, Consolas per numeri/codici tabulari (font di sistema, nessuna dipendenza esterna).

## Stato build

- `dotnet build -f net10.0-windows10.0.19041.0` → OK, 0 errori
- Android → testato su emulatore (avviato da Visual Studio) il 2026-07-16: funziona correttamente, splash screen incluso. In precedenza `dotnet build -f net10.0-android` da riga di comando falliva per disallineamento tra la feature band dell'SDK (10.0.301 → band 300) e i workload MAUI registrati (band 10.0.100); non ancora riverificato se il problema persiste per i build da CLI.

## Prossimi passi

Da definire insieme: target utenti (staff stabilimento vs clienti finali vs entrambi), backend/dati, piattaforme di rilascio prioritarie.
