using UnityEngine;
using System.Collections;

public class WordManager
{
	string[] words = new string[]{"apple","baby","cafe","daddy","empty", "face","gold","head","int", "japan"};
	
	public string GetWord ()
	{
		int r = Random.Range (0, words.GetLength (0));
		return words [r];
	}
}
