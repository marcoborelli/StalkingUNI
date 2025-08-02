package CSVManager

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
	"time"
)

type Studente struct {
	Matricola string
	Cognome   string
	Nome      string
}

type Voto struct {
	Data      time.Time
	TipoEsame string
	Voto      int
}

func GetNomeCognome(filepath, matricola string) (studente Studente) {
	studente.Matricola = matricola
	file, err := os.Open(filepath)

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()

		fields := strings.Split(line, ";")

		if fields[0] == matricola {
			studente.Cognome = fields[1]
			studente.Nome = fields[2]
			break
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println(err)
	}

	return
}

func GetMatricola(filepath, cognome, nome string) (studenti []Studente) {
	if cognome == "" && nome == "" {
		return
	}

	cognome = strings.ToUpper(cognome)
	nome = strings.ToUpper(nome)

	file, err := os.Open(filepath)

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()

		fields := strings.Split(line, ";")
		for i := 1; i < len(fields); i++ {
			fields[i] = strings.ToUpper(fields[i])
		}

		/*if cognome == fields[1] && nome == fields[2] ||
		cognome == fields[1] && nome == "" ||
		cognome == "" && nome == fields[2]*/

		if strings.Contains(fields[1], cognome) && strings.Contains(fields[2], nome) {
			studenti = append(studenti, Studente{Matricola: fields[0], Cognome: fields[1], Nome: fields[2]})
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println(err)
	}

	return
}

func GetVoti(dirpath, matricola string) (voti map[string][]Voto) {
	data, err := (os.ReadDir(dirpath)) //la cartella letta contiene sottocartelle, ognuna per ogni materia
	if err != nil {
		fmt.Println(err)
	}

	voti = make(map[string][]Voto)

	for _, elem := range data {
		//ogni sottocaretella contiene un file .csv con lo stesso nome della directory in cui e' contenuto
		file, err := os.Open(dirpath + elem.Name() + string(os.PathSeparator) + elem.Name() + ".csv")

		if err != nil {
			fmt.Println(err)
		}

		defer file.Close()

		scanner := bufio.NewScanner(file)

		for scanner.Scan() {
			line := scanner.Text()

			fields := strings.Split(line, ";")

			if fields[0] == matricola {
				votiPresi := strings.Split(fields[1], ",")

				for _, votoPreso := range votiPresi {
					fieldsVoto := strings.Split(votoPreso, "_")

					data := parseStringTime(fieldsVoto[0])
					tipoEsame := fieldsVoto[1]
					voto, _ := strconv.Atoi(fieldsVoto[2])

					tmp := Voto{Data: data, TipoEsame: tipoEsame, Voto: voto}
					voti[elem.Name()] = append(voti[elem.Name()], tmp)
				}

				break
			}
		}

		if err := scanner.Err(); err != nil {
			fmt.Println(err)
		} else if _, ok := voti[elem.Name()]; !ok {
			//se sono qui non esiste la chiave -> l'esame non e' stato sostenuto
			voti[elem.Name()] = nil
		}
	}

	return
}

func parseStringTime(input string) (res time.Time) { // TODO: controllo errori
	fields := strings.Split(input, "-")

	anno, _ := strconv.Atoi(fields[2])
	tmp, _ := strconv.Atoi(fields[1])
	mese := time.Month(tmp)
	giorno, _ := strconv.Atoi(fields[0])

	//fmt.Printf("stringa:'%s'\ngiorno: %d, mese: %s, anno: %d\n\n", input, giorno, mese, anno)

	res = time.Date(anno, mese, giorno, 0, 0, 0, 0, time.UTC)
	return
}
