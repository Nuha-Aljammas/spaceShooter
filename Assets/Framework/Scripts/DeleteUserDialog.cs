using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteUserDialog : MonoBehaviour
{
    
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button confirmButton, cancelButton;

    [SerializeField]
    private TMP_InputField username;

    [SerializeField]
    private TextMeshProUGUI description;

    private string defaultDescription;

    void Awake() {
        confirmButton.onClick.AddListener(Submit);
        cancelButton.onClick.AddListener(Continue);
        defaultDescription = description.text;
    }

    void Submit() {
        if (AttemptDelete()) {
            Continue();
        } else {
            description.text = "User does not exist";
            description.color = Color.red;
        }
    }

    bool AttemptDelete() {

        var users = PrefsHelper.GetUsers();
        foreach(var user in users) {
            if (username.text == user.name) {
                PrefsHelper.DeleteUser(user);
                return true;
            }
        }
        return false;
    }

    public void Show() {
        this.content.SetActive(false);
        this.gameObject.SetActive(true);
    }
    
    public void Continue() {
        username.text = "";
        description.text = defaultDescription;
        description.color = Color.white;
        this.content.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
