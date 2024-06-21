using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStage : MonoBehaviour
{
    [SerializeField] private List<WinStageCharacter> winStageCharacterList = new List<WinStageCharacter>();    
    private List<Character> characterWinLeaderboard = new List<Character>();
    private Character character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            character = other.GetComponent<Bot>();
            SetCharacterWin(character);

            if (characterWinLeaderboard.Count >= 3)
            {
                GameManager.Instance.ChangeGameState(GameState.Pausing);
                UIManager.Instance.CloseAll();
                UIManager.Instance.Open<CanvasFail>();
            }
        }
        if (other.CompareTag("Player"))
        {
            character = other.GetComponent<Player>();
            SetCharacterWin(character);

            GameManager.Instance.ChangeGameState(GameState.Pausing);
            UIManager.Instance.CloseAll();
            UIManager.Instance.Open<CanvasVictory>();
        }
    }
    private void SetCharacterWin(Character character)
    {
        characterWinLeaderboard.Add(character);
        winStageCharacterList[characterWinLeaderboard.Count - 1].Show(character.Color);
        character.Hide();
    }
}
