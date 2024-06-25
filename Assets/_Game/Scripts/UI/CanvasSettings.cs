using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    [SerializeField] private GameObject[] buttons;
    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        if (canvas is CanvasMainMenu)
        {
            buttons[2].SetActive(true);
        }
        else if (canvas is CanvasGameplay)
        {
            buttons[2].SetActive(true);
            buttons[0].SetActive(true);
            buttons[1].SetActive(true);
        }
    }
    public void MainMenuButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Pausing);
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
    public void ContinueButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Playing);
        Close(0);
    }
    
}
