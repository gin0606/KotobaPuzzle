using UnityEngine;
using System.Collections;

public class Enemy
{
	public string word{ get; set; }

	public ColorType type{ get; set; }

	public Enemy ()
	{
		this.word = string.Empty;
		this.type = ColorType.nullColor;
	}
	
	public Enemy (string word)
	{
		this.word = word;
		this.type = ColorType.nullColor;
	}
	
	public Enemy (ColorType type)
	{
		this.word = string.Empty;
		this.type = type;
	}
	
	public Enemy (string word, ColorType type)
	{
		this.word = word;
		this.type = type;
	}
	
	public InputStatus Equals (string str)
	{
		if (word.Equals (str)) {
			return InputStatus.complete;
		} else {
			if (CompareToMiddle (str) == str.Length) {
				return InputStatus.incomplete;
			} else {
				return InputStatus.feiled;
			}
		}
	}
	
	private int CompareToMiddle (string str)
	{
		if (str.Length > word.Length) {
			return -1;
		}
		
		int i;
		for (i = 0; i < str.Length && i < word.Length; i++) {
			if (str [i] != word [i]) {
				i--;
				break;
			}
		}
		return i;
	}
}
