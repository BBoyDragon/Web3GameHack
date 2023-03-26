package telegram;

import org.json.*;
import java.io.InputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.util.Scanner;
import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

public class JWTRequester {
    private static final String url = "https://external.api.tonplay.io/x/auth/v2/login/tg";
    private static final String expectedKey = "token";
    public static String generateJWT(UserInfo userInfo, String botKey, String botToken) throws Exception {
        String dataCheckString = userInfo.getDataCheckString();
        String hash = calculateHMac(botToken, dataCheckString);
        String payload = userInfo.getPayloadString(hash, botKey);
        String response = send(url, payload);
        Object value = new JSONObject(response).get(expectedKey);
        if (!(value instanceof String)) {
            throw new RuntimeException("Unknown value for key '" + expectedKey + "': '" + value.getClass() + "'");
        }
        return (String) value;
    }

    private static final String ALGORITHM = "HmacSHA256";
    private static String calculateHMac(String key, String data) throws Exception {
        Mac sha256_HMAC = Mac.getInstance(ALGORITHM);
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(key.getBytes(StandardCharsets.UTF_8));
        SecretKeySpec secret_key = new SecretKeySpec(hash, ALGORITHM);
        sha256_HMAC.init(secret_key);

        return byteArrayToHex(sha256_HMAC.doFinal(data.getBytes(StandardCharsets.UTF_8)));
    }
    private static String byteArrayToHex(byte[] a) {
        StringBuilder sb = new StringBuilder(a.length * 2);
        for (byte b : a)
            sb.append(String.format("%02x", b));
        return sb.toString();
    }

    private static String send(String urlString, String payload) throws Exception {
        URL url = new URL(urlString);
        HttpURLConnection httpConn = (HttpURLConnection) url.openConnection();
        httpConn.setRequestMethod("POST");

        httpConn.setRequestProperty("X-Auth-Tonplay", "jrN8UPnrck:0L3l7yt4odamcsX2XNvP");
        httpConn.setRequestProperty("Content-Type", "application/json");

        httpConn.setDoOutput(true);
        OutputStreamWriter writer = new OutputStreamWriter(httpConn.getOutputStream());
        writer.write(payload);
        writer.flush();
        writer.close();
        httpConn.getOutputStream().close();

        InputStream responseStream = httpConn.getResponseCode() / 100 == 2
                ? httpConn.getInputStream()
                : httpConn.getErrorStream();
        Scanner s = new Scanner(responseStream).useDelimiter("\\A");
        String response = s.hasNext() ? s.next() : "";
        return response;
    }
}