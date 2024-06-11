using UnityEngine;

public class SpawnItemObstacle : MonoBehaviour
{
    public int itemGap = 3;
    public int obstacleGap = 6;

    public int row = 29;
    public float rowGap = 2.5f;

    public int col = 3;
    public float colGap = 2;
    public Vector3 pos;

    public GameObject prefab;

    private int randNum;
    public int itemTypeNum = 11;

    public Transform spawnTransform;
    private Vector3 transPos;
    private Vector3 itemPos;
    private Vector3 obstaclePos;
    private Vector3 objPos;
    private GameObject obj;

    private bool isItemSpawn = false;
    public int itemRowDelay = 3;
    private int rowChecking = 0;

    public string[] tagArr = new string[10];

    public void Spawn()
    {
        itemPos = spawnTransform.position + new Vector3(0, 1, 0);
        obstaclePos = spawnTransform.position;

        //짝수행에선 코인만, 홀수행에선 코인, 장애물 둘다
        for (int i = 0; i < row; i++)
        {
            if (i % 2 == 0)
            {
                transPos = itemPos; //짝수에선 아이템만 
                
                if (isItemSpawn)
                {
                    rowChecking++;
                    if (rowChecking >= itemRowDelay)
                    {
                        isItemSpawn = false;
                        rowChecking = 0;
                    }
                }
            }

            for (int j = 0; j < col; j++)
            {
                if (i % 2 == 0)
                {
                    randNum = Random.Range(-3, 4);
                    switch (randNum)
                    {
                        case -3:
                            continue;
                        case -2:
                        case -1:
                            randNum = 0;
                            break;
                        default:
                            if (!isItemSpawn)
                            {
                                isItemSpawn = true;
                                break;
                            }
                            else 
                            {
                                randNum = 0;
                                break;
                            }
                    }
                }
                else
                {
                    randNum = Random.Range(-3, itemTypeNum);
                    switch (randNum)
                    {
                        case -3:
                            continue;
                        case -1:
                            transPos = obstaclePos;
                            randNum = 6;
                            break;
                        case 0:
                            transPos = itemPos;
                            break;
                        case -2:
                        case 2:
                            randNum = 7; //rack
                            transPos = obstaclePos;
                            break;
                        case 1:
                        case 3:
                            randNum = 8; //student_Desk
                            transPos = itemPos - new Vector3(0, 0.3f, 0);
                            break;
                        case 8: //student_Desk
                            transPos = itemPos - new Vector3(0, 0.3f, 0);
                            break;
                        default:
                            transPos = obstaclePos;
                            break;
                    }
                }

                if(MapManager.Instance.IsEmptyPool(tagArr[randNum])) continue;

                obj = MapManager.Instance.SpawnFromPool(tagArr[randNum]);
                objPos = transPos + new Vector3(j * colGap, 0, i * rowGap);

                obj.transform.position = objPos;
            }
        }
    }
}