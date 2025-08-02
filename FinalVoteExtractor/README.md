# FinalVoteExtractor

Dato in input un file `.csv` contenente tutte le valutazioni di tutti gli appelli di un dato esame, il programma restituisce in output un documento dello stesso tipo di quello in input, contenente, in caso di multiple occorrenze di una matricola, su una sola riga, tutti i voti presi agli appelli sostenuti, dal meno al più recente.\
Per come è scritto il programma **non** è possibile fornire in input un file già elaborato in precedenza a cui si vuole aggiungere una nuova parte in append. Il file di output viene infatti sempre sovrascritto per intero.

## Struttura file `.csv` in input

Ogni file ha la seguente struttura:
`Matricola;gg-mm-aaaa_tipoEsame_voto`\
Con:
- `gg-mm-aaaa`: la data in cui l'esame è stato sostenuto
- `tipoEsame`:
    1. `P`: Parziale
    2. `R`: Recupero (del parziale)
    3. `C`: Completo
- `voto`: vedi sezione [modalità di voto in input](#modalità-di-voto-in-input)

Ogni file in input, presenta sempre, come prima riga, un esempio della struttura del documento. Per questo motivo il programma salta sempre la sua lettura.

## Struttura file `.csv` in output

Ogni documento presenta la medesima struttura di quello in input, con tre sole differenze:
- **Non** è più presente la riga di intestazione con la struttura del file
- Ogni matricola può eventualmente avere più voti. In questo caso essi saranno separati da una virgola (`,`). Ad esempio: `Matricola;gg-mm-aaaa_TipoEsame_voto1,gg-mm-aaaa_TipoEsame_voto2`


## Modalità di voto in input

Non tutti gli esami hanno lo stesso modo per rappresentare il voto finale (`-` indica che non è possibile prendere quella valutazione):

|                   |Lode   |Sufficienza                        |Insufficienza         |
|-------------------|-------|-----------------------------------|----------------------|
|**AlgebraLineare** |`'30L'`|`'<voto>'`                         |`'<voto>'`            |
|**Analisi1**       |-      |`'Esame superato con \<voto\> su 30' \|\| ''Esame scritto superato con \<voto\> su 30'`|`'Parte I insufficiente' \|\| 'Compito insufficiente' \|\| 'Assente' \|\| 'Ritirato'`|
|**Architettura**   |`'31' \|\| '32' \|\| '33'`|`'<voto>'`      |`'\'`                 |
|**Fondamenti**     |-      |`'<voto>'`                         |`'INS'`               |
|**Programmazione1**|-      |`'<voto>'`                         |`''`                  |
|**Programmazione2**|`'31'` |`'<voto>'`                         |`'Insuff' \|\| 'compitini non svolti'`|
|**Algoritmi**      |`'30 LODE'`|`'<voto>'`                     |`'RIT' \|\| 'INS' \|\| 'REC I' \|\| 'REC II'`|


## Modalità di voto in output

Ogni documento in output rispetta il medesimo standard per i voti:

| Lode   |Sufficienza                        |Insufficienza         |
|--------|-----------------------------------|----------------------|
|`'31'`  |`'<voto [18-30]>'`                 |`'0'`                 |