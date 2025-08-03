package ServerHTTP

import (
	"fmt"
	"net/http"
	"os"

	"github.com/marcoborelli/StalkingUNI/CSVManager"
	"github.com/marcoborelli/StalkingUNI/JSONManager"
)

var matricole_nome_file, matricole_voti_folder, server_port string

const endpoint_student = "/getStudent"
const endpoint_voti = "/getVoti"

func StartServer() {
	matricole_nome_file = os.Getenv("MATRICOLE_NOME_FILE")
	matricole_voti_folder = os.Getenv("MATRICOLE_VOTI_FOLDER")
	server_port = os.Getenv("HTTP_SERVER_PORT")

	http.HandleFunc("/", handlerGeneric)
	http.HandleFunc(endpoint_student, handlerFindStudent)
	http.HandleFunc(endpoint_voti, handlerFindExamesVotes)

	http.ListenAndServe(server_port, nil)
}

func handlerFindStudent(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	query := r.URL.Query()

	matricola := query.Get("matricola")
	cognome := query.Get("cognome")
	nome := query.Get("nome")

	if query.Has("matricola") {
		ris := CSVManager.GetNomeCognome(matricole_nome_file, matricola)
		fmt.Fprint(w, JSONManager.StructUserToJSON(ris))
	} else {
		ris := CSVManager.GetMatricola(matricole_nome_file, cognome, nome)

		//Ci possono essere piu' studenti con lo stesso nome, quindi: creazione array JSON
		json_string_arr := "["
		for i, elem := range ris {
			json_string_arr += JSONManager.StructUserToJSON(elem)

			if i != len(ris)-1 {
				json_string_arr += ", "
			}
		}
		json_string_arr += "]"

		fmt.Fprint(w, json_string_arr)
	}
}

func handlerFindExamesVotes(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	matricola := r.URL.Query().Get("matricola")

	ris := CSVManager.GetVoti(matricole_voti_folder, matricola)
	fmt.Fprint(w, JSONManager.MapEsamiToJSON(ris))
}

func handlerGeneric(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Gli endpoint sono: '%s', '%s'.\nValori della query: matricola, nome, cognome", endpoint_student, endpoint_voti)
}
