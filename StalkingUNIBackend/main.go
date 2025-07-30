package main

import (
	"fmt"

	"github.com/joho/godotenv"
	"github.com/marcoborelli/StalkingUNI/ServerHTTP"
)

func main() {
	fmt.Println("Helo")

	err := godotenv.Load()
	if err != nil {
		fmt.Println(err)
	}

	ServerHTTP.StartServer(":8080")
}
