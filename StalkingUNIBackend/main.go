package main

import (
	"fmt"
	"log"
	"os"

	"github.com/joho/godotenv"
	"github.com/marcoborelli/StalkingUNI/ServerHTTP"
)

func main() {
	fmt.Println("Helo")

	f, err := os.OpenFile("log", os.O_RDWR|os.O_CREATE|os.O_APPEND, 0666)
	if err != nil {
		log.Fatalf("Error opening file: %v", err)
	}
	defer f.Close()

	log.SetOutput(f)
	log.SetFlags(log.LstdFlags)

	err = godotenv.Load()
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}

	ServerHTTP.StartServer()
}
