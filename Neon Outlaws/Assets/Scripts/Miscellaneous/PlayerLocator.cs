using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;

public class PlayerLocator : MonoBehaviour {
    public static PlayerLocator locator;

    public List<PlayerInput> players;
    private List<CombatInputListener> playerCombatInputListeners;
    private List<MenuInputListener> playerMenuInputListeners;
        

	// Use this for initialization
	void Start () {
		if(locator == null)
        {
            locator = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(locator != this)
        {
            Destroy(this.gameObject);
        }
        players = new List<PlayerInput>();
        playerCombatInputListeners = new List<CombatInputListener>();
        playerMenuInputListeners = new List<MenuInputListener>();
	}
	
	// Update is called once per frame
	void Update () {
        findPlayers();
	}


    void findPlayers()
    {
        PlayerInput[] inputsExist = FindObjectsOfType<PlayerInput>();
        for (int i = 0; i < inputsExist.Length; i++)
        {
            CombatInputListener combatListener = inputsExist[i].gameObject.GetComponent
                <CombatInputListener>();
            if (combatListener != null)
            {
                int inputInd = players.FindIndex(playerInput =>
                    playerInput.handle == inputsExist[i].handle);
                int combatInd = playerCombatInputListeners.FindIndex(listener =>
                    listener.playerNum == combatListener.playerNum);
                if (!(inputInd >= 0))
                {
                    players.Add(inputsExist[i]);
                }

                if (!(combatInd >= 0))
                {
                    playerCombatInputListeners.Add(combatListener);
                }
            }
            MenuInputListener menuListener = inputsExist[i].gameObject.GetComponent
                <MenuInputListener>();
            if (menuListener != null)
            {
                int menuInd = playerMenuInputListeners.FindIndex(listener =>
                    listener.playerNum == menuListener.playerNum);
                if (!(menuInd >= 0))
                {
                    playerMenuInputListeners.Add(menuListener);
                }
            }
        }
    }

    public CombatInputListener getCombatListener(int playerNum)
    {
        int ind = playerCombatInputListeners.FindIndex(player => player.playerNum == playerNum);
        if(ind >= 0)
        {
            return playerCombatInputListeners[ind];
        }
        else
        {
            return null;
        }
    }

    public MenuInputListener getMenuListener(int playerNum)
    {
        int ind = playerMenuInputListeners.FindIndex(player => player.playerNum == playerNum);
        if(ind >= 0)
        {
            return playerMenuInputListeners[ind];
        }
        else
        {
            return null;
        }
    }

    public PlayerInput getPlayer(int playerNum)
    {
        PlayerInfo info = DeviceMapper.mapper.getPlayer(playerNum);
        if(info != null)
        {
            return getPlayer(info.handle);
        }
        else
        {
            return null;
        }
    }

    private PlayerInput getPlayer(PlayerHandle handle)
    {
        int ind = players.FindIndex(player => player.handle == handle);
        if(ind >= 0)
        {
            return players[ind];
        }
        else
        {
            return null;
        }
    }

    public int getNumPlayers()
    {
        return players.Count;
    }

    public int getNumCombatListeners()
    {
        return playerCombatInputListeners.Count;
    }

    public int getNumMenuListeners()
    {
        return playerMenuInputListeners.Count;
    }

    public void clearLists()
    {
        players.Clear();
        playerCombatInputListeners.Clear();
        playerMenuInputListeners.Clear();
    }
}
