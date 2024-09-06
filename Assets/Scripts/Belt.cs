using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Belt : MonoBehaviour
{
    public float speed = 0.02f;
    public Vector3 movepostion;
    public int direction=1;

    public Material belt;
    public List<float> timetomove = new List<float>();
    private void LateUpdate()
    {
        speed += Time.deltaTime;
        belt.SetTextureOffset("_BaseMap", new Vector2(0,(direction*speed)));
    }
   public List<Transform> onbeltobject = new List<Transform>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            if (!onbeltobject.Contains(other.gameObject.transform))
            {
                onbeltobject.Add(other.transform);
            }
            for (int i = 0; i < onbeltobject.Count; i++)
            {
                if (onbeltobject[i].gameObject.tag=="Cube")
                     onbeltobject[i].transform.DOMove(movepostion, timetomove[i]).SetDelay(0.5f).SetEase(Ease.Linear);
                Debug.Log(onbeltobject[i].transform.position.z);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        
            onbeltobject.Remove(other.gameObject.transform);
        
    }
}
