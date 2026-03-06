# FinalVoteExtractor
Dato il percorso di una cartella materia strutturata come indicato [qui](https://github.com/marcoborelli/StalkingUNI)), il software produce un output in formato `.csv` che raggruppa per ogni matricola lo storico dei voti, disposti in ordine cronologico in un **unico** record.

## Struttura dei file `.csv` in input

Ogni file salvato nella cartella `SingoliAppelli` ha la seguente struttura:
`Matricola;Voto`\
Con:
- `Matricola`: il numero di matricola dell'esaminato
- `Voto`: si veda sezione [modalità di voto in input](#modalità-di-voto-in-input)

## Struttura file `.csv` in output
La struttura del'output prevede che ogni record sia `Matricola;Appelli`:
1. `Matricola`: l'identificatore dell'esaminato
2. `Appelli`: qui vengono scritti, dal meno al più recente, tutti gli appelli sostenuti, separati dal carattere `,`. Ogni appello presenta la struttura `Data_TipoEsame_Voto`:
    - `Data`: nel **formato americano** (`aaaa-mm-gg`)
    - `TipoEsame`: `P`, `C` o `R`, si veda l'[homepage](https://github.com/marcoborelli/StalkingUNI)
    - `Voto`: si veda sezione [modalità di voto in output](#modalità-di-voto-in-output)

Ad esempio: `Matricola;aaaa-mm-gg_TipoEsame1_voto1,aaaa-mm-gg_TipoEsame2_voto2`


## Modalità di voto in input

Non tutti gli esami hanno lo stesso modo per rappresentare il voto finale (`-` indica che non è possibile prendere quella valutazione):

|                   |Lode                   |Sufficienza                        |Insufficienza             |
|-------------------|-----------------------|-----------------------------------|--------------------------|
|**AlgebraLineare** |`'30L'`                |`'<voto>'`                         |`'<voto>'`                |
|**Analisi1**       |-                      |`'Esame superato con <voto> su 30' \|\| 'Esame scritto superato con <voto> su 30'`|`'Parte I insufficiente' \|\| 'Compito insufficiente' \|\| 'Assente' \|\| 'Ritirato'`|
|**Architettura**   |`'<voto [30-33]>'`     |`'<voto>'`                         |`'\'`                     |
|**Fondamenti**     |-                      |`'<voto>'`                         |`'INS'`                   |
|**Programmazione1**|-                      |`'<voto>'`                         |`''`                      |
|**Programmazione2**|`'31'`                 |`'<voto>'`                         |`'Insuff' \|\| 'compitini non svolti'`|
|**Algoritmi**      |`'30 LODE'`            |`'<voto>'`                         |`'RIT' \|\| 'INS' \|\| 'REC I' \|\| 'REC II'`|
|**MetodiAlgebtici**|`'<voto [30-36]>'`     |`'<voto>'`                         | `'#N/D'` \|\| `'<voto>'` |


## Modalità di voto in output

Ogni documento in output rispetta il medesimo standard per i voti:

| Lode   |Sufficienza                        |Insufficienza         |
|--------|-----------------------------------|----------------------|
|`'31'`  |`'<voto [18-30]>'`                 |`'0'`                 |