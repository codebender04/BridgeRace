using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFail : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasMainMenu>();
    }
    public void RetryButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.Open<CanvasGameplay>();
        GameManager.Instance.ChangeGameState(GameState.Playing);
        LevelManager.Instance.LoadLevel();
    }
}
