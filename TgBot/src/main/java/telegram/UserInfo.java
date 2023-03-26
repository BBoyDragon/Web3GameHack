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
        return String.format("first_name=%s\nid=%d\nlast_name=%s\nusername=%s",
                firstName, id, lastName, userName);
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
