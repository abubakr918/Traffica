using UnityEngine;

public class QuitGameScreenHandler : MonoBehaviour
{
    public void OnYesButtoncClick()
    {
        Application.Quit();
    }

    public void OnNoButtoncClick()
    {
        UIManager.Instance.DeActivateScreen(GameScreens.QuitGameScreen);
    }
}
