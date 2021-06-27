using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pbrc_ball : pbrc_component
{
    Vector2 _direction;

    public void setDirection(float x, float y)
    {
        _direction = new Vector2(x, y);
    }

    public void setDirection(Vector2 dir)
    {
        _direction = dir;
    }

    public Vector2 getDirection()
    {
        return _direction;
    }
}