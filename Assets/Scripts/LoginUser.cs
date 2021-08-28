using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginUser : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button loginButton;
    public TMP_Text loginButtonText;

    public GameObject currentPlayerObject;

    public void Login()
    {
        loginButton.interactable = false;
        if (usernameInput.text.Length < 3)
        {
            ErrorOnLoginMessage("Check Username");
        }
        else if (passwordInput.text.Length < 3)
        {
            ErrorOnLoginMessage("Check Password");
        }
        else
        {
            StartCoroutine(SendLoginForm());
        }
    }

    public void ErrorOnLoginMessage(string message)
    {
        loginButton.GetComponent<Image>().color = Color.red;
        loginButtonText.text = message;
        loginButtonText.fontSize = 50;
    }
    
    public void ResetLoginButton()
    {
        Color g = new Color32(41, 113, 52, 168);
        loginButton.GetComponent<Image>().color = g;
        loginButtonText.text = "Login";
        loginButtonText.fontSize = 75;
        loginButton.interactable = true;
    }

    IEnumerator SendLoginForm()
    {
        WWWForm LoginInfo = new WWWForm();
        LoginInfo.AddField("apppassword", "tismhouse");
        LoginInfo.AddField("username", usernameInput.text);
        LoginInfo.AddField("password", passwordInput.text);
        UnityWebRequest loginRequest = UnityWebRequest.Post("http://localhost/cruds/loginuser.php", LoginInfo);
        yield return loginRequest.SendWebRequest();
        if (loginRequest.error == null)
        {
            Debug.Log("Form Sent");
        }
        else
        {
            Debug.Log(loginRequest.error);
            var currentPlayer = Instantiate(currentPlayerObject, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
