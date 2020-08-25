using UnityEngine;
using UnityEngine.UI;

public class MainScreenHandler : MonoBehaviour
{
    [Header("HUD")]
    public GameObject profileButton;
    public GameObject loginButton;

    private void OnEnable()
    {
        SetHUD();
    }

    public void SetHUD()
    {
        if (string.IsNullOrEmpty(PreferenceManager.Username))
        {
            profileButton.SetActive(false);
            loginButton.SetActive(true);
        }
        else
        {
            UIManager.Instance.UIScreensReferences[GameScreens.ProfileScreen].GetComponent<ProfileScreenHandler>().SetProfileFromCache();
            profileButton.transform.GetChild(0).GetComponent<Text>().text = PreferenceManager.Username.ToUpper();
            profileButton.SetActive(true);
            loginButton.SetActive(false);
        }
    }

    public void OnLoginButtoncClick()
    {
        UIManager.Instance.ActivateScreen(GameScreens.LoginScreen);
    }

    public void OnProfileButtoncClick()
    {
        UIManager.Instance.ActivateScreen(GameScreens.ProfileScreen);
    }

    public void OnPlayButtoncClick()
    {
        UIManager.Instance.ActivateSpecificScreen(GameScreens.LevelsScreen);
    }

    public void OnRulesButtoncClick()
    {
        UIManager.Instance.ActivateScreen(GameScreens.RulesScreen);
    }

    public void OnSettingsButtoncClick()
    {
        UIManager.Instance.ActivateScreen(GameScreens.SettingsScreen);
    }

    public void OnLeaderBoardButtoncClick()
    {
        UIManager.Instance.ActivateSpecificScreen(GameScreens.LeaderBoardScreen);
    }

    public void OnExitButtoncClick()
    {
        UIManager.Instance.ActivateScreen(GameScreens.QuitGameScreen);
    }
}
