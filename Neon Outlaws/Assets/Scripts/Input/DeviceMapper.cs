using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;

public class PlayerInfo
{
    public PlayerHandle handle;
    public ButtonInputControl acceptControl;
    public ButtonInputControl declineControl;
    public int playerNum;
    public GameObject controller;
    public bool canLeave;

    public PlayerInfo(PlayerHandle inHandle, ButtonAction acceptAction, ButtonAction declineAction, int playerNumIn, GameObject controllerIn)
    {
        this.handle = inHandle;
        acceptControl = (ButtonInputControl)inHandle.GetActions(acceptAction.action.actionMap)[acceptAction.action.actionIndex];
        declineControl = (ButtonInputControl)inHandle.GetActions(declineAction.action.actionMap)[declineAction.action.actionIndex];
        playerNum = playerNumIn;
        controller = controllerIn;
        canLeave = false;
    }
}

public class DeviceMapper : MonoBehaviour {


    public static DeviceMapper mapper;
    public int maxPlayers;


    public PlayerInput globalInput;

    private PlayerHandle globalHandle;
    public List<PlayerInfo> players = new List<PlayerInfo>();

    public ButtonAction acceptAction, declineAction;

    public int controllerIndex = -1;


	// Use this for initialization
	void Start () {

        if(mapper == null)
        {
            mapper = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(mapper != this)
        {
            Destroy(gameObject);
        }


        globalHandle = PlayerHandleManager.GetNewPlayerHandle();
        globalHandle.global = true;
        List<ActionMapSlot> actionMaps = globalInput.actionMaps;
        foreach(ActionMapSlot slot in actionMaps)
        {
            ActionMapInput input = ActionMapInput.Create(slot.actionMap);
            input.TryInitializeWithDevices(globalHandle.GetApplicableDevices());
            input.active = slot.active;
            input.blockSubsequent = slot.blockSubsequent;
            globalHandle.maps.Add(input);
        }

        acceptAction.Bind(globalHandle);
        declineAction.Bind(globalHandle);

	}
	
	// Update is called once per frame
	void Update () {
      
        if (acceptAction.control.wasJustPressed)
        {
            int num = -1;
            for(int i = 1;i <= maxPlayers;i++)
            {
                int ind = players.FindIndex(player => player.playerNum == i);
                if (ind >= 0)
                {
                    continue;
                }
                else
                {
                    num = i;
                    break;
                }
               
            }
            if (num >= 0)
            {

                List<InputDevice> devices = globalHandle.GetActions(acceptAction.action.actionMap).GetCurrentlyUsedDevices();

                PlayerHandle handle = PlayerHandleManager.GetNewPlayerHandle();
                foreach (InputDevice device in devices)
                {
                    handle.AssignDevice(device, true);
                }

                foreach (ActionMapSlot slot in globalInput.actionMaps)
                {
                    ActionMapInput map = ActionMapInput.Create(slot.actionMap);
                    map.TryInitializeWithDevices(handle.GetApplicableDevices());
                    map.blockSubsequent = slot.blockSubsequent;

                    if (map.actionMap == acceptAction.action.actionMap)
                        map.active = true;
                    else
                        map.active = slot.active;


                    handle.maps.Add(map);
                }

                GameObject controller = GameObject.Instantiate(ObjectDB.data.getPrefab(controllerIndex));
                players.Add(new PlayerInfo(handle, acceptAction, declineAction, num, controller));
                CombatInputListener cil = controller.GetComponent<CombatInputListener>();
                MenuInputListener mil = controller.GetComponent<MenuInputListener>();
                int index = players.Count - 1;
                cil.bindInput(index);
                mil.bindInput(index);                
            }
        }


        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].canLeave)
            {
                if (players[i].declineControl.wasJustReleased)
                {
                    players[i].handle.Destroy();
                    Destroy(players[i].controller);
                    players.Remove(players[i]);
                    continue;
                }

            }
        }
	}

    public PlayerInfo getPlayer(int playerNum)
    {
        int ind = players.FindIndex(player => player.playerNum == playerNum);
        if(ind >= 0)
        {
            return players[ind];
        }
        else
        {
            return null;
        }
    }

    public void setPlayerCanLeave(int playerNum, bool playerLeavable)
    {
        int ind = players.FindIndex(player => player.playerNum == playerNum);
        if(ind >= 0)
        {
            players[ind].canLeave = playerLeavable;
        }
        else
        {
            Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + 
                " Could not find player with player number: " + playerNum);
        }
    }
}
