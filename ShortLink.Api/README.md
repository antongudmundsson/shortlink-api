# ShortLink API

En URL-förkortningstjänst byggd med ASP.NET Core och C#.

## Översikt

Detta projekt är utvecklat som lösning på en kodutmaning.

Tjänsten tar emot en lång URL, validerar att den är giltig, genererar en unik kortkod, lagrar kopplingen i minnet och omdirigerar användaren till original-URL:en när kortlänken anropas.

Fokus har legat på:

* Ren och lättläst kod
* Tydlig ansvarsfördelning
* Trådsäker hantering
* Testbarhet
* Enkelhet framför onödig komplexitet

---

## Funktionalitet

* Skapa kortlänkar från långa URL:er
* Validering av URL:er
* Generering av unika kortkoder
* Trådsäker lagring i minnet
* Omdirigering till original-URL
* Automatiserade tester
* En enkel webbvy för demonstration

---

## Arkitektur

Projektet är uppdelat i flera delar med tydliga ansvarsområden.

### Endpoints

Ansvarar för HTTP-anrop och HTTP-svar.

### Services

Innehåller affärslogiken:

* URL-validering
* Kortkodsgenerering
* Skapande av kortlänkar
* Uppslag av befintliga länkar

### Storage

Ansvarar för lagring av länkar i minnet.

### Utilities

Innehåller hjälpfunktioner, exempelvis generering av kortkoder.

### Models

Innehåller request-, response- och domänmodeller.

---

## Trådsäkerhet

Lagringen bygger på `ConcurrentDictionary`.

Eftersom flera användare kan anropa API:t samtidigt krävs en trådsäker datastruktur. `ConcurrentDictionary` möjliggör säkra läs- och skrivoperationer utan att egen låslogik behöver implementeras.

---

## Generering av kortkoder

Kortkoder genereras med en Base62-teckenuppsättning:

```text
abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789
```

Varje kortkod består av 6 tecken.

Det ger ett mycket stort antal möjliga kombinationer samtidigt som länkarna hålls korta och lättlästa.

---

## API-endpoints

### Skapa kortlänk

```http
POST /api/links
```

Request:

```json
{
  "url": "https://google.com"
}
```

Response:

```json
{
  "originalUrl": "https://google.com",
  "shortCode": "Ab12Cd",
  "shortUrl": "http://localhost:5253/Ab12Cd"
}
```

---

### Omdirigering

```http
GET /{shortCode}
```

Exempel:

```http
GET /Ab12Cd
```

Svar:

```http
302 Found
```

Användaren skickas vidare till den ursprungliga URL:en.

---

### Hälsokontroll

```http
GET /health
```

Response:

```json
{
  "name": "ShortLink API",
  "status": "Running"
}
```

---

## Köra projektet

Klona repot:

```bash
git clone <repository-url>
```

Starta applikationen:

```bash
dotnet run
```

Applikationen startar som standard på:

```text
http://localhost:5253
```

---

## Köra tester

Kör samtliga tester:

```bash
dotnet test
```

---

## Designbeslut

### Varför ConcurrentDictionary?

Kodutmaningen kräver trådsäker hantering.

`ConcurrentDictionary` ger inbyggt stöd för samtidiga läs- och skrivoperationer utan att egen synkronisering med `lock` behöver implementeras.

### Varför Dependency Injection?

Dependency Injection gör lösningen enklare att testa, underhålla och vidareutveckla.

Det gör det även möjligt att ersätta implementationer i tester utan att ändra affärslogiken.

### Varför lagring i minnet?

Kravspecifikationen anger att data endast behöver leva under applikationens livstid.

Därför används ingen databas i denna lösning.

---

## Möjliga förbättringar

Om tjänsten skulle vidareutvecklas för produktion skulle följande kunna läggas till:

* Databaslagring
* Utgångsdatum för länkar
* Statistik och klickspårning
* Anpassade kortkoder
* Rate limiting
* Autentisering och behörighetsstyrning

---

## Författare

Anton Gudmundsson
