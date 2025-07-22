# StudentsMerge

Dati in input due file `.csv`, il primo `Original` e il secondo `New`; il programma restituisce `Original` con, in append, le eventuali righe presenti in `New` ma non in `Original`.\
Per il suo impiego, si potrebbe fare un unico file `New`, contenente anche doppioni di righe e il programma funzionerebbe ancora. Nonostante questo, è consigliato procedere gradualmente, inserendo un file alla volta e richiamando il programma ogni volta. Questo per evitare consumi eccessivi di memoria da parte delle strutture dati impiegate.

## Struttura file `.csv`

Ogni file ha la seguente struttura:
`matricola;cognome;nome`

