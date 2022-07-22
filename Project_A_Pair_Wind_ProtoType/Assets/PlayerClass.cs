using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public float HP;
    public float SP;
    public float Atk;
    public float Dfe;

    private Player _player;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
