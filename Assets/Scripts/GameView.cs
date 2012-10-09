using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour
{
	public GUIStyle redGUIStyle;
	public GUIStyle blueGUIStyle;
	public GUIStyle greenGUIStyle;
	public GUIStyle nullGUIStyle;
	GameController gameController;
	
	void Start ()
	{
		gameController = new GameController ();
	}
	
	void OnGUI ()
	{
		Panel[,] panels = gameController.panels;
		foreach (Panel p in panels) {
			Rect rect = p.position;
			string str = p.char_;
			GUIStyle style = GetGUIStyleWithType (p.type);
			if (!p.isPushed && GUI.Button (rect, str, style)) {
				gameController.PushPanel (p);
			}
		}
			
		Player player = gameController.player;
		GUI.Label (new Rect (51, 149, 254, 50), player.pushingString, nullGUIStyle);

		EnemyManager enemyManager = gameController.enemyManager;
		GUI.Label (new Rect (0, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.red), redGUIStyle);
		GUI.Label (new Rect (150, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.blue), blueGUIStyle);
		GUI.Label (new Rect (300, 0, 150, 50), enemyManager.GetEnemyWord (ColorType.green), greenGUIStyle);
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
}
