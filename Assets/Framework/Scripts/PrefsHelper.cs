using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameType { Apple = 0, Rock = 1, Memory = 2, Space = 3, Files=4 };
public class PrefsHelper
{


    public const string ADMIN = "admin";
    private const string USER_NAME = "_username_";
    private const string PASSWORD = "_password_";
    private const string BLOCKED = "_blocked_";
    private const string NEW_USER = "_new_";
    private const string APPLE_HISTORY = "Apple Picker";
    private const string ROCK_HISTORY = "Rock Paper Scissors";
    private const string MEMORY_HISTORY = "Memory";
    private const string SPACE_HISTORY = "Space Shooter";
    private const string FilesHistory = " Files";
    public static GameType currentGame;

    private static readonly Dictionary<GameType, string> historyIds
        = new Dictionary<GameType, string>() {
            { GameType.Apple, APPLE_HISTORY },
            { GameType.Rock, ROCK_HISTORY },
            { GameType.Memory, MEMORY_HISTORY },
            { GameType.Space, SPACE_HISTORY },
            {GameType.Files, FilesHistory },
        };

    public static User currentUser = null;
    public static int logTime = 0;

    private static List<string> LoadList(string key) {
        var items = new List<string>(PlayerPrefs.GetString(key, "").Split(','));
        items.Remove("");
        return items;
    }

    private static void WriteList(string key, List<string> values) {
        string value = "";
        foreach(string v in values) {
            value += v + ",";
        }
        PlayerPrefs.SetString(key, value);
    }

    public static void InitAdmin() {
        var users = LoadList(USER_NAME);
        if (!users.Contains(ADMIN)) {
            users.Add(ADMIN);
            PlayerPrefs.SetString(ADMIN + PASSWORD, PlayerPrefs.GetString(ADMIN + PASSWORD, ADMIN));
            PlayerPrefs.SetInt(ADMIN + NEW_USER, 1);
            WriteList(USER_NAME, users);
        }
    }

    public static List<string> GetHistory(GameType GameType, User user = null) {
        if (user == null) {
            user = currentUser;
        }
        return LoadList(user + historyIds[GameType]);
    }

    private static void SaveHistory(GameType GameType, string history) {
        var histories = GetHistory(GameType);
        histories.Add(history);
        WriteList(currentUser + historyIds[GameType], histories);
    }

    public static List<User> GetUsers() {
        var users = new List<User>();
        var usernames = LoadList(USER_NAME);
        foreach(var name in usernames) {
            string password = PlayerPrefs.GetString(name + PASSWORD);
            bool blocked = PlayerPrefs.GetInt(name + BLOCKED, 0) != 0;
            bool newUser = PlayerPrefs.GetInt(name + NEW_USER, 0) != 0;
            users.Add(new User(name, password, blocked));
        }

        return users;
    }

    public static void SaveUser(User user) {
        PlayerPrefs.SetString(user.name + PASSWORD, user.password);
        PlayerPrefs.SetInt(user.name + BLOCKED, user.blocked ? 1 : 0);
    }

    public static void SetStale(User user, bool enabled = true) {
        PlayerPrefs.SetInt(user.name + NEW_USER, enabled ? 1 : 0);
    }

    public static bool IsStale(User user) {
        return PlayerPrefs.GetInt(user.name + NEW_USER, 0) == 0;
    }

    public static void SavePref(string id, int value) {
        PlayerPrefs.SetInt(currentUser.name + "pref" + id, value);
    }

    public static int LoadPref(string id) {
        return PlayerPrefs.GetInt(currentUser.name + "pref" + id, 0);
    }

    public static void CreateUser(User user) {
        var users = LoadList(USER_NAME);
        users.Add(user.name);
        WriteList(USER_NAME, users);
    }

    public static void DeleteUser(User user) {
        PlayerPrefs.DeleteKey(user.name + user.password);
        PlayerPrefs.DeleteKey(user.name + BLOCKED);
        PlayerPrefs.DeleteKey(user.name + "prefbackground");
        PlayerPrefs.DeleteKey(user.name + "prefmusic");
        PlayerPrefs.DeleteKey(user.name + NEW_USER);

        foreach(var value in historyIds.Values) {
            PlayerPrefs.DeleteKey(currentUser + value);
        }

        var users = LoadList(USER_NAME);
        users.Remove(user.name);
        WriteList(USER_NAME, users);
    }

    private static string history;
    public static void BeginHistory() {
        history = System.DateTime.Now.ToString("MMMM dd| yyyy") + "|" + currentUser.name + "|";
    }

    public static void EndHistory(int score, GameType gameType) {
        SaveHistory(gameType, history + score + "|" + historyIds[gameType]);
    }
    private static string loghistory;
    public static void BeginHistorylog()
    {
        loghistory = System.DateTime.Now.ToString("MMMM dd yyyy") + "|" + currentUser.name + "|";
    }

    public static void EndHistorylog(int time)
    {
        SaveHistory(GameType.Files, history + time + "|" + historyIds[GameType.Files]);
    }

}
