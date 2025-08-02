package JSONManager

import (
	"encoding/json"
	"fmt"

	"github.com/marcoborelli/StalkingUNI/CSVManager"
)

func StructUserToJSON(input CSVManager.Studente) (res string) {
	bytes, err := json.Marshal(input)

	if err != nil {
		fmt.Println(err)
	}

	res = string(bytes[:])
	//fmt.Println(res)
	return
}

func MapEsamiToJSON(input map[string][]CSVManager.Voto) (res string) {
	bytes, err := json.Marshal(input)

	if err != nil {
		fmt.Println(err)
	}

	res = string(bytes[:])
	//fmt.Println(res)
	return
}
