using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : pbrc_component
{
    Vector2 _direction;
    public float _force;
    [SerializeField]
    int flip = 1;

    public int Flip { get => flip; set => flip = value; }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("COK");
        pbrc_ball theball = collision.gameObject.GetComponent<pbrc_ball>();
        theball.transform.position = transform.position;
        Rigidbody2D ballrigidbody = theball.GetComponent<Rigidbody2D>();
        ballrigidbody.velocity = new Vector2(0, 0);
        float x = theball.getDirection().x, y = theball.getDirection().y;
        if(theball.getDirection().x == 0)
        {
            //theball.setDirection(1, 0);
            _direction = new Vector2(y, 0);
        }
        else
        {
            //theball.setDirection(0, 1);
            _direction = new Vector2(0, x);
        }
        _direction *= flip;
        theball.setDirection(_direction);
        ballrigidbody.AddForce(_direction * _force, ForceMode2D.Impulse);
    }    
}
