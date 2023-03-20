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
        var msg = update.getMessage();
        var user = msg.getFrom();
        var id = user.getId();

        if(msg.isCommand()){
            if(msg.getText().equals("/play")) {
                onPlay(id, msg);
            }
            return;
        }
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
                .build();    //Message content

        sm.setReplyMarkup(markupInline);
        try {
            execute(sm);                        //Actually sending the message
        } catch (TelegramApiException e) {
            System.out.println(what);
            throw new RuntimeException(e);      //Any error will be printed here
        }
    }
}
