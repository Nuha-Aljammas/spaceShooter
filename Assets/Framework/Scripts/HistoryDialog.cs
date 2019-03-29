using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HistoryDialog : MonoBehaviour
{
    
    [SerializeField]
    private GameObject content;

    [SerializeField]
    private TextMeshProUGUI message;

    public void Show(List<string> history) {
        string message = "";
        foreach(var record in history) {
            var records = record.Split('|');
            message += records[3] + "\t: " + records[2] + " (" + records[0] + "," + records[1] + ")\n";
        }
        this.message.text = message;
        gameObject.SetActive(true);
        content.SetActive(false);
    }

    public void Show() {
        string message = "";
        if (!PrefsHelper.currentUser.IsAdmin()) {
            message = GetRec();

               
        } else {
            foreach(var User in PrefsHelper.GetUsers()) {
                message += GetRec(User);
                   
            }
        }

        this.message.text = message;
        gameObject.SetActive(true);
        content.SetActive(false);
    }

    private string GetRecords(GameType game, User user = null) {
        string message = "";
        var history = PrefsHelper.GetHistory(game, user);
        foreach(var record in history) {
            var records = record.Split('|');
            message += records[3] + ": " + records[2] + " in " + records[4] + " on " + records[0] + "," + records[1] + "\n";
        }
        return message;
    }

    public void Hide() {
        gameObject.SetActive(false);
        content.SetActive(true);
    }
    private string GetRec(User user = null)
    {
        string msg = "";
        var history = PrefsHelper.GetHistory(GameType.Files, user);

        foreach(var rec in history)
        {
            var records = rec.Split('|');
            msg += " Date:  " + records[0] + "..... Time Taken: " + records[3] + "...... By: " + records[2] + "\n";  
        }
        return msg;
    }

    
}
