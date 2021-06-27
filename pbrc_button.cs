using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pbrc_button : pbrc_component
{
    Vector2 _direction = new Vector2(0, 0);
    public pbrc_ball _ball;
    public bounce _bounc;
    public float _force;
    bool _isStart = false, _isFinish = false;

    public pinball_recall Pinball_recall { get => _pinball_recall; set => _pinball_recall = value; }
    public bool IsFinish { get => _isFinish; set => _isFinish = value; }

    public void setup(pbrc_ball ball)
    {
        //_bounc = GetComponentInParent<pinball_recall>()._bounce.GetComponent<bounce>();
        _ball = ball;
        _direction = new Vector2(1, 0);
    }

    public void setDirection(float x, float y)
    {
        _direction = new Vector2(x, y);
    }
    
    private void OnMouseEnter()
    {
        //Debug.Log("IN");
    }

    private void OnMouseDown()
    {
        //Debug.Log("IN");
        if(_isFinish == false)
        {
            _isStart = true;
            pbrc_ball ball = Instantiate(_ball, transform.position + new Vector3(0, 0, -1), transform.rotation, transform);
            ball.GetComponent<Rigidbody2D>().AddForce(_direction * _force, ForceMode2D.Impulse);
            ball.transform.localScale *= 5;
            ball.setDirection(_direction);
            _pinball_recall._has_chosen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("WHAT?");
        if (collision.GetComponent<pbrc_ball>() != null && !_isStart)
        {
            Destroy(collision.gameObject);
            if (_isFinish)
            {
                Debug.Log("Correct");
                _pinball_recall.end_game(true);
            }
            else
            {
                Debug.Log("NOT Correct");
                _pinball_recall.end_game(false);
            }            
        }        
    }
}
