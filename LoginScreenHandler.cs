using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenHandler : MonoBehaviour
{
    [Header("Login")]
    public GameObject LoginPanel;
    public InputField emailLoginInputField;
    public InputField passwordLoginInputField;

    [Header("Registration")]
    public GameObject RegistrationPanel;
    public InputField usernameRegistrationInputField;
    public InputField emailRegistrationInputField;
    public InputField passwordRegistrationInputField;
    public InputField confirmPasswordRegistrationInputField;

    [Header("Message")]
    public GameObject messagePanel;
    public Image messageIcon;
    public Sprite errorSprite;
    public Sprite successSprite;
    public Text messageText;

    private void OnEnable()
    {
        ResetLoginInputFields();
        LoginPanel.SetActive(true);
        RegistrationPanel.SetActive(false);
        HideMessage();
    }

    public void OnCloseButtonClick()
    {
        UIManager.Instance.DeActivateScreen(GameScreens.LoginScreen);
    }

    public void OnDisplayLoginPanelButtonClick()
    {
        ResetLoginInputFields();
        LoginPanel.SetActive(true);
        RegistrationPanel.SetActive(false);
        //AudioManager.Instance.PlayButtonSound();
    }

    public void OnDisplayRegistrationPanelButtonClick()
    {
        ResetSignupInputFields();
        LoginPanel.SetActive(false);
        RegistrationPanel.SetActive(true);
        //AudioManager.Instance.PlayButtonSound();
    }

    public void InputFieldValueChange()
    {
        HideMessage();
    }

    #region Login

    public void OnLoginButtonClick()
    {
        if (string.IsNullOrWhiteSpace(emailLoginInputField.text))
        {
            DisplayErrorMessage("Enter email address");
        }
        else if (string.IsNullOrWhiteSpace(passwordLoginInputField.text))
        {
            DisplayErrorMessage("Enter password");
        }
        else
        {
            UIManager.Instance.ActivateScreen(GameScreens.LoadingScreen);
            FirebaseManager.Instance.LoginUser(emailLoginInputField.text, passwordLoginInputField.text, OnLoginComplete);
        }
        //AudioManager.Instance.PlayButtonSound();
    }

    void OnLoginComplete(LoginResult _result)
    {
        if (_result == LoginResult.Successfull)
        {
            DisplaySuccessMessage("Successfully logged in");
            GetUserFromDatabase();
        }
        else if (_result == LoginResult.Error)
        {
            UIManager.Instance.DeActivateScreen(GameScreens.LoadingScreen);
            DisplayErrorMessage("Error");
        }
    }

    private void GetUserFromDatabase()
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        FirebaseManager.Instance.GetUser(auth.CurrentUser.UserId, OnGetMyProfileComplete);
    }

    public void OnGetMyProfileComplete(User user, ReadUserResult _result)
    {
        if (_result == ReadUserResult.Successfull)
        {
            UIManager.Instance.UIScreensReferences[GameScreens.ProfileScreen].GetComponent<ProfileScreenHandler>().SetUserProfileFromFirebase(user);
            UIManager.Instance.ActivateSpecificScreen(GameScreens.MainScreen);
        }
        else
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            FirebaseManager.Instance.GetUser(auth.CurrentUser.UserId, OnGetMyProfileComplete);
        }
    }

    public void ResetLoginInputFields()
    {
        emailLoginInputField.text = null;
        passwordLoginInputField.text = null;
    }

    #endregion





    #region Registration

    public void OnSignupButtonClick()
    {
        if (string.IsNullOrWhiteSpace(usernameRegistrationInputField.text))
        {
            DisplayErrorMessage("Enter username");
        }
        else if (usernameRegistrationInputField.text.Length < 3)
        {
            DisplayErrorMessage("Enter a username having at least 3 characters");
        }
        else if (usernameRegistrationInputField.text.Length > 8)
        {
            DisplayErrorMessage("Enter a username up-to 8 characters");
        }
        else if (string.IsNullOrWhiteSpace(emailRegistrationInputField.text))
        {
            DisplayErrorMessage("Enter email address");
        }
        else if (string.IsNullOrWhiteSpace(passwordRegistrationInputField.text))
        {
            DisplayErrorMessage("Enter password");
        }
        else if (string.IsNullOrWhiteSpace(confirmPasswordRegistrationInputField.text))
        {
            DisplayErrorMessage("Enter confirm password");
        }
        else if (passwordRegistrationInputField.text.Length < 8)
        {
            DisplayErrorMessage("Enter a password having at least 8 characters");
        }
        else if (passwordRegistrationInputField.text != confirmPasswordRegistrationInputField.text)
        {
            DisplayErrorMessage("Your password doesn't match");
        }
        else
        {
            UIManager.Instance.ActivateScreen(GameScreens.LoadingScreen);

            User user = new User()
            {
                username = usernameRegistrationInputField.text,
                levelNumber = PreferenceManager.LevelNumber,
                email = emailRegistrationInputField.text,
                password = passwordRegistrationInputField.text
            };

            FirebaseManager.Instance.RegisterUser(user, OnRegistrationComplete);
        }
        //AudioManager.Instance.PlayButtonSound();
    }

    void OnRegistrationComplete(RegisterResult _result)
    {
        if (_result == RegisterResult.Successfull)
        {
            DisplaySuccessMessage("Successfully registered");
            Invoke(nameof(GetUserFromDatabase), 2f);
        }
        else if (_result == RegisterResult.Error)
        {
            UIManager.Instance.DeActivateScreen(GameScreens.LoadingScreen);
            DisplayErrorMessage("Error");
        }
    }

    public void ResetSignupInputFields()
    {
        usernameRegistrationInputField.text = null;
        emailRegistrationInputField.text = null;
        passwordRegistrationInputField.text = null;
        confirmPasswordRegistrationInputField.text = null;
    }

    #endregion





    #region Message

    public void DisplayErrorMessage(string _message)
    {
        messagePanel.SetActive(true);
        messageIcon.GetComponent<Image>().sprite = errorSprite;
        messageText.text = _message;
        Invoke(nameof(HideMessage), 4f);
    }

    public void DisplaySuccessMessage(string _message)
    {
        messagePanel.SetActive(true);
        messageIcon.GetComponent<Image>().sprite = successSprite;
        messageText.text = _message;
        Invoke(nameof(HideMessage), 4f);
    }

    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }

    #endregion
}
