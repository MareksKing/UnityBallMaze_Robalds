using System.Globalization;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    // [SerializeField] private GameObject level;
    [SerializeField] private UnityEngine.Material material;
    private GameObject[] levelObjects;
    private List<GameObject> levelsCleared = new List<GameObject>();
    private List<GameObject> levelsNotCleared = new List<GameObject>();
    private GameObject LevelClearing;
    private String LevelTag;
    private int Level;
    public Text countText;

    public Text winText;

    
    
    // Start is called before the first frame update
    void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("Cleared");
        foreach (var item in objects)
        {
            levelsCleared.Add(item);
        }
        var NotClearedLevels = GameObject.FindGameObjectsWithTag("Not Cleared");
        foreach (var item in NotClearedLevels)
        {
            levelsNotCleared.Add(item);
        }
        foreach (var level in levelsNotCleared)
        {
            if(levelsNotCleared.Count > 0){
                UnityEngine.Debug.Log(level);
                level.SetActive(false);

            }
        }
        Level = 1;
        countText.text  = "Level: " + Level.ToString();
        winText.text = "";

        LevelClearing = GameObject.FindGameObjectWithTag("Clearing");
    }

    // Update is called once per frame
    void Update()
    {
        LevelTag = "Level" + Level.ToString();
        LevelClearing.SetActive(true);    
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(LevelTag))
        {
            
            levelObjects = GameObject.FindGameObjectsWithTag(LevelTag);
            foreach (var item in levelObjects)
            {
                Material[] materials = item.GetComponent<MeshRenderer>().materials;
                materials[0] = material;
                item.GetComponent<MeshRenderer>().materials = materials;
            }

            LevelClearing.tag = "Cleared";
            LevelClearing = levelsNotCleared[0];
            levelsNotCleared.Remove(levelsNotCleared[0]);
            Level++;           
                      
            countText.text = "Level: " + Level.ToString();
            if (Level >= 6) {winText.text = "Tu uzvarēji!";}
        }
    }

}
