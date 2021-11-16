using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int playerLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            playerLevel = 1;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddExp()
    {
        instance.playerLevel += 1;
    }
    public static int getLevel()
    {
        return instance.playerLevel;
    }
    
}
