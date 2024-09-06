using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


public class Ingamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public Color[] Bgcolor;
    public Text levelnumber;

    public Sprite[] Completesmiles;
    //public Sprite[] CompleteTextAwesome;
    private string[] CompleteTextAwesome = new string[] { "Fantastic!", "Epic!", "Incredible!", "wonderful!", "phenomenal!", "BRILLIAN" };
    public Sprite[] failsmiles;
    public Sprite[] Failtext;

    public GameObject levelcompletepanel, levelfailpanel;

    public Image levelcompletesmile, levelfailsmile , levelfailtext;

    public Text lvlcompletetext;
    public Text numberofcoinsText;
    public ParticleSystem inglassparticle;
    public ParticleSystem levelcomleteparticle;
    public ParticleSystem holeripple;
    public GameObject hand;
    public Transform Skins_Parent;
    public Sprite[] ui_skins;
    public Material[] skinMaterials;
    public Mesh[] skinMesh;

    public int currentcount;
    int numberofcoins;
    int[] randomnumberofcoins =  new[] { 100, 80, 75, 92, 172, 39, 73, 88, 101, 57, 49, 60, 77, 124 };
    void Awake()
    {
        AssingSkinstoshopbuttons();
        Loadcurrentskin();
        numberofcoins = PlayerPrefs.GetInt("Coinscount");
        numberofcoinsText.text = numberofcoins.ToString();
        //  material.SetColor("_BaseColor", color);
        levelnumber.text = "level " + SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "1")
        {
            hand.SetActive(true);
        }
        else
        {
            hand.SetActive(false);
        }
    }

    // Update is called once per frame
    public void Update()
    {
       if(Input.GetMouseButtonDown(0) && hand.activeInHierarchy)
        {
            hand.SetActive(false);
        }
    }

    public void onnextbuttonpress()
    {

        if (PlayerPrefs.GetInt("level", 1) >= SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(Random.Range(0, SceneManager.sceneCountInBuildSettings - 1));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 1) + 1));
           
        }
        PlayerPrefs.SetInt("level2", (PlayerPrefs.GetInt("level2") + 1));
       // if (PlayerPrefs.GetInt("level2") == 2)
        {
            PlayerPrefs.SetInt("level2", 0);
            //AdsManager.Instance.showAds();
        }
    }
    public void onretrybuttonpress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
        PlayerPrefs.SetInt("level3", (PlayerPrefs.GetInt("level3") + 1));
      //  if (PlayerPrefs.GetInt("level3") == 2)
        {
            PlayerPrefs.SetInt("level3", 0);
            Ads.Instance.ShowAds();
        }

    }
    public void levelcompleted()
    {
        levelcompletepanel.SetActive(true);
        ParticleSystem par = Instantiate(levelcomleteparticle, levelcomleteparticle.transform.position, levelcomleteparticle.transform.rotation);
        par.Play();
        levelcompletesmile.sprite = Completesmiles[Random.Range(0, Completesmiles.Length)];
        lvlcompletetext.text = CompleteTextAwesome[Random.Range(0, CompleteTextAwesome.Length)];
        int coins = randomnumberofcoins[ Random.Range(0,randomnumberofcoins.Length)];
        PlayerPrefs.SetInt("Coinscount", PlayerPrefs.GetInt("Coinscount") +coins);
        numberofcoinsText.text = PlayerPrefs.GetInt("Coinscount").ToString();
       if (soundmanager.instance)
            soundmanager.instance.playmysound(soundmanager.instance.levelcomplete);
    }
    public void levelfail()
    {
        levelfailpanel.SetActive(true);
        levelfailsmile.sprite = failsmiles[Random.Range(0, failsmiles.Length)];
        levelfailtext.sprite = Failtext[Random.Range(0, Failtext.Length)];
      //  if (soundmanager.instance)
          //  soundmanager.instance.playmysound(soundmanager.instance.fail);
    }
    bool x = false;
    public void onskipbuttonpress()
    {
        Ads.Instance.ShowAds();
    }
    public void rewardsuccess()
    {
        Ads.Instance.ShowAds();
        onnextbuttonpress();
    }
    //==========================================================================Shop begin==================================================

    public void randomcolorDefaultcubes()
    {
        int ran = Random.Range(0, Bgcolor.Length);
        int cubesran = Random.Range(0, Bgcolor.Length);
        PushButton[] buttons = FindObjectsOfType<PushButton>();
        CubeReleaseBUtton[] CubeReleasebuttons = FindObjectsOfType<CubeReleaseBUtton>();
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Bgcolor[ran]);
            buttons[i].transform.parent.GetChild(3).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Bgcolor[ran]);
            buttons[i].transform.parent.GetChild(2).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Bgcolor[ran]);
        }
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Bgcolor[cubesran]);
        }
        for (int i = 0; i < CubeReleasebuttons.Length; i++)
        {
            CubeReleasebuttons[i].gameObject.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Bgcolor[ran]);

        }
    }
    public void AssingSkinstoshopbuttons()
    {
        for(int i = 0; i < Skins_Parent.transform.childCount; i++)
        {
            Skins_Parent.transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = ui_skins[i];
            if (PlayerPrefs.GetInt("Foruse" + i) == i)
            {
                Skins_Parent.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                Debug.Log(PlayerPrefs.GetInt("Foruse" + i));

            }
           
        }
    }
    public void onbuybuttonpress()
    {
        int cost = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text);
        
        if (cost < numberofcoins)
        {
            int randomskin = Random.Range(0, skinMaterials.Length);
           
                if (PlayerPrefs.GetInt("Foruse" + randomskin) != randomskin)
                {
                    assignselectedskin(randomskin);
                    numberofcoins = numberofcoins - cost;
                    PlayerPrefs.SetInt("Coinscount", numberofcoins);
                    numberofcoinsText.text = numberofcoins.ToString();
                    AssingSkinstoshopbuttons();
                }
                else
                {
                    onbuybuttonpress();
                }
            
        }

    }
    public void onskinButtonpress()
    {
        int selectedskin = int.Parse(EventSystem.current.currentSelectedGameObject.gameObject.name);
        assignselectedskin(selectedskin);
        onBackbuttonpress();
    }
    public void assignselectedskin(int selectedskin)
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.GetComponent<MeshFilter>().mesh = skinMesh[selectedskin];
            cubes[i].gameObject.GetComponent<MeshRenderer>().material = skinMaterials[selectedskin];
        }
        PlayerPrefs.SetInt("currentskin", selectedskin);
        PlayerPrefs.SetInt("Foruse" + selectedskin, selectedskin);
    }
    public void Loadcurrentskin()
    {
        int current = PlayerPrefs.GetInt("currentskin");
       
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].gameObject.GetComponent<MeshFilter>().mesh = skinMesh[current];
            cubes[i].gameObject.GetComponent<MeshRenderer>().material = skinMaterials[current];
        }
        if (current == 0)
        {
            randomcolorDefaultcubes();
        }
    }
    public void onBackbuttonpress()
    {
        Skins_Parent.parent.gameObject.SetActive(false);
    }
    public void onshopbuttonpress()
    {
        Skins_Parent.parent.gameObject.SetActive(true);
       
    }
    //===============================================Shop end=========================
}
