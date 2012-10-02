using UnityEngine;
using System.Collections;

public class Player
{
	public string pushingString;
	public ColorType pushingColor;
	
	public Player ()
	{
		initInputStatus ();
	}
	
	public void initInputStatus ()
	{
		pushingString = "";
		pushingColor = ColorType.nullColor;
	}
	
	public void pushedPanel (Panel p)
	{
		this.pushingString += p.char_;
		this.pushingColor = p.type;
	}
}
