using UnityEngine;
using System.Collections;

public class Panel
{
	public string char_;
	public bool isPushed;
	public ColorType type;
	public Rect position;
	
	public Panel ()
	{
		InitPanel ();		
	}
	
	public void InitPanel ()
	{
		this.char_ = null;
		this.isPushed = false;
		this.type = ColorType.nullColor;
	}
	
	public void PutChar (string ch, ColorType color)
	{
		this.char_ = ch;
		this.type = color;
	}
}
