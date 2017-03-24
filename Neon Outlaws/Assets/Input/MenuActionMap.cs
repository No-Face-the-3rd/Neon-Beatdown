using UnityEngine;
using UnityEngine.InputNew;

// GENERATED FILE - DO NOT EDIT MANUALLY
public class MenuActionMap : ActionMapInput {
	public MenuActionMap (ActionMap actionMap) : base (actionMap) { }
	
	public AxisInputControl @horizontalNav { get { return (AxisInputControl)this[0]; } }
	public AxisInputControl @verticalNav { get { return (AxisInputControl)this[1]; } }
	public ButtonInputControl @accept { get { return (ButtonInputControl)this[2]; } }
	public ButtonInputControl @decline { get { return (ButtonInputControl)this[3]; } }
	public ButtonInputControl @resume { get { return (ButtonInputControl)this[4]; } }
}
