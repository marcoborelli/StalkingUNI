# StalkingUNIBackend

## API Pubbliche
L'applicativo fornisce due API:
1. `/getStudent`: fornisce matricola, nome e cognome di uno o più studenti, in base alla pertinenza del parametro passato
2. `/getVoti`: fornisce tutti gli esami, sostenuti e non, dallo studente, con le relative valutazioni

## Chiamata alle API e parametri
Le API vanno richiamate tramite `GET`.\
I parametri vengono passati direttamente in query (`/api?param1=val1&param2=val2`).

Le possibili variabili che si possono aggiungere agli endpoint sono:

| Endpoint      | nome | cognome | matricola |
|---------------|------|---------|-----------|
|`/getStudent`  | ✅   | ✅      | ✅        |
|`/getVoti`     | ❌   | ❌      | ✅        |

## Organizzazione file nel server
Il servizio, per funzionare, necessita della configurazione del file `.env`:
- `MATRICOLE_VOTI_FOLDER`: indica la cartella contenente tutti i risultati dei diversi esami. La struttura della cartella è riportata nella [homepage](https://github.com/marcoborelli/StalkingUNI) del progetto
- `MATRICOLE_NOME_FILE`: è il path al file di output del programma `StudentsMerge` (contente la tripletta `matricola;cognome;nome` di tutti gli studenti noti)
- `HTTP_SERVER_PORT`: la porta su cui il servizio deve essere in ascolto