using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PushButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pushpipe, pushbrick;
    public float pipelenth,bricklenth;
    float currentbrickpos;
    public PushButton pushButton1,pushButton2, pushButton3;
    void Start()
    {
        currentbrickpos = pushbrick.transform.localPosition.x;
    }

    public void psuh()
    {
        pushpipe.DOScaleY(pipelenth, 0.3f).OnComplete(closepipe).SetEase(Ease.Linear);
        pushbrick.DOLocalMoveX(currentbrickpos + bricklenth, 0.3f).SetEase(Ease.Linear);
    }
    
    public void OnMouseDown()
    {
        pushpipe.DOScaleY(pipelenth, 0.3f).OnComplete(closepipe).SetEase(Ease.Linear);
        pushbrick.DOLocalMoveX(currentbrickpos+bricklenth, 0.3f).SetEase(Ease.Linear) ;
        if(pushButton1)
            pushButton1.psuh(); 
        if(pushButton2)
            pushButton2.psuh();
        if (pushButton3)
            pushButton3.psuh();
        if (soundmanager.instance)
            soundmanager.instance.playmysound(soundmanager.instance.buttonclick);
    }
    List<Transform> cubes = new List<Transform>();
    public void closepipe()
    {
       
        pushpipe.DOScaleY(0.2f, 0.3f).SetEase(Ease.Linear);
        pushbrick.DOLocalMoveX(currentbrickpos, 0.3f).SetEase(Ease.Linear);
        if (pushbrick.transform.GetChild(0).childCount != 0)
        {
            
        for(int i=0;i< pushbrick.transform.GetChild(0).childCount;i++)
          {
                cubes.Add(pushbrick.transform.GetChild(0).GetChild(i));
        }
        for(int j = 0; j < cubes.Count; j++)
            {
                cubes[j].parent = null;
                cubes[j].transform.position = new Vector3(Mathf.RoundToInt(cubes[j].transform.position.x), cubes[j].transform.position.y, Mathf.RoundToInt(cubes[j].transform.position.z));
                //cubes[j].transform.DOLocalMove(new Vector3(Mathf.RoundToInt(cubes[j].transform.localPosition.x), cubes[j].transform.localPosition.y, Mathf.RoundToInt(cubes[j].transform.localPosition.z)), 0.05f);//.OnComplete(roundtoitn);
            }
       }
    }
}
