using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserScreen : MonoBehaviour
{
    
    [SerializeField]
    private MainScreen mainScreen;

    [SerializeField]
    private PasswordDialog passwordDialog;

    [SerializeField]
    private TextMeshProUGUI header;

    [SerializeField]
    private Button passwordButton, createUserButton, deleteUserButton, unblockUserButton, menuButton;
    
    void Awake() {
        passwordButton.onClick.AddListener(()=>passwordDialog.Show());
        menuButton.onClick.AddListener(()=>mainScreen.Show());
    }

    public void Show() {
        header.text = PrefsHelper.currentUser.name + "'s Account";
        createUserButton.gameObject.SetActive(PrefsHelper.currentUser.IsAdmin());
        deleteUserButton.gameObject.SetActive(PrefsHelper.currentUser.IsAdmin());
        unblockUserButton.gameObject.SetActive(PrefsHelper.currentUser.IsAdmin());
        transform.SetAsLastSibling();
    }
}
