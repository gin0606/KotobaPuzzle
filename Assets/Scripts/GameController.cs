using UnityEngine;
using System.Collections;

public enum ColorType
{
	red,
	blue,
	green,
	nullColor,
}

public enum InputStatus
{
	feiled,
	incomplete,
	complete
}

public class GameController : MonoBehaviour
{
	public GUIStyle redGUIStyle;
	public GUIStyle blueGUIStyle;
	public GUIStyle greenGUIStyle;
	public GUIStyle nullGUIStyle;
	WordManager wordManager;
	Player player;
	EnemyManager enemyManager;
	Panel[,] panels;

	void Start ()
	{
		wordManager = new WordManager ();
		
		player = new Player ();
		enemyManager = new EnemyManager ();

		int buttonLenX = 3;
		int buttonLenY = 5;
		panels = new Panel[buttonLenX, buttonLenY];
		
		float buttonSize = 50;
		Rect buttonRect = new Rect (0, 200, buttonSize, buttonSize);
		for (int x = 0; x < buttonLenX; x++) {
			for (int y = 0; y < buttonLenY; y++) {
				buttonRect.x += buttonSize + 1;
				panels [x, y] = new Panel ();
				panels [x, y].position = buttonRect;
			}
			buttonRect.x = 0;
			buttonRect.y += buttonSize + 1;
		}
				
		AddWord (wordManager.GetWord (), ColorType.red);
		AddWord (wordManager.GetWord (), ColorType.blue);
		AddWord (wordManager.GetWord (), ColorType.green);
	}
	
	void OnGUI ()
	{
		foreach (Panel p in panels) {
			Rect rect = p.position;
			string str = p.char_;
			GUIStyle style = GetGUIStyleWithType (p.type);
			if (!p.isPushed && GUI.Button (rect, str, style)) {
				PushPanel (p);
			}
		}
		
		GUI.Label (new Rect (51, 149, 254, 50), player.pushingString, nullGUIStyle);
		
		GUI.Label (new Rect (0, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.red), redGUIStyle);
		GUI.Label (new Rect (150, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.blue), blueGUIStyle);
		GUI.Label (new Rect (300, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.green), greenGUIStyle);
	}
	
	void AddWord (string word, ColorType type)
	{
		enemyManager.SetEnemyWord (word, type);
		
		Panel[,] shufflesPanels = this.ShuffledPanels ();
		int wordLength = word.Length;
		int count = 0;

		foreach (Panel p in shufflesPanels) {
			if (p.char_ == null && count < wordLength) {
				string ch = word.Substring (count, 1);
				p.PutChar (ch, type);
				count ++;
			}
		}
	}
	
	void SucceedInput (ColorType type)
	{
		player.InitInputStatus ();
		ErasePanels (type);
		AddWord (wordManager.GetWord (), type);
	}
	
	void FailInput ()
	{
		RestorePanels ();
		player.InitInputStatus ();
	}
	
	void PushPanel (Panel p)
	{
		if (CanPanelPush (p)) {
			p.isPushed = true;

			player.PushedPanel (p);

			Enemy enemy = enemyManager.GetEnemy (p.type);
			InputStatus st = enemy.Equals (player.pushingString);
			switch (st) {
			case InputStatus.complete:
				SucceedInput (p.type);
				break;
			case InputStatus.incomplete:
				break;
			case InputStatus.feiled:
				FailInput ();
				break;
			default:
				break;
			}
		} else {
			FailInput ();
		}
	}
	
	bool CanPanelPush (Panel p)
	{
		return p.type != ColorType.nullColor
			&& (player.pushingColor == ColorType.nullColor
				|| player.pushingColor == p.type);
	}

	void ErasePanels (ColorType type)
	{
		foreach (Panel p in panels) {
			if (p.type == type) {
				p.InitPanel ();
			}
		}
	}
	
	void RestorePanels ()
	{
		foreach (Panel p in panels) {
			p.isPushed = false;
		}
	}
	
	GUIStyle GetGUIStyleWithType (ColorType type)
	{
		GUIStyle ret = null;
		switch (type) {
		case ColorType.blue:
			ret = blueGUIStyle;
			break;
		case ColorType.green:
			ret = greenGUIStyle;
			break;
		case ColorType.red:
			ret = redGUIStyle;
			break;
		case ColorType.nullColor:
			ret = nullGUIStyle;
			break;
		}
		return ret;
	}
		
	Panel[,] ShuffledPanels ()
	{
		Panel[,] dstPanels = panels;
		int len_x = dstPanels.GetLength (0);
		int len_y = dstPanels.GetLength (1);
		int pLength = len_x * len_y;

		for (int i = 0; i < pLength; i++) {
			int r = Random.Range (0, pLength);
			
			int[] srcIndex = OneDimensionToTwo (r);
			int[] dstIndex = OneDimensionToTwo (i);
			
			Panel temp = dstPanels [dstIndex [0], dstIndex [1]];
			dstPanels [dstIndex [0], dstIndex [1]] = dstPanels [srcIndex [0], srcIndex [1]];
			dstPanels [srcIndex [0], srcIndex [1]] = temp;
		}

		return dstPanels;
	}
	
	int TwoDimensionToOne (int x, int y)
	{
		int len_y = panels.GetLength (1);
		return (x * len_y + y);
	}

	int[] OneDimensionToTwo (int i)
	{
		int len_y = panels.GetLength (1);
		int x = i / len_y;
		int y = i % len_y;
		return new int[]{x, y};
	}
}
