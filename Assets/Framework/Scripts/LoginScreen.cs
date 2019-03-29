using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginScreen : MonoBehaviour
{

    [SerializeField]
    private LoginDialog failDialog;

    [SerializeField]
    private MainScreen mainScreen;

    [SerializeField]
    private TMP_InputField usernameInput, passwordInput;

    [SerializeField]
    private Button continueButton, exitButton;

    private Dictionary<string, User> users;

    void Awake()
    {

        continueButton.onClick.AddListener(AttemptLogin);
        exitButton.onClick.AddListener(ExitGame);
        if (audioManager.instance != null)
        {
            Destroy(audioManager.instance.gameObject);
        }
        if(PrefsHelper.currentUser == null) {
            Show();
        }
    }

    void AttemptLogin()
    {
        if (users.ContainsKey(usernameInput.text))
        {
            var user = users[usernameInput.text];
            if (user.blocked) {
                failDialog.Show("<size=15>This user is blocked</size>");
                return;
            }

            if (user.password == passwordInput.text)
            {
                //forward user to next screen
                PrefsHelper.logTime = (int)Time.time;
                PrefsHelper.currentUser = user;
                user.LoadConfigs();
                mainScreen.Show();
                passwordInput.text = "";
                PrefsHelper.BeginHistorylog();
            }
            else
            {
                // flag user account for failed login
                user.Tick();

                // show the failed login dialog
                if (user.ticks > 0) {
                    string warning = user.blocked ? "User has been blocked" : "User has been flagged";
                    failDialog.Show("<size=15>" + warning + "</size>");
                } else {
                    failDialog.Show();
                }
            }
        }
        else
        {
            failDialog.Show();
        }
    }

    public void Show() {
        // TODO: move a lot of this stuff into a separate UserHelper
        PrefsHelper.InitAdmin();
        this.users = new Dictionary<string, User>();
        var users = PrefsHelper.GetUsers();
        foreach(var user in users) {
            this.users.Add(user.name, user);
        }
        transform.SetAsLastSibling();
    }

    public void ExitGame() {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
}
