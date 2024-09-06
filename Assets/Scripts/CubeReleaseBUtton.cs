using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class CubeReleaseBUtton : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberofcubes;
    public Vector3 position;
    public GameObject releasebutton1, releasebutton2, releasebutton3;
    //public tra
    void Start()
    {
        this.transform.GetChild(0).GetComponent<TextMesh>().text = numberofcubes.ToString();
    }
 
    // Update is called once per frame
  public  List<Transform> releasedcubes = new List<Transform>();
    Transform cube;
    bool wait = true;
    public void OnMouseDown()
    {
        if (wait)
        {


            if (numberofcubes > 0)
            {
                if (releasebutton1)
                    releasebutton1.GetComponent<CubeReleaseBUtton>().releasecube();
                if (releasebutton2)
                    releasebutton2.GetComponent<CubeReleaseBUtton>().releasecube();
                if (releasebutton3)
                    releasebutton3.GetComponent<CubeReleaseBUtton>().releasecube();

                cube = transform.GetChild(1);
                numberofcubes--;
                this.transform.GetChild(0).GetComponent<TextMesh>().text = numberofcubes.ToString();
                cube.transform.parent = null;
                cube.transform.DOMove(position, 0.5f);
                cube.transform.rotation = Quaternion.Euler(-90, 0, 0);
                cube.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(removekinametic).SetEase(Ease.Linear);
                // releasedcubes.Add(cube);
                if (soundmanager.instance)
                    soundmanager.instance.playmysound(soundmanager.instance.spawn);
            }
            wait = false;
        }
    }
    public void removekinametic()
    {
        cube.transform.GetComponent<Rigidbody>().isKinematic = false;
        wait = true;
    }

    public void releasecube()
    {
        if (numberofcubes > 0)
        {
            cube = transform.GetChild(1);
            numberofcubes--;
            this.transform.GetChild(0).GetComponent<TextMesh>().text = numberofcubes.ToString();
            cube.transform.parent = null;
            cube.transform.DOMove(position, 0.5f);
            cube.transform.rotation = Quaternion.Euler(-90, 0, 0);
            cube.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(removekinametic).SetEase(Ease.Linear);
            // releasedcubes.Add(cube);
        }
    }
}
