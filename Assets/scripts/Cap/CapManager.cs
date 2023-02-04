using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CapManager : MonoBehaviour
{
    private int nCaps = 10;
    [SerializeField] Sprite[] Caps = new Sprite[10];
    public CapStats[] Statistics;



    public int currentCapIndex = 0;

    private void Awake()
    {
        Statistics = new CapStats[nCaps];
        for (int i = 0; i < Statistics.Length; i++)
        {
            Statistics[i] = new CapStats();
        }
        SaveCapStats();
        LoadCapStats();
        currentCapIndex = 0;
    }

    private void Start()
    {
    }

    private void Update()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<CapContainer>().updateSprite( Caps[currentCapIndex]);
    }

    public void updateCap(int index)
    {
        currentCapIndex = index;
    }

    public void SaveCapStats()
    {
        for (int i = 0; i < Statistics.Length; i++)
        {
            int test = Statistics[i].healthStat;
            PlayerPrefs.SetInt("CapStat_" + i + "_Health", test);
            PlayerPrefs.SetFloat("CapStat_" + i + "_Ranged", Statistics[i].rangedStat);
            PlayerPrefs.SetFloat("CapStat_" + i + "_Speed", Statistics[i].speedStat);
            PlayerPrefs.SetFloat("CapStat_" + i + "_Strenght", Statistics[i].strenghtStat);
            PlayerPrefs.SetFloat("CapStat_" + i + "_Toxic", Statistics[i].toxicStat);
        }
        PlayerPrefs.Save();
    }

    public void LoadCapStats()
    {
        for (int i = 0; i < Statistics.Length; i++)
        {
            Statistics[i].healthStat = PlayerPrefs.GetInt("CapStat_" + i + "_Health", 0);
            Statistics[i].rangedStat = PlayerPrefs.GetFloat("CapStat_" + i + "_Ranged", 0);
            Statistics[i].speedStat = PlayerPrefs.GetFloat("CapStat_" + i + "_Speed", 0);
            Statistics[i].strenghtStat = PlayerPrefs.GetFloat("CapStat_" + i + "_Strenght", 0);
            Statistics[i].toxicStat = PlayerPrefs.GetFloat("CapStat_" + i + "_Toxic", 0);
        }
    }


}
