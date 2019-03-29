using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigScreen : MonoBehaviour
{
    
    [SerializeField]
    private Sprite[] backgrounds;

    [SerializeField]
    private AudioClip[] music;

    [SerializeField]
    private TMP_Dropdown backgroundDropdown, audioDropdown;

    [SerializeField]
    private Image preview;

    [SerializeField]
    private AudioSource musicPlayer;

    [SerializeField]
    private Slider slider;

    public void Show() {
        this.transform.SetAsLastSibling();

        preview.sprite = backgrounds[PrefsHelper.currentUser.background];
        musicPlayer.clip = music[PrefsHelper.currentUser.music];
    }

    public void Hide() {
        PrefsHelper.currentUser.SaveConfigs();
    }

    public void SetMusic(int index) {
        if (index != PrefsHelper.currentUser.music) {
            musicPlayer.Stop();
            musicPlayer.clip = music[index];
            musicPlayer.Play();
            audioDropdown.value = index;
            PrefsHelper.currentUser.music = index;
        }
    }

    public void SetBackground(int index) {
        if (index != PrefsHelper.currentUser.background) {
            preview.sprite = backgrounds[index];
            backgroundDropdown.value = index;
            PrefsHelper.currentUser.background = index;
        }
    }

    public Sprite GetBackground(int index) {
        return backgrounds[index];
    }

    void Awake()
    {
        backgroundDropdown.onValueChanged.AddListener(SetBackground);

        audioDropdown.onValueChanged.AddListener(SetMusic);

        slider.onValueChanged.AddListener((float v) => musicPlayer.volume = v);
    }
}
