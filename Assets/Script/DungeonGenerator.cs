using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class DungeonGenerator : MonoBehaviour
    {
        public class Cell // tao class tung cell trong maze voi 2 tt laf co the visit hay khong va status cua tuong va cua
        {
            public bool visited = false;
            public bool[] status = new bool[4];
            public bool TargetRoom = false;
        }
        public static DungeonGenerator DungeonSet;
        public GameObject[] Enemy;
        public Vector2 size;
        public int statusPos = 0;
        public GameObject room;
        public GameObject Target;
        public GameObject BigRoom;
        private Vector2 offset;
        public Vector2 roomOffset;
        public Vector2 bigRoomOffset;
        private int currenTargerRoom;
        public GameObject roomTarget;
        public Transform mapPosition;
        public GameObject newRoom;
        //public GameObject keyOpen;
        public List<GameObject> child;
        public List<GameObject> enemy;
        

        //List<int> numberOfCellInMaze;
        
        //public Vector3 teleMapPosition;
        List<Cell> board;
        private void Awake()
        {
            DungeonSet = this;
            child = new List<GameObject>();
            enemy = new List<GameObject>();
            
        }

        // Start is called before the first frame update

        [System.Obsolete]
        void Start()
        {
            MazeGenerator();
            
        }

        [System.Obsolete]
        void GenerarDungeon()
        {
            
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    offset = roomOffset;

                    Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                    if (currentCell.visited)
                    {

                        if (currentCell.TargetRoom == true)
                        {
                            Debug.Log("phong thuong la phong so " + i + " " + j + " va la phong " + Mathf.FloorToInt(i + j * size.x));
                            newRoom = Instantiate(roomTarget, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform);
                            newRoom.GetComponent<RoomBehaviour>().UpdateRoom(currentCell.status);  
                            newRoom.name += " " + i + " - " + j;
                            child.Add(newRoom);
                            
                        }
                        else
                        {
                            
                            newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform);
                            newRoom.GetComponent<RoomBehaviour>().UpdateRoom(currentCell.status);
                            newRoom.GetComponent<RoomBehaviour>().Coin();
                            int numberEnemy = Random.RandomRange(0, 4);
                            int selec = Random.RandomRange(0, 2);
                            if (selec == 0)
                            {
                                for(int a = 0; a < numberEnemy; a++)
                                {
                                    int typeofEnemy = Random.RandomRange(0, Enemy.Length);
                                    GameObject creep = Instantiate(Enemy[typeofEnemy], newRoom.transform.position, Quaternion.identity);
                                    enemy.Add(creep);
                                }
                            }   
                            
                            newRoom.name += " " + i + " - " + j;
                            child.Add(newRoom);
                            
                        }
                        

                    }


                }
            }
            
        }

        [System.Obsolete]
        public void MazeGenerator()
        {
            board = new List<Cell>(); // list luu tung cell ( cac note trong maze) board.size = x * y
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    board.Add(new Cell()); // tao ra moi cell moi trong ma tran vector 2 x * y
                }
            }

            board[board.Count-1].TargetRoom = true;

            //int targetroom = Random.RandomRange(0, board.Count - 1); //*
            //board[targetroom].TargetRoom = true; //*
            int currentCell = statusPos; // cell khoi dau o 0 chay tu list(0)
            Stack<int> path = new Stack<int>(); // tao stack LIFO
            int k = 0;
            
            while (k < 1000)
            {
                k++;
                board[currentCell].visited = true; // cell duoc tham chieu den xet truong vistited = 1

                if (currentCell == board.Count - 1)
                {
                    break;
                }
                //if (board[currentCell].TargetRoom == true) //*
                //{
                //    break;
                //}
                List<int> neighbors = CheckNeighbors(currentCell); // check neighbor cua cell dang duoc xet den 
                if (neighbors.Count == 0)
                {
                    if (path.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        currentCell = path.Pop();
                    }
                }
                else
                {
                    path.Push(currentCell);
                    
                    int newCell = neighbors[Random.RandomRange(0, neighbors.Count)];// Random duong di tiep theo tu currentCell
                    if (newCell > currentCell)
                    {
                        if (newCell - 1 == currentCell)
                        {
                            board[currentCell].status[2] = true;
                            //numberOfCellInMaze.Add(currentCell);//*
                            currentCell = newCell; // chuyen cell dang xet qua cell neighbor                           
                            board[currentCell].status[3] = true;
                           

                        }
                        else
                        {
                            board[currentCell].status[1] = true;
                            //numberOfCellInMaze.Add(currentCell);//*
                            currentCell = newCell;// chuyen cell dang xet qua cell neighbor 
                            board[currentCell].status[0] = true;
                        }
                    }
                    else
                    {
                        if (newCell + 1 == currentCell)
                        {
                            board[currentCell].status[3] = true;
                            //numberOfCellInMaze.Add(currentCell);//*
                            currentCell = newCell;// chuyen cell dang xet qua cell neighbor 
                            board[currentCell].status[2] = true;

                        }
                        else
                        {
                            board[currentCell].status[0] = true;
                            //numberOfCellInMaze.Add(currentCell);//*
                            currentCell = newCell;// chuyen cell dang xet qua cell neighbor 
                            board[currentCell].status[1] = true;
                        }
                    }
                }

            }
            GenerarDungeon();
        }
        List<int> CheckNeighbors(int cell) //tao list check neigbor tra ve mot neighbor
        {
            List<int> neighbors = new List<int>(); //list kieu int  chua neighborsx
                                                   // up neighbor
            if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
            {
                neighbors.Add(Mathf.FloorToInt(cell - size.x));
                Debug.Log(cell - size.x);
            }
            //down nighbor
            if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
            {
                neighbors.Add(Mathf.FloorToInt(cell + size.x));
                Debug.Log(cell + size.x);
            }
            if (((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited))
            {
                neighbors.Add(Mathf.FloorToInt(cell + 1));
                Debug.Log(cell + 1);
            }
            if ((cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited))
            {
                neighbors.Add(Mathf.FloorToInt(cell - 1));
                Debug.Log(cell - 1);
            }
            return neighbors;

        }
        

        public void DesTroyClone()
        {
            for(int i =  0; i < child.Count; i++)
            {
                Destroy(child[i]);
                
            }
            for ( int i = 0; i < enemy.Count; i++)
            {
                Destroy(enemy[i]);
            }
            child.Clear();
            enemy.Clear();
        }
        }
    }

