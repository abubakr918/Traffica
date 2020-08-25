using UnityEngine;
using UnityEngine.UI;

public class ProfileScreenHandler : MonoBehaviour
{
    public Text usernameText;
    public Text levelNumberText;
    public Text emailText;

    public void SetProfileFromCache()
    {
        usernameText.text = PreferenceManager.Username.ToUpper();
        levelNumberText.text = PreferenceManager.LevelNumber.ToString();
        emailText.text = PreferenceManager.Email.ToString();
    }

    public void SetUserProfileFromFirebase(User user)
    {
        usernameText.text = user.username.ToUpper();
        levelNumberText.text = user.levelNumber.ToString();
        emailText.text = user.email.ToString();

        PreferenceManager.Username = user.username;
        PreferenceManager.Email = user.email;
        PreferenceManager.LevelNumber = user.levelNumber;
    }

    public void OnCloseButtonClick()
    {
        UIManager.Instance.DeActivateScreen(GameScreens.ProfileScreen);
        //AudioManager.Instance.PlayButtonSound();
    }

    public void OnLogoutButtonClick()
    {
        PreferenceManager.Username = null;
        PreferenceManager.LevelNumber = 1;
        PreferenceManager.Email = null;
        UIManager.Instance.DeActivateScreen(GameScreens.ProfileScreen);
        UIManager.Instance.ActivateScreen(GameScreens.LoginScreen);
        UIManager.Instance.UIScreensReferences[GameScreens.MainScreen].GetComponent<MainScreenHandler>().SetHUD();
        //AudioManager.Instance.PlayButtonSound();
    }
}
