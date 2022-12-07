using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Flyweight : MonoBehaviour
{
    [DllImport("LoadPlugin")]
    private static extern float LoadFromFile(int j, string fileName);

    [DllImport("LoadPlugin")]
    private static extern int GetLines(string fileName);

    List<Enemy> allEnemies;

    string fn;

    // Start is called before the first frame update
    void Start()
    {
        allEnemies = new List<Enemy>();

        fn = Application.dataPath + "/save.txt";

        //LoadEnemy();
    }

    void LoadEnemy()
    {
        int numLines = GetLines(fn);
        int maxItems = numLines / 4;
        int infoSet = 0;

        //using flyweight 
        Enemy newEnemy = new Enemy();
        float y = LoadFromFile(2, fn);

        for (int j = 0; j < 10000; j++)
        {
            for (int i = 0; i < maxItems; i++)
            {
                //using flyweight
                newEnemy.enemyID = (int)LoadFromFile(0 + infoSet, fn);
                newEnemy.enemyPosition.x = LoadFromFile(1 + infoSet, fn);
                newEnemy.enemyPosition.y = y;
                newEnemy.enemyPosition.z = LoadFromFile(3 + infoSet, fn);

                allEnemies.Add(newEnemy);
                infoSet += 4;
            }
            infoSet = 0;
        }

        Debug.Log(allEnemies.Count);
    }
}