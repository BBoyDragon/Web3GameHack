package telegram;

import org.telegram.telegrambots.bots.TelegramLongPollingBot;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.InlineKeyboardMarkup;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.InlineKeyboardButton;
import org.telegram.telegrambots.meta.exceptions.TelegramApiException;
import org.telegram.telegrambots.meta.api.methods.send.SendMessage;
import org.telegram.telegrambots.meta.api.objects.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

import io.github.cdimascio.dotenv.Dotenv;

import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import org.apache.commons.codec.binary.Base64;

public class Bot extends TelegramLongPollingBot {
    private final Dotenv dotenv;

    public Bot() {
        dotenv = Dotenv.load();
    }

    @Override
    public String getBotUsername() {
        return dotenv.get("BOT_USERNAME");
    }

    @Override
    public String getBotToken() {
        return dotenv.get("BOT_TOKEN");
    }

    @Override
    public void onUpdateReceived(Update update) {
        var user = update.getCallbackQuery().getFrom();
        var id = user.getId();
        var userName = user.getUserName();
        var firstName = user.getFirstName();
        var lastName = user.getLastName();
        var locale = user.getLanguageCode();
        try {
            String secret = getBotToken();;
            String message = id + '\n' + userName + '\n' + firstName + '\n' + lastName + '\n' + locale + '\n' + dotenv.get("BOT_KEY");

            Mac sha256_HMAC = Mac.getInstance("HmacSHA256");
            SecretKeySpec secret_key = new SecretKeySpec(secret.getBytes(), "HmacSHA256");
            sha256_HMAC.init(secret_key);

            String hash = Base64.encodeBase64String(sha256_HMAC.doFinal(message.getBytes()));
            System.out.println(hash);
        }
        catch (Exception e){
            System.out.println("Error");
        }
//        if(msg.isCommand()){
//            if(msg.getText().equals("/play")) {
//                onPlay(id, msg);
//            }
//            return;
//        }
    }

    private void onPlay(Long id, Message msg) {
        sendText(id, msg.getText());
    }

    public void sendText(Long who, String what){
        List<List<InlineKeyboardButton>> rowsInline = new ArrayList<>();
        List<InlineKeyboardButton> rowInline = new ArrayList<>();
        LoginUrl loginUrl = LoginUrl.builder()
                .url(Objects.requireNonNull(dotenv.get("BOT_DOMAIN"))).build();
        InlineKeyboardButton loginButton = InlineKeyboardButton.builder()
                .text("Login").loginUrl(loginUrl).build();
        rowInline.add(loginButton);
        rowsInline.add(rowInline);
        InlineKeyboardMarkup markupInline = InlineKeyboardMarkup.builder().keyboard(rowsInline).build();

        SendMessage sm = SendMessage.builder()
                .text(what)
                .chatId(who.toString()) //Who are we sending a message to
                .replyMarkup(markupInline)
                .build();    //Message content

        try {
            execute(sm);                        //Actually sending the message
        } catch (TelegramApiException e) {
            System.out.println(what);
            throw new RuntimeException(e);      //Any error will be printed here
        }
    }
}
