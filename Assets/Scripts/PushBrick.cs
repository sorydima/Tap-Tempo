using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBrick : MonoBehaviour
{
    Transform cube;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Brick" /*|| collision.gameObject.tag=="Cube"*/)
        {
            this.transform.parent = collision.transform.transform;
            if (soundmanager.instance)
                soundmanager.instance.playmysound(soundmanager.instance.cubemove);
        }
    }
}

