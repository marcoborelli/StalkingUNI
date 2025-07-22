# FinalVoteExtractor

Dato in input un file `.csv` contenente tutte le valutazioni di tutti gli appelli di un dato esame, il programma restituisce in output un documento dello stesso tipo di quello in input, contenente, in caso di multiple occorrenze di una matricola, solamente la valutazione più recente

## Struttura file `.csv`

Ogni file ha la seguente struttura:
`matricola;voto`

## Modalità di voto

Non tutti gli esami hanno lo stesso modo per rappresentare il voto finale (`-` indica che non è possibile prendere quella valutazione):

|                   |Lode   | Sufficienza                       |Insufficienza         |
|-------------------|-------|-----------------------------------|----------------------|
|**AlgebraLineare** |`'30L'`|`'<voto>'`                         |`'<voto>'`            |
|**Analisi1**       |-      |`'Esame superato con \<voto\> su 30' \|\| ''Esame scritto superato con \<voto\> su 30'`|`'Parte I insufficiente' \|\| 'Compito insufficiente' \|\| 'Assente' \|\| 'Ritirato'`|
|**Architettura**   |`'31' \|\| '32' \|\| '33'`|`'<voto>'`      |`'\'`                 |
|**Fondamenti**     |-      |`'<voto>'`                         |`'INS'`               |
|**Programmazione1**|-      |`'<voto>'`                         |`''`                  |
|**Programmazione2**|`'31'` |`'<voto>'`                         |`'Insuff' \|\| 'compitini non svolti'`|
|**Algoritmi**      |`'30 LODE'`|`'<voto>'`                     |`'RIT' \|\| 'INS' \|\| 'REC I' \|\| 'REC II'`|



