using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    public float position;
    public int fitcubesnumber;
    public int totalcubes,totalholes;
    Ingamemanager ui;
    void Start()
    {
        ui = FindObjectOfType<Ingamemanager>();
        totalcubes = FindObjectsOfType<PushBrick>().Length;
        Hole[] hole = FindObjectsOfType<Hole>();
        for (int i = 0; i <hole.Length; i++)
        {
            totalholes += hole[i].fitcubesnumber;
        }
    }

    GameObject cube;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            if (fitcubesnumber ==1)
            {
                other.transform.DOMove(new Vector3(this.transform.parent.position.x, position, this.transform.position.z), 0.4f).SetDelay(0.3f).OnComplete(onhole);
                other.gameObject.tag = "Untagged";
                cube = other.gameObject;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("TEST  1");
                ui.currentcount++;
                if(totalcubes == ui.currentcount && totalholes == ui.currentcount)
                {
                    ui.Invoke("levelcompleted",1);
                }
                if (soundmanager.instance)
                    soundmanager.instance.playmysound(soundmanager.instance.hole);
                Invoke("particle", 0.5f);
            }
            else if(fitcubesnumber>1)
            {
                other.transform.DOMove(new Vector3(this.transform.parent.position.x, -0.2f, this.transform.position.z), 0.4f).SetDelay(0.3f).OnComplete(onhole);
                other.gameObject.tag = "Untagged";
                cube = other.gameObject;
                fitcubesnumber--;
                Debug.Log("TEST  2");
                ui.currentcount++;
                if (totalcubes == ui.currentcount && totalholes == ui.currentcount)
                {
                    ui.Invoke("levelcompleted", 1);
                }
                if (soundmanager.instance)
                    soundmanager.instance.playmysound(soundmanager.instance.hole);
                Invoke("particle", 0.5f);
            }

        }
    }

    public void onhole()
    {
        cube.GetComponent<Rigidbody>().isKinematic = true;
        cube.GetComponent<BoxCollider>().isTrigger = true;
    }
    public void particle()
    {
        ParticleSystem par = Instantiate(ui.inglassparticle, this.transform.position, Quaternion.identity);
        par.Play(); 
       // ParticleSystem par1 = Instantiate(ui.holeripple, this.transform.position, ui.holeripple.transform.rotation);
       // par1.Play();
    }
}
