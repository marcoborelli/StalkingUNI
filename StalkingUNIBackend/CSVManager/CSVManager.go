package CSVManager

import (
	"bufio"
	"fmt"
	"log"
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
		logError(err.Error())
		return
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
		logError(err.Error())
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
		logError(err.Error())
		return
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
		logError(err.Error())
	}

	return
}

func GetVoti(dirpath, matricola string) (voti map[string][]Voto) {
	data, err := (os.ReadDir(dirpath)) //la cartella letta contiene sottocartelle, ognuna per ogni materia
	if err != nil {
		fmt.Println(err)
		logError(err.Error())
		return
	}

	voti = make(map[string][]Voto)

	for _, elem := range data {
		//ogni sottocaretella contiene un file .csv con lo stesso nome della directory in cui e' contenuto
		nome_materia := elem.Name()
		file, err := os.Open(dirpath + nome_materia + string(os.PathSeparator) + nome_materia + ".csv")

		if err != nil {
			fmt.Println(err)
			logError(err.Error())
			return
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
					voti[nome_materia] = append(voti[nome_materia], tmp)
				}

				break
			}
		}

		if err := scanner.Err(); err != nil {
			fmt.Println(err)
			logError(err.Error())
		} else if _, ok := voti[nome_materia]; !ok {
			//se sono qui non esiste la chiave -> l'esame non e' stato sostenuto
			voti[nome_materia] = nil
		}
	}

	return
}

func parseStringTime(input string) (res time.Time) {
	fields := strings.Split(input, "-")

	anno, err := strconv.Atoi(fields[2])
	if err != nil {
		fmt.Println(err)
		logError(err.Error())
		return
	}

	tmp, err := strconv.Atoi(fields[1])
	if err != nil {
		fmt.Println(err)
		logError(err.Error())
		return
	}
	mese := time.Month(tmp)

	giorno, err := strconv.Atoi(fields[0])
	if err != nil {
		fmt.Println(err)
		logError(err.Error())
		return
	}

	//fmt.Printf("stringa:'%s'\ngiorno: %d, mese: %s, anno: %d\n\n", input, giorno, mese, anno)

	res = time.Date(anno, mese, giorno, 0, 0, 0, 0, time.UTC)
	return
}

func logError(message string) {
	log.Printf("Error: '%s'", message)
}
