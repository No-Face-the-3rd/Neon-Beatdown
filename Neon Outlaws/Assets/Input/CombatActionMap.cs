using UnityEngine;
using UnityEngine.InputNew;

// GENERATED FILE - DO NOT EDIT MANUALLY
public class CombatActionMap : ActionMapInput {
	public CombatActionMap (ActionMap actionMap) : base (actionMap) { }
	
	public AxisInputControl @moveX { get { return (AxisInputControl)this[0]; } }
	public AxisInputControl @moveY { get { return (AxisInputControl)this[1]; } }
	public ButtonInputControl @pause { get { return (ButtonInputControl)this[2]; } }
	public ButtonInputControl @light_X { get { return (ButtonInputControl)this[3]; } }
	public ButtonInputControl @heavy_Y { get { return (ButtonInputControl)this[4]; } }
	public ButtonInputControl @special1_A { get { return (ButtonInputControl)this[5]; } }
	public ButtonInputControl @special2_B { get { return (ButtonInputControl)this[6]; } }
	public ButtonInputControl @special3_Rb { get { return (ButtonInputControl)this[7]; } }
	public ButtonInputControl @ultimate_Rt { get { return (ButtonInputControl)this[8]; } }
}
