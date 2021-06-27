using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinball_recall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _grid_sprite, _bounce;
    public pbrc_button _button;
    public pbrc_ball _ball;
    public GameObject[,] _grid;
    public GameObject _gridparent;
    public float _OX, _OY, _cellWidth, _cellHeight;
    int _gridRow, _gridCol;
    Chest _chest;
    [SerializeField]
    float end_time;
    [SerializeField]
    float game_timer;
    public float now_time;
    public bool _has_chosen = false, _game_isRunning = false;
    // Update is called once per frame
    private void Update()
    {
        if (_game_isRunning)
        {
            now_time = Time.time;
            if (now_time >= end_time && !_has_chosen)
            {
                end_game(false);
            }
        }        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Physics2D.IgnoreLayerCollision(10, 12, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(9, 12, true);
    }

    public void Start_Game(int row, int col, Vector3 position, Chest chest)
    {
        end_time = Time.time + game_timer;
        generate_grid(row, col, position);
        _chest = chest;
        _game_isRunning = true;
        _has_chosen = false;
    }

    void generate_grid(int row, int col, Vector3 position)
    {        
        _gridRow = row;
        _gridCol = col;
        float width = _grid_sprite.GetComponent<SpriteRenderer>().bounds.size.x;
        float height = _grid_sprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _cellHeight = height;
        _cellWidth = width;            
        transform.localScale *= 5;        
        
        Vector3 offset = new Vector3(_OX, _OY);
        for (int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {
                Vector3 adjust = new Vector3(width * i, height * j) ;
                Instantiate(_grid_sprite, _gridparent.transform);
                int len = _gridparent.GetComponentsInChildren<Transform>().Length;
                _gridparent.GetComponentsInChildren<Transform>()[len-1].gameObject.transform.localPosition += adjust;
            }
        }
        generate_barrier();
        generate_buttons();

        float scaledWidth = GetComponentInChildren<pbrc_component>().GetComponent<SpriteRenderer>().bounds.size.x;
        float scaledHeight = GetComponentInChildren<pbrc_component>().GetComponent<SpriteRenderer>().bounds.size.y;
        position.x -= Mathf.FloorToInt(col / 2) * scaledWidth;
        position.y -= Mathf.FloorToInt(row / 2) * scaledHeight;
        position.z = -13;
        transform.position = position;
    }

    void generate_barrier(int maxCount = 5, int prob=30)
    {
        int count = 0;
        foreach(pbrc_grid cell in _gridparent.GetComponentsInChildren<pbrc_grid>())
        {
            if(count >= maxCount)
            {
                break;
            }
            else if(prob >= Random.Range(0,100))
            {
                if(Random.Range(0, 100) <= 50)
                {
                    Instantiate(_bounce, cell.transform.position, _bounce.transform.rotation, _gridparent.transform);
                }
                else
                {
                    Quaternion newrotation = _bounce.transform.rotation;
                    newrotation.z *= -1;
                    Instantiate(_bounce, cell.transform.position, newrotation, _gridparent.transform).GetComponent<bounce>().Flip *= -1;
                }
                count++;
            }            
        }
    }

    void generate_buttons()
    {
        pbrc_grid[] array_of_cell = _gridparent.GetComponentsInChildren<pbrc_grid>();
        pbrc_grid[,] array2d_of_cell = new pbrc_grid[_gridRow,_gridCol];
        //convert 1D array to 2D
        int index = 0;
        for(int i = 0; i < _gridRow; i++)
        {
            for(int j = 0; j < _gridCol; j++)
            {
                array2d_of_cell[i,j] = array_of_cell[index];
                index++;
            }
        }
        #region adjust position button
        bool flag = true;
        for (int i = 0; i < _gridRow; i++)
        {
            for (int j = 0; j < _gridCol; j++)
            {
                if (i % (_gridRow - 1) == 0 || j % (_gridCol - 1) == 0) 
                {
                    Vector3 adjust = new Vector3(0,0,0);
                    //sisi
                    pbrc_button button = new pbrc_button();
                    if (i == 0)
                    {
                        adjust = new Vector3(-_cellWidth, 0, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(1, 0);
                    }
                    else if (i == _gridRow - 1) 
                    {
                        adjust = new Vector3(_cellWidth, 0, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(-1, 0);
                    }
                    else if (j == 0)
                    {
                        adjust = new Vector3(0, -_cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, 1);
                    }
                    else if (j == _gridCol - 1)
                    {
                        adjust = new Vector3(0, _cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, -1);
                    }
                    //int len = _gridparent.GetComponentsInChildren<pbrc_button>().Length;
                    button.transform.localPosition += adjust;
                    button.Pinball_recall = this;
                    if(Random.Range(1,100) < 30 && flag)
                    {
                        button.IsFinish = true;
                        button.GetComponent<SpriteRenderer>().color = Color.green;
                        flag = false;
                    }
                    //button.setup(_ball);

                    //corners
                    if (i == 0 && j == 0)
                    {
                        adjust = new Vector3(0, -_cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, 1);
                    }
                    else if (i == _gridRow - 1 && j == _gridCol - 1)
                    {
                        adjust = new Vector3(0, _cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, -1);
                    }
                    else if (j == 0 && i == _gridRow - 1)
                    {
                        adjust = new Vector3(0, -_cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, 1);
                    }
                    else if (j == _gridCol - 1 && i == 0)
                    {
                        adjust = new Vector3(0, _cellHeight, 0);
                        button = Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                        button.setDirection(0, -1);
                    }
                    else
                    {
                        adjust = new Vector3(0, 0, 0);
                    }
                    button.transform.localPosition += adjust;
                    button.Pinball_recall = this;
                    if (Random.Range(1, 100) < 30 && flag)
                    {
                        button.IsFinish = true;
                        button.GetComponent<SpriteRenderer>().color = Color.green;
                        flag = false;
                    }
                    else if (i + j == _gridRow + _gridCol - 2 && flag) 
                    {
                        button.IsFinish = true;
                        button.GetComponent<SpriteRenderer>().color = Color.green;
                        flag = false;
                    }
                    //button.setup(_ball);
                    //else
                    //{
                    //    Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                    //}
                    //Instantiate(_button, array2d_of_cell[i, j].transform.position, array2d_of_cell[i, j].transform.rotation, _gridparent.transform);
                }
            }
        }
        #endregion
    }

    private void clear_all()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = new Vector3(1, 1, 1);
        foreach (pbrc_component child in _gridparent.GetComponentsInChildren<pbrc_component>())
        {
            Destroy(child.gameObject);
        }
    }

    public void end_game(bool isCorrect)
    {
        _game_isRunning = false;
        clear_all();
        _chest.Finish_Game(isCorrect);        
    }
}
