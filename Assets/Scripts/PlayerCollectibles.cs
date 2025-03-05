using UnityEngine;
using System.Collections.Generic;

public class PlayerCollectibles : MonoBehaviour
{
    public static PlayerCollectibles Instance;

    private Dictionary<string, int> collectiblesCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            collectiblesCount = new Dictionary<string, int>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Collect(string collectibleType)
    {
        if (collectiblesCount.ContainsKey(collectibleType))
        {
            collectiblesCount[collectibleType]++;
        }
        else
        {
            collectiblesCount[collectibleType] = 1;
        }
    }

    public int GetCollectibleCount(string collectibleType)
    {
        return collectiblesCount.ContainsKey(collectibleType) ? collectiblesCount[collectibleType] : 0;
    }
}
