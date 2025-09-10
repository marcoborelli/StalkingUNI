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
- `MATRICOLE_VOTI_FOLDER`: indica la cartella contenente tutti i risultati dei diversi esami. La cartella **deve** essere organizzata nel seguente modo:\
A ogni esame viene assegnata una sottodirectory, con il nome dell'esame stesso e che contenga un file `.csv` rinominato allo stesso modo della cartella. Questo è il file formattato dal programma `FinalVoteExtractor` e deve quindi rispettare la sua struttura.\
Ulteriori file nella directory (il file con tutti i voti non formattato, ad esempio) verranno ignorati dal server.\
Esempio di struttura:
    ```
    /root
    |_ Esame1
    |  |_ Esame1.csv
    |  |_ other stuff
    |_ Esame2
    |  |_ Esame2.csv
    |_ Esame3
    |  |_ Esame3.csv
    |  |_ EsameNonFormattato.csv
    |  |_ ...
    |_ ...```
- `MATRICOLE_NOME_FILE`: è il path al file di output del programma `StudentsMerge` (contente la tripletta `matricola;cognome;nome` di tutti gli studenti noti)
- `HTTP_SERVER_PORT`: la porta su cui il servizio deve essere in ascolto