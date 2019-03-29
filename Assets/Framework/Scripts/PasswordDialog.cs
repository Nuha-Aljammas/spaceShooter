using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordDialog : MonoBehaviour
{
    
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button submitButton, cancelButton;

    [SerializeField]
    private TMP_InputField oldPassword, newPassword;

    [SerializeField]
    private TextMeshProUGUI oldPasswordMessage, newPasswordMessage;
    private string oldPWDefaultText, newPWDefaultText;

    void Awake() {
        oldPWDefaultText = oldPasswordMessage.text;
        newPWDefaultText = newPasswordMessage.text;

        submitButton.onClick.AddListener(ChangePassword);
        cancelButton.onClick.AddListener(Hide);
    }

    void ChangePassword() {
        switch (PrefsHelper.currentUser.ChangePassword(oldPassword.text, newPassword.text)) {
        case User.PasswordState.SUCCESS:
            PrefsHelper.SaveUser(PrefsHelper.currentUser);
            PrefsHelper.SetStale(PrefsHelper.currentUser);
            Hide();
            // TODO: success dialog
            break;
        case User.PasswordState.INCORRECT:
            oldPasswordMessage.text = "Incorrect Password";
            oldPasswordMessage.color = Color.red;

            newPasswordMessage.text = newPWDefaultText;
            newPasswordMessage.color = Color.white;

            break;
        case User.PasswordState.INVALID:
            newPasswordMessage.text = "Invalid Password";
            newPasswordMessage.color = Color.red;

            oldPasswordMessage.text = oldPWDefaultText;
            oldPasswordMessage.color = Color.white;
            break;
        }
    }

    public void Show(bool lockUser = false) {
        this.gameObject.SetActive(true);
        this.content.SetActive(false);
        this.transform.SetAsLastSibling();

        cancelButton.gameObject.SetActive(!lockUser);
    }

    public void Hide() {
        this.content.SetActive(true);
        this.gameObject.SetActive(false);

        oldPasswordMessage.text = oldPWDefaultText;
        oldPasswordMessage.color = Color.white;

        newPasswordMessage.text = newPWDefaultText;
        newPasswordMessage.color = Color.white;

        newPassword.text = oldPassword.text = "";
    }
}
