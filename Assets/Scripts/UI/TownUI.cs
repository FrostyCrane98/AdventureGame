using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject ShopPanel;
    public GameObject QuestPanel;

    public void GoToShop()
    {
        HideAllPanels();
        ShopPanel.SetActive(true);
    }

    public void GoToLobby()
    {
        HideAllPanels();
        LobbyPanel.SetActive(true);
    }

    public void GoToQuest()
    {
        HideAllPanels();
        QuestPanel.SetActive(true);
    }
    public void ReturnToDungeon()
    {
        UIController.Instance.ExitTown();
        DungeonController.Instance.EnterDungeon();
        GameController.Instance.GoToState(GameController.eGameState.PlayerTurn);
    }

    private void HideAllPanels()
    {
        LobbyPanel.SetActive(false);
        ShopPanel.SetActive(false);
        QuestPanel.SetActive(false);
    }

}
