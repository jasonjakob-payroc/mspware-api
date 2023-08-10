//npm install
//node ApiTest.js


const axios = require('axios');
const fs = require('fs');
const FormData = require('form-data');

class ApiTest {

  constructor() {
    // dev testing
    this.apiurl = 'http://localapi.mspware.com:3000';
    this.appid = 'yourAppIdHere';
    this.appkey = 'yourAppKeyHere';
    this.merchantApplicationNo = '95649';
    this.baseURL = `${this.apiurl}/${this.appid}/v2/`;

    this.httpClient = axios.create({
      baseURL: this.baseURL,
      headers: {
        'x-api-key': this.appkey,
        'x-app-id': this.appid
      }
    });
  }

  async getApplicationTypes() {
    try {
      const request = 'applications/types';
      console.log('request: GET ', this.baseURL + request);
      const response = await this.httpClient.get(request);
      console.log('response: ', response.data);
    } catch (error) {
      console.error('Error: ', error);
    }
  }

  async putSubmitApp() {
    try {
      const json = {
        documentlist: '180184, 180206',
        mpadocument: '180293',
        mpadocumentpage: '7',
        override: false
      };

      const request = `applications/${this.merchantApplicationNo}/submit`;
      console.log('request: PUT ', this.baseURL + request);
      const response = await this.httpClient.put(request, json);
      console.log('response: ', response.data);
    } catch (error) {
      console.error('Error: ', error);
    }
  }

  async postUploadDocument() {
    try {
      const filePath = `${process.cwd()}/../dummy.pdf`;
      const form = new FormData();

      form.append('file', fs.createReadStream(filePath));

      const request = `applications/${this.merchantApplicationNo}/documents`;
      console.log('request: POST ', this.baseURL + request);
      const response = await this.httpClient.post(request, form, {
        headers: form.getHeaders()
      });
      console.log('response:', response.data);
    } catch (error) {
      console.error('Error: ', error);
    }
  }

  async runTests() {
    await this.getApplicationTypes();
    await this.putSubmitApp();
    await this.postUploadDocument();
  }
  
}

// Example usage:
const apiTest = new ApiTest();
apiTest.runTests();