using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LoginDialog : MonoBehaviour
{
    
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private TextMeshProUGUI message;

    private const string DEFAULT_MESSAGE = "Invalid login credentials";

    void Awake() {
        continueButton.onClick.AddListener(Continue);
    }

    public void Continue() {
        this.content.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void Show(string message = "") {
        this.gameObject.SetActive(true);
        this.content.SetActive(false);
        this.message.text = DEFAULT_MESSAGE + "\n" + message;
    }
}
