import okhttp3.*;
import okhttp3.logging.HttpLoggingInterceptor;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class ApiTest {

    private final String apiurl = "http://localapi.mspware.com:3000";
    private final String appid = "yourAppIdHere";
    private final String appkey = "yourAppKeyHere";
    private final String merchantApplicationNo = "95649";
    
    private OkHttpClient httpClient;

    public ApiTest() {
        HttpLoggingInterceptor loggingInterceptor = new HttpLoggingInterceptor();
        loggingInterceptor.setLevel(HttpLoggingInterceptor.Level.BODY);

        httpClient = new OkHttpClient.Builder()
                .addInterceptor(chain -> {
                    Request originalRequest = chain.request();
                    Request.Builder requestBuilder = originalRequest.newBuilder()
                            .addHeader("x-api-key", appkey)
                            .addHeader("x-app-id", appid)
                            .method(originalRequest.method(), originalRequest.body());
                    Request request = requestBuilder.build();
                    return chain.proceed(request);
                })
                .addInterceptor(loggingInterceptor)
                .build();
    }

    public void getApplicationTypes() throws IOException {
        Request request = new Request.Builder()
                .url(apiurl + "/" + appid + "/v2/applications/types")
                .build();

        Response response = httpClient.newCall(request).execute();
        System.out.println("response: " + response.body().string());
    }

    public void postUploadDocument() throws IOException {
        Path filePath = Paths.get(System.getProperty("user.dir"), "dummy.pdf");
        byte[] fileBytes = Files.readAllBytes(filePath);

        RequestBody requestBody = new MultipartBody.Builder()
                .setType(MultipartBody.FORM)
                .addFormDataPart("file", filePath.getFileName().toString(),
                        RequestBody.create(fileBytes, MediaType.parse("multipart/form-data")))
                .build();

        Request request = new Request.Builder()
                .url(apiurl + "/" + appid + "/v2/applications/" + merchantApplicationNo + "/documents")
                .post(requestBody)
                .build();

        Response response = httpClient.newCall(request).execute();
        System.out.println("response: " + response.body().string());
    }

    public void putSubmitApp() throws IOException {
        String json = "{\"documentlist\": \"180184, 180206\",\"mpadocument\":\"180293\",\"mpadocumentpage\":\"7\",\"override\":false}";
        RequestBody body = RequestBody.create(json, MediaType.parse("application/json"));

        Request request = new Request.Builder()
                .url(apiurl + "/" + appid + "/v2/applications/" + merchantApplicationNo + "/submit")
                .put(body)
                .build();

        Response response = httpClient.newCall(request).execute();
        System.out.println("response: " + response.body().string());
    }

    public static void main(String[] args) {
        ApiTest apiTest = new ApiTest();
        apiTest.getApplicationTypes();
        apiTest.postUploadDocument();
        apiTest.putSubmitApp();
    }
}
