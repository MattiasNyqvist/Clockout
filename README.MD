# Work Time Calculator

En webbapp byggd med Blazor WebAssembly (.NET 10) som räknar ut när du kan gå hem – eller när du måste börja – utifrån arbetstid och lunch.

## Funktioner

- Två lägen: **Jag börjar kl** (räknar fram sluttid) och **Jag vill sluta kl** (räknar fram starttid)
- Val av tid med hjul för timme, minut och lunch, plus "Sätt till nu"
- Arbetstid som decimaltal (7,7) eller klockformat (7.42), valbart i inställningarna
- Normal arbetstid, lunch, tidssystem och veckostart sparas i webbläsaren (localStorage)
- Ljust och mörkt tema följer systemet

## Teknik

- Blazor WebAssembly (.NET 10), C#
- Ren CSS (design tokens i `wwwroot/css/app.css`), Bootstrap används bara för grundstil

## Komma igång

Kräver .NET 10 SDK.

```bash
cd src/WorkTimeCalculator.Wasm
dotnet watch
```

## Projektstruktur

```
src/WorkTimeCalculator.Wasm
├── Pages/         Sidor: Home (beräkning) och Settings (inställningar)
├── Components/    Återanvändbara delar, t.ex. WheelColumn (ett hjul)
├── Layout/        Sidskal med fast sidfot (länk, version, flikar)
├── Services/      AppSettings (sparade inställningar), TimeFormat (tolka/formatera tid), BuildInfo (version)
└── wwwroot/       CSS, ikoner och index.html
```

## Version och bygge

Sidfoten visar `Version 1.0 · Build 20261117.12`.

- **Version** sätts manuellt i `AppVersion` i `WorkTimeCalculator.Wasm.csproj`.
- **Build** är datum (UTC) och ett löpnummer. I GitHub Actions används körningens nummer (`github.run_number`), som räknas upp vid varje körning. Lokalt är löpnumret 0, men det går att sätta med `-p:BuildCounter=<nummer>`.

## Publicering

Push till `main` publicerar till GitHub Pages via `.github/workflows/deploy.yml`.
