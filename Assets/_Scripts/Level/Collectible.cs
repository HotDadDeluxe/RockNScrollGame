using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public string collectibleType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.Collect(collectibleType);
            Destroy(gameObject);
        }
    }
}
