#pip install requests

import requests
import os

class ApiTest:
    def __init__(self):
        # dev testing
        self.apiurl = 'http://localapi.mspware.com:3000'
        self.appid = 'yourAppIdHere'
        self.appkey = 'yourAppKeyHere'
        self.merchantApplicationNo = '95649'
        self.baseURL = f'{self.apiurl}/{self.appid}/v2/'

        self.headers = {
            'x-api-key': self.appkey,
            'x-app-id': self.appid
        }

    def get_application_types(self):
        try:
            request = 'applications/types'
            print(f'request: GET {self.baseURL + request}')
            response = requests.get(self.baseURL + request, headers=self.headers)
            print('response:', response.json())
        except Exception as error:
            print('Error:', error)

    def put_submit_app(self):
        try:
            json_data = {
                'documentlist': '180184, 180206',
                'mpadocument': '180293',
                'mpadocumentpage': '7',
                'override': False
            }
            request = f'applications/{self.merchantApplicationNo}/submit'
            print(f'request: PUT {self.baseURL + request}')
            response = requests.put(self.baseURL + request, json=json_data, headers=self.headers)
            print('response:', response.json())
        except Exception as error:
            print('Error:', error)

    def post_upload_document(self):
        try:
            file_path = os.path.join(os.getcwd(), '../dummy.pdf')
            with open(file_path, 'rb') as f:
                files = {'file': f}
                request = f'applications/{self.merchantApplicationNo}/documents'
                print(f'request: POST {self.baseURL + request}')
                response = requests.post(self.baseURL + request, files=files, headers=self.headers)
                print('response:', response.json())
        except Exception as error:
            print('Error:', error)

    def run_tests(self):
        self.get_application_types()
        self.put_submit_app()
        self.post_upload_document()


# Example usage:
api_test = ApiTest()
api_test.run_tests()
