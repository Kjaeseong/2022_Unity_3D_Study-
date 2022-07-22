using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum CharactorStatus
    {
        HP,
        SP,
        Atk,
        Dfe
    }
    
    public int ClassSelection = 0;
    public float HP;
    public float SP;
    public float Atk;
    public float Dfe;
    public bool ActiveFirstSkill;
    public bool ActiveSecondSkill;
    private bool jumping = false;
    [SerializeField] private float JumpForce;
    [SerializeField] private float MoveSpeed;
    
    private GameInput _input;
    private Rigidbody _rigid;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<GameInput>();
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.up == true) 
        {
            _rigid.velocity = new Vector3(0f, 10f, 0f);
            
        }
        if(_input.down == true) {transform.Translate(0f, 0f, 0f);}
        if(_input.left == true) {transform.Translate(-0.05f, 0f, 0f);}
        if(_input.right == true) {transform.Translate(0.05f, 0f, 0f);}
    }
}
