using UnityEngine;
using System.Collections;

public class Player
{
	public string pushingString;
	public ColorType pushingColor;
	
	public Player ()
	{
		InitInputStatus ();
	}
	
	public void InitInputStatus ()
	{
		pushingString = "";
		pushingColor = ColorType.nullColor;
	}
	
	public void PushedPanel (Panel p)
	{
		this.pushingString += p.char_;
		this.pushingColor = p.type;
	}
}
