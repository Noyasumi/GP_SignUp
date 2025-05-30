using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class PlayerData
{
    public string email;
    public string password;
    public string createDate;
    public PlayerData(string email, string password, string createDate)
    {
        this.email = email;
        this.password = password;
        this.createDate = createDate;
    }
}

public class GameData : MonoBehaviour
{
    public string email;
    public string password;

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;

    public void Submit()
    {
        if(passwordInput.text == confirmPasswordInput.text)
        {
            email = emailInput.text;
            password = passwordInput.text;
            PlayerData playerData = new PlayerData(email, password, DateTime.Now.ToString());
            string jsonFile = JsonUtility.ToJson(playerData);
            Debug.Log(jsonFile);

            StartCoroutine(UploadData(jsonFile));
        }
        else
        {
            Debug.LogError("Passwords do not match!");
        }
    }

    public IEnumerator UploadData(string form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://binusgat.rf.gd/unity-api-test/api/auth/signup.php", form, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("JSON upload complete!");
            }
        }
    }
}
