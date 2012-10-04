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
		
	public Enemy GetEnemy (ColorType type)
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
	
	public void SetEnemyWord (string word, ColorType type)
	{
		Enemy enemy = this.GetEnemy (type);	
		enemy.word = word;
	}
	
	public string GetEnemyWord (ColorType type)
	{
		Enemy enemy = this.GetEnemy (type);	
		return enemy.word;
	}
}
