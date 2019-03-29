using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    
    public string name { get; private set; }
    public string password { get; private set; }
    public bool newUser { get; private set; }
    public bool blocked { get; private set; }
    public int ticks { get; private set; }
    public int background, music;

    public override string ToString() {
        return name;
    }

    public User(string name, string password, bool blocked) {
        this.name = name;
        this.password = password;
        this.blocked = blocked;
    }

    public bool IsAdmin() {
        return this.name == PrefsHelper.ADMIN;
    }

    public void Tick() {
        // don't allow the admin to be blocked
        if (IsAdmin()) {
            return;
        }
        
        if (++ticks == 3) {
            blocked = true;
            PrefsHelper.SaveUser(this);
        }
    }

    public void LoadConfigs() {
        background = PrefsHelper.LoadPref("background");
        music = PrefsHelper.LoadPref("music");
    }

    public void SaveConfigs() {
        PrefsHelper.SavePref("background", background);
        PrefsHelper.SavePref("music", music);
    }

    public void ClearTicks() {
        ticks = 0;
        blocked = false;
        password = name;

        PrefsHelper.SetStale(this, false);
        PrefsHelper.SaveUser(this);
    }

    public enum PasswordState { SUCCESS, INCORRECT, INVALID }

    public PasswordState ChangePassword(string oldPassword, string newPassword) {
        if (password == oldPassword) {
            if (oldPassword == newPassword) {
                return PasswordState.INVALID;
            }
            password = newPassword;
            return PasswordState.SUCCESS;
        }
        return PasswordState.INCORRECT;
    }

}
