

# StalkingUNI

## Organizzazione
Il progetto è formato da:
1. `FinalVoteExtractor`: gestisce l'associazione matricole-esami
2. `StudentsMerge`: gestisce l'associazione nomi-matricole
3. `StalkingUNIBackend`: implementato in Go
4. `StalkingUNIFrontend`: implementato in JavaScript e React

## Struttura
La cartella contenente le informazioni relative agli appelli **deve** essere organizzata con criterio. A ogni esame viene assegnata una sottodirectory, con il nome dell'esame stesso. Questa deve contenere due elementi:
1. Un file `.csv` rinominato come la cartella. Questo è il file restituito dal programma `FinalVoteExtractor`.
2. Una sottodirectory, nominata `SingoliAppelli`, contenente **soltanto** i file `.csv` relativi alle singole sessioni della materia. Il nome di questi file deve essere: `Materia_Data_TipoEsame` con
	- `Materia`: coincide con il nome della cartella stessa
	- `Data`: scritta nel **formato americano** (`aaaa-mm-gg`)
	- `TipoEsame`: può essere `P`, `C` o `R`, rispettivamente **P**arziale, **C**ompleto o **R**ecupero

Ulteriori file nella directory verranno ignorati dal server.\
Esempio di struttura:

    /root
    |_ Esame1
    |  |_ SingoliAppelli
    |  |  |_ Esame1_aaaa-mm-gg_P.csv
    |  |_ Esame1.csv
    |  |_ other stuff
    |_ Esame2
    |  |_ SingoliAppelli
    |  |  |_ Esame2_aaaa-mm-gg_C.csv
    |  |_ Esame2.csv
    |_ Esame3
    |  |_ SingoliAppelli
    |  |  |_ Esame3_aaaa-mm-gg_R.csv
    |  |_ Esame3.csv
    |  |_ ...
    |_ ...
