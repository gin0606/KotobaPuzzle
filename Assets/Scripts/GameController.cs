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
				
		addWord (wordManager.getWord (), ColorType.red);
		addWord (wordManager.getWord (), ColorType.blue);
		addWord (wordManager.getWord (), ColorType.green);
	}
	
	void OnGUI ()
	{
		foreach (Panel p in panels) {
			Rect rect = p.position;
			string str = p.char_;
			GUIStyle style = getGUIStyleWithType (p.type);
			if (!p.isPushed && GUI.Button (rect, str, style)) {
				pushPanel (p);
			}
		}
		
		GUI.Label (new Rect (51, 149, 254, 50), player.pushingString, nullGUIStyle);
		
		GUI.Label (new Rect (0, 0, 150, 50), enemyManager.getEnemyWord (ColorType.red), redGUIStyle);
		GUI.Label (new Rect (150, 0, 150, 50), enemyManager.getEnemyWord (ColorType.blue), blueGUIStyle);
		GUI.Label (new Rect (300, 0, 150, 50), enemyManager.getEnemyWord (ColorType.green), greenGUIStyle);
	}
	
	void addWord (string word, ColorType type)
	{
		enemyManager.setEnemyWord (word, type);
		
		Panel[,] shufflesPanels = this.shuffledPanels ();
		int wordLength = word.Length;
		int count = 0;

		foreach (Panel p in shufflesPanels) {
			if (p.char_ == null && count < wordLength) {
				string ch = word.Substring (count, 1);
				p.putChar (ch, type);
				count ++;
			}
		}
	}
	
	void succeedInput (ColorType type)
	{
		player.initInputStatus ();
		erasePanels (type);
		addWord (wordManager.getWord (), type);
	}
	
	void failInput ()
	{
		restorePanels ();
		player.initInputStatus ();
	}
	
	void pushPanel (Panel p)
	{
		if (canPanelPush (p)) {
			p.isPushed = true;

			player.pushedPanel (p);

			Enemy enemy = enemyManager.getEnemy (p.type);
			InputStatus st = enemy.equals (player.pushingString);
			switch (st) {
			case InputStatus.complete:
				succeedInput (p.type);
				break;
			case InputStatus.incomplete:
				break;
			case InputStatus.feiled:
				failInput ();
				break;
			default:
				break;
			}
		} else {
			failInput ();
		}
	}
	
	bool canPanelPush (Panel p)
	{
		return p.type != ColorType.nullColor
			&& (player.pushingColor == ColorType.nullColor
				|| player.pushingColor == p.type);
	}

	void erasePanels (ColorType type)
	{
		foreach (Panel p in panels) {
			if (p.type == type) {
				p.initPanel ();
			}
		}
	}
	
	void restorePanels ()
	{
		foreach (Panel p in panels) {
			p.isPushed = false;
		}
	}
	
	GUIStyle getGUIStyleWithType (ColorType type)
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
		
	Panel[,] shuffledPanels ()
	{
		Panel[,] dstPanels = panels;
		int len_x = dstPanels.GetLength (0);
		int len_y = dstPanels.GetLength (1);
		int pLength = len_x * len_y;

		for (int i = 0; i < pLength; i++) {
			int r = Random.Range (0, pLength);
			
			int[] srcIndex = oneDimensionToTwo (r);
			int[] dstIndex = oneDimensionToTwo (i);
			
			Panel temp = dstPanels [dstIndex [0], dstIndex [1]];
			dstPanels [dstIndex [0], dstIndex [1]] = dstPanels [srcIndex [0], srcIndex [1]];
			dstPanels [srcIndex [0], srcIndex [1]] = temp;
		}

		return dstPanels;
	}
	
	int twoDimensionToOne (int x, int y)
	{
		int len_y = panels.GetLength (1);
		return (x * len_y + y);
	}

	int[] oneDimensionToTwo (int i)
	{
		int len_y = panels.GetLength (1);
		int x = i / len_y;
		int y = i % len_y;
		return new int[]{x, y};
	}
}
