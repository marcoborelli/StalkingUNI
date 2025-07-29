package CSVManager

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

type Studente struct {
	Matricola string
	Cognome   string
	Nome      string
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
