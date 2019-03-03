using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	[SerializeField]
    public class Count
    {
        public int maximum;
        public int minimum;

       public Count(int min,int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 8);
    public Count foodCount = new Count(1, 5);

    public GameObject exit;
    public GameObject tiles;
    public GameObject walls;
    public GameObject enemies;
    public GameObject food;
    public GameObject outerWall;
    public GameObject player;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();


    void InitializeList()
    {
        gridPositions.Clear();
        for(int x=0;x<columns-1;x++)
        {
            for(int y=0;y<rows-1;y++)
            {
                gridPositions.Add(new Vector3(x, y, 0));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for(int x=-1;x<=columns;x++)
        {
            for(int y=-1;y<=rows;y++)
            {
                GameObject toInstantiate = tiles;

                if(x==-1||x==rows||y==-1||y==columns)
                {
                    toInstantiate = walls;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }

    }


    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        
        Vector3 randomPosition = gridPositions[randomIndex];

        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }


    void LayOutObjectAtRandomPositons(GameObject tiles,int min,int max)
    {
        int objectCount = Random.Range(min, max + 1);

        for(int i=0;i<objectCount;i++)
        {
            Vector3 randomPosition = RandomPosition();

            Instantiate(tiles, randomPosition, Quaternion.identity);
        }
    }

    public void setUpScene(int level)
    {
        BoardSetup();

        InitializeList();

        LayOutObjectAtRandomPositons(walls, wallCount.minimum, wallCount.maximum);

        LayOutObjectAtRandomPositons(food, foodCount.minimum, foodCount.maximum);

        int enemyCount = (int)Mathf.Log(level, 2f);

        LayOutObjectAtRandomPositons(enemies, enemyCount, enemyCount);

        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

}
