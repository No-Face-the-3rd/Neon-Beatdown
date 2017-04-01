using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputNew;

public class DeviceMapper : MonoBehaviour {

    public class PlayerInfo
    {
        public PlayerHandle handle;
        public ButtonInputControl acceptControl;
        public ButtonInputControl declineControl;

        public PlayerInfo(PlayerHandle inHandle, ButtonAction acceptAction, ButtonAction declineAction)
        {
            this.handle = inHandle;
            acceptControl = (ButtonInputControl)inHandle.GetActions(acceptAction.action.actionMap)[acceptAction.action.actionIndex];
            declineControl = (ButtonInputControl)inHandle.GetActions(declineAction.action.actionMap)[declineAction.action.actionIndex];
        }
    }

    public static DeviceMapper mapper;


    public PlayerInput globalInput;

    private PlayerHandle globalHandle;
    public List<PlayerInfo> players = new List<PlayerInfo>();

    public ButtonAction acceptAction, declineAction;


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
            List<InputDevice> devices = globalHandle.GetActions(acceptAction.action.actionMap).GetCurrentlyUsedDevices();

            PlayerHandle handle = PlayerHandleManager.GetNewPlayerHandle();
            foreach(InputDevice device in devices)
            {
                handle.AssignDevice(device, true);
            }

            foreach(ActionMapSlot slot in globalInput.actionMaps)
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
            players.Add(new PlayerInfo(handle, acceptAction, declineAction));
        }

	}
}
