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
		initPanel ();		
	}
	
	public void initPanel ()
	{
		this.char_ = null;
		this.isPushed = false;
		this.type = ColorType.nullColor;
	}
	
	public void putChar (string ch, ColorType color)
	{
		this.char_ = ch;
		this.type = color;
	}
}
