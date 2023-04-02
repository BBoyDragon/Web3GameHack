package telegram;

public class UserInfo {
    private final String firstName;
    private final long id;
    private final String lastName;
    private final String userName;

    public UserInfo(long id, String firstName, String lastName, String userName) {
        this.firstName = firstName;
        this.id = id;
        this.lastName = lastName;
        this.userName = userName;
    }

    public String getDataCheckString() {
        String s = "";
        if (firstName != null) {
            s += String.format("first_name=%s\n", firstName);
        }
        s += String.format("id=%d", id);
        if (lastName != null) {
            s += String.format("\nlast_name=%s", lastName);
        }
        if (userName != null) {
            s += String.format("\nusername=%s", userName);
        }
        return s;
    }
    public String getPayloadString(String hash, String botKey) {
        return String.format("""
        {"id": %d,
        "username": "%s",
        "first_name": "%s",
        "last_name": "%s",
        "hash": "%s",
        "bot_key": "%s"}""",
         id, userName, firstName, lastName, hash, botKey);
    }
}
