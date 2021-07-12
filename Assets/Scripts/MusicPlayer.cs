using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] SingletonPatternMusic menuPlayerMusicPrefab;
    private void Awake()
    {
        if (menuPlayerMusicPrefab)
        {
            Destroy(GameObject.Find(menuPlayerMusicPrefab.name));
        }
    }
}
