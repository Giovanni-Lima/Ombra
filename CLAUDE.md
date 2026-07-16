# Ombra

App .NET MAUI per la gestione degli ombrelloni negli stabilimenti balneari.

## Stack

- .NET 10 (SDK 10.0.301)
- .NET MAUI, single project, target frameworks: `net10.0-android`, `net10.0-ios`, `net10.0-maccatalyst`, `net10.0-windows10.0.19041.0`
- Progetto alla radice: `Ombra.csproj` (namespace `Ombra`, app id `com.companyname.ombra`)
- `Ombra.Core` (`Ombra.Core/Ombra.Core.csproj`, target `net10.0`, nessuna dipendenza MAUI): class library con modelli di dominio (`Models/`, annotati con attributi `sqlite-net-pcl` per la persistenza) e interfacce dei repository (`Repositories/`: `IOmbrelloneRepository`, `IClienteRepository`, `IPrenotazioneRepository`). Separata da `Ombra.csproj` per poter scrivere test unitari senza dover referenziare i target framework specifici delle piattaforme.
- `Ombra.Infrastructure` (`Ombra.Infrastructure/Ombra.Infrastructure.csproj`, target `net10.0`): implementazione SQLite delle interfacce di `Ombra.Core` (`OmbraDatabase` + i tre repository), con seed di dati finti (griglia 5×4 ombrelloni) se il DB è vuoto. Nessuna dipendenza MAUI: il path del file `.db3` viene passato dall'esterno (da `MauiProgram.cs`, che usa `FileSystem.AppDataDirectory`). Sarà il progetto sostituito quando si migrerà al backend cloud.
- Sia `Ombra.Core` che `Ombra.Infrastructure` sono annidati dentro la cartella di `Ombra.csproj` ma esclusi esplicitamente dai suoi item glob di default tramite `<DefaultItemExcludes>` (altrimenti MAUI compila due volte gli stessi file/attributi).

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

- `dotnet build Ombra.csproj -f net10.0-windows10.0.19041.0` → OK, 0 errori. **Specificare sempre `Ombra.csproj`**: da quando esiste anche `Ombra.Core` nella `.slnx`, `dotnet build -f <TFM>` senza indicare il progetto risolve la soluzione e applica quel target framework a tutti i progetti in essa, facendo fallire `Ombra.Core` (che ha solo `net10.0`).
- Android → testato su emulatore (avviato da Visual Studio) il 2026-07-16: funziona correttamente, splash screen incluso. In precedenza `dotnet build -f net10.0-android` da riga di comando falliva per disallineamento tra la feature band dell'SDK (10.0.301 → band 300) e i workload MAUI registrati (band 10.0.100); non ancora riverificato se il problema persiste per i build da CLI.

### Vulnerabilità nota (accettata) — SQLitePCLRaw

Build genera warning `NU1903` su `SQLitePCLRaw.lib.e_sqlite3*` (CVE-2025-6965, gravità alta): nessuna versione del pacchetto NuGet la risolve ancora, nemmeno l'ultima disponibile (2.1.10). La CVE richiede query SQL con termini aggregati costruiti da input non fidato; i repository di Ombra usano solo query fisse via l'ORM di sqlite-net-pcl, senza SQL dinamico da input utente, quindi il rischio pratico è basso. Da riverificare quando SQLitePCLRaw rilascia una versione patchata.

## Decisioni di prodotto

- **Target utenti**: solo staff stabilimento (app gestionale interna — bagnini/gestori assegnano e monitorano gli ombrelloni). Nessuna vista cliente finale, per ora.
- **Backend/dati**: fase iniziale solo locale (SQLite on-device) dietro interfacce di dominio (es. `IUmbrelloneRepository`), registrate via DI in `MauiProgram.cs`. Migrazione futura a backend .NET cloud tramite una seconda implementazione delle stesse interfacce, senza toccare viste/viewmodel. Le interfacce vanno progettate fin da subito come se il backend fosse remoto (metodi async, gestione errori/latenza).
- **Piattaforma di rilascio prioritaria**: Android.
