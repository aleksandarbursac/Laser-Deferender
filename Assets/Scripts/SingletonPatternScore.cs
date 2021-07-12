using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPatternScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
