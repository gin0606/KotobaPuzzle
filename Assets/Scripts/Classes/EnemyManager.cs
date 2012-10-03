using UnityEngine;
using System.Collections;

public class EnemyManager
{
	Enemy redEnemy;
	Enemy blueEnemy;
	Enemy greenEnemy;
	Enemy nullEnemy;
	
	public EnemyManager ()
	{
		redEnemy = new Enemy ("red", ColorType.red);
		blueEnemy = new Enemy ("blue", ColorType.blue);
		greenEnemy = new Enemy ("green", ColorType.green);
		nullEnemy = new Enemy ();
	}
		
	public Enemy getEnemy (ColorType type)
	{
		Enemy ret = null;
		switch (type) {
		case ColorType.blue:
			ret = blueEnemy;
			break;
		case ColorType.green:
			ret = greenEnemy;
			break;
		case ColorType.red:
			ret = redEnemy;
			break;
		case ColorType.nullColor:
			ret = nullEnemy;
			break;
		}
		return ret;
	}
	
	public void setEnemyWord (string word, ColorType type)
	{
		Enemy enemy = this.getEnemy (type);	
		enemy.word = word;
	}
	
	public string getEnemyWord (ColorType type)
	{
		Enemy enemy = this.getEnemy (type);	
		return enemy.word;
	}
}
