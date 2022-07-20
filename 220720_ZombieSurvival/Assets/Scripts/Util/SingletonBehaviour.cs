using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// template <typename T>
// where 절 : T에 대한 제약 조건을 나타낸다.
// 제네릭을 쓸 경우에만 사용할 수 있는 문법
// 타입은 MonoBehaviour를 상속받아야 한다.
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get 
        { 
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance != null)
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

}
