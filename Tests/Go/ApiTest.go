/*go run ApiTest.go*/

package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"log"
	"mime/multipart"
	"net/http"
	"os"
	"path/filepath"
)

type ApiTest struct {
	apiurl                string
	appid                 string
	appkey                string
	merchantApplicationNo string
	baseURL               string
}

func NewApiTest() *ApiTest {
	return &ApiTest{
		apiurl:                "http://localapi.mspware.com:3000",
		appid:                 "yourAppIdHere",
		appkey:                "yourAppKeyHere",
		merchantApplicationNo: "95649",
		baseURL:               "http://localapi.mspware.com:3000/jjakob/v2/",
	}
}

func (a *ApiTest) getApplicationTypes() {
	req, err := http.NewRequest("GET", a.baseURL+"applications/types", nil)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	req.Header.Set("x-api-key", a.appkey)
	req.Header.Set("x-app-id", a.appid)

	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)
	fmt.Println("response:", string(body))
}

func (a *ApiTest) putSubmitApp() {
	data := map[string]interface{}{
		"documentlist":    "180184, 180206",
		"mpadocument":     "180293",
		"mpadocumentpage": "7",
		"override":        false,
	}
	jsonData, err := json.Marshal(data)
	if err != nil {
		log.Println("Error:", err)
		return
	}

	req, err := http.NewRequest("PUT", a.baseURL+"applications/"+a.merchantApplicationNo+"/submit", bytes.NewBuffer(jsonData))
	if err != nil {
		log.Println("Error:", err)
		return
	}
	req.Header.Set("x-api-key", a.appkey)
	req.Header.Set("x-app-id", a.appid)
	req.Header.Set("Content-Type", "application/json")

	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)
	fmt.Println("response:", string(body))
}

func (a *ApiTest) postUploadDocument() {
	filePath := filepath.Join(os.Getenv("PWD"), "../dummy.pdf")
	file, err := os.Open(filePath)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	defer file.Close()

	body := &bytes.Buffer{}
	writer := multipart.NewWriter(body)
	part, err := writer.CreateFormFile("file", filepath.Base(filePath))
	if err != nil {
		log.Println("Error:", err)
		return
	}
	io.Copy(part, file)
	writer.Close()

	req, err := http.NewRequest("POST", a.baseURL+"applications/"+a.merchantApplicationNo+"/documents", body)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	req.Header.Set("Content-Type", writer.FormDataContentType())
	req.Header.Set("x-api-key", a.appkey)
	req.Header.Set("x-app-id", a.appid)

	resp, err := http.DefaultClient.Do(req)
	if err != nil {
		log.Println("Error:", err)
		return
	}
	defer resp.Body.Close()

	respBody, _ := io.ReadAll(resp.Body)
	fmt.Println("response:", string(respBody))
}

func (a *ApiTest) runTests() {
	a.getApplicationTypes()
	a.putSubmitApp()
	a.postUploadDocument()
}

func main() {
	apiTest := NewApiTest()
	apiTest.runTests()
}
