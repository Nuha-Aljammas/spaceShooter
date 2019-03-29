using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ButtonDropDown {
    public Button button;
    public RectTransform buttonList;
    public IEnumerator animation;
}

public class MainScreen : MonoBehaviour
{

    [SerializeField]
    private LoginScreen loginScreen;

    [SerializeField]
    private UserScreen userScreen;

    [SerializeField]
    private HistoryDialog historyDialog;

    [SerializeField]
    private ConfigScreen configScreen;

    [SerializeField]
    private PasswordDialog passwordDialog;

    [SerializeField]
    private ButtonDropDown files, apple, rock, memory, space;

    [SerializeField]
    private Button logoutButton;

    [SerializeField]
    private Image background;

    private ButtonDropDown activeDropdown = null;
    
    void Awake() {
        Bind(files);
        Bind(apple);
        Bind(rock);
        Bind(memory);
        Bind(space);

        logoutButton.onClick.AddListener(loginScreen.Show);

        if(PrefsHelper.currentUser != null) {
            Show();
        }
    }

    public void Show() {
        transform.SetAsLastSibling();
        configScreen.SetMusic(PrefsHelper.currentUser.music);
        background.sprite = configScreen.GetBackground(PrefsHelper.currentUser.background);

        if (PrefsHelper.IsStale(PrefsHelper.currentUser)) {
            passwordDialog.Show(true);
        }
    }

    private IEnumerator ShowAnimation(RectTransform t) {
        float start = Time.time;
        float timer = .1f, dt;

        do {
            dt = (Time.time - start) / timer;
            t.anchoredPosition
                = Vector2.Lerp(
                    new Vector2(t.anchoredPosition.x, t.rect.height),
                    new Vector2(t.anchoredPosition.x, 0),
                    dt);

            yield return null;
        } while (!Mathf.Approximately(dt, 1.0f));
    }

    private IEnumerator HideAnimation(RectTransform t) {
        float start = Time.time;
        float timer = .1f, dt;
        
        do {
            dt = (Time.time - start) / timer;
            t.anchoredPosition
                = Vector2.Lerp(
                    new Vector2(t.anchoredPosition.x, 0),
                    new Vector2(t.anchoredPosition.x, t.rect.height),
                    dt);

            yield return null;
        } while (!Mathf.Approximately(dt, 1.0f));
    }

    public void Bind(ButtonDropDown b) {
        b.animation = HideAnimation(b.buttonList);
        StartCoroutine(b.animation);

        b.button.onClick.AddListener(() =>
        {
            if (activeDropdown != null) {
                StopCoroutine(activeDropdown.animation);
                activeDropdown.animation = HideAnimation(activeDropdown.buttonList);
                StartCoroutine(activeDropdown.animation);
            }

            activeDropdown = b;
            StopCoroutine(b.animation);

            b.animation = ShowAnimation(b.buttonList);
            StartCoroutine(b.animation);
        });
    }

    
    public void ExitGame() {
        PrefsHelper.EndHistorylog(((int)Time.time) - PrefsHelper.logTime);

        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }


    public void logOut()
    {
        PrefsHelper.EndHistorylog(((int)Time.time) - PrefsHelper.logTime);


    }

    public void LoadGame(string sceneName) {
        PrefsHelper.BeginHistory();
        SceneManager.LoadScene(sceneName);
    }

    public void BeginHistory(int game) {
        PrefsHelper.BeginHistory();
    }

    public void ShowHistory(int game) {
        historyDialog.Show(PrefsHelper.GetHistory((GameType)game));
    }

    
}
