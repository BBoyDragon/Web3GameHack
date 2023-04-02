package telegram;

import org.telegram.telegrambots.bots.TelegramLongPollingBot;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.InlineKeyboardMarkup;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.InlineKeyboardButton;
import org.telegram.telegrambots.meta.api.objects.replykeyboard.buttons.KeyboardButton;
import org.telegram.telegrambots.meta.api.objects.webapp.WebAppInfo;
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
    public String getBotKey() {
        return dotenv.get("BOT_KEY");
    }
    public String getBotDomain() {
        return dotenv.get("BOT_DOMAIN");
    }

    private void addKeyboardStartButton(long id, String jwt) {
        List<List<InlineKeyboardButton>> rowsInline = new ArrayList<>();
        List<InlineKeyboardButton> rowInline = new ArrayList<>();
        WebAppInfo webApp = WebAppInfo.builder()
                .url(getBotDomain() + "?token=" + jwt).build();
        InlineKeyboardButton loginButton = InlineKeyboardButton.builder()
                .text("Login").webApp(webApp).build();
        rowInline.add(loginButton);
        rowsInline.add(rowInline);
        InlineKeyboardMarkup markupInline = InlineKeyboardMarkup.builder().keyboard(rowsInline).build();

        SendMessage sm = SendMessage.builder()
                .text("Start new game")
                .chatId(Long.toString(id))
                .replyMarkup(markupInline)
                .build();
        try {
            execute(sm);
        } catch (TelegramApiException e) {
            throw new RuntimeException(e);
        }
    }

    private void sendMessage(long chatId, String message) throws TelegramApiException {
        SendMessage messageToSend = SendMessage.builder()
                .text(message)
                .chatId(Long.toString(chatId))
                .build();
        execute(messageToSend);
    }

    @Override
    public void onUpdateReceived(Update update) {
        final var msg = update.getMessage();
        final var user = msg.getFrom();
        final long id = user.getId();
        if (!(msg.isCommand() && msg.getText().equals("/start"))) {
            try {
                sendMessage(id, "This is not a command.\nTry again /start");
            } catch (TelegramApiException e) {
                System.err.println(e.getMessage());
            }
            return;
        }
        final var userName = user.getUserName();
        final var firstName = user.getFirstName();
        final var lastName = user.getLastName();
        final UserInfo userInfo = new UserInfo(id, firstName, lastName, userName);
        final String botToken = getBotToken();
        final String botKey = getBotKey();
        try {
            String jwt = JWTRequester.generateJWT(userInfo, botKey, botToken);
            addKeyboardStartButton(id, jwt);
        }
        catch (Exception e){
            try {
                sendMessage(id, "\u26A0\uFE0F Something goes wrong...\n\uD83D\uDEE0 We are already fixing it!");
            } catch (TelegramApiException telegramE) {
                System.out.println("Error while sending a message: " + telegramE);
            }
        }
    }
}
