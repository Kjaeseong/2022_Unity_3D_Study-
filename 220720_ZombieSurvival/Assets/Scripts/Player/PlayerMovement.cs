using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour 
{
    [SerializeField]
    public float MoveSpeed = 5f; // 앞뒤 움직임의 속도
    public float RotateSpeed = 180f; // 좌우 회전 속도
    private float MoveTime = 0;

    private Animator _animator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput _input; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody _rigidbody; // 플레이어 캐릭터의 리지드바디
    private NavMeshAgent _nav;

    private static class AnimID
    {
        public static readonly int MOVE = Animator.StringToHash("Move");
    }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() 
    {
        //Rigidbody를 쓰고 있으면 여기서 쓰는게 맞음. 그래야 정교하게 움직임을 계산할 수 있음.
        move();
        rotate();

        _animator.SetFloat(AnimID.MOVE, _input.MoveDirection);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void move() 
    {
        //transform.forward * MoveSpeed * Time.fixedDeltaTime * _input.MoveDirection;
        //이 순서대로라면 벡터 연산이 3번 있음.
        
        Vector3 offset = MoveSpeed * Time.fixedDeltaTime * _input.MoveDirection * transform.forward;
        //이 코드는 벡터 연산이 한번이 되므로 전자보다 빠르다.

        _rigidbody.MovePosition(_rigidbody.position + offset);

        if (_input.CanMove)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(_input.MousePosition);

            RaycastHit hit;
            bool isHit = Physics.Raycast(mouseRay, out hit, 100f);
            if (isHit)
            {
                NavMeshHit meshHit;
                if (NavMesh.SamplePosition(hit.point, out meshHit, 2f, NavMesh.AllAreas))
                {
                    return;
                }
            }




            
        }
        else 
        { 
            Vector3 deltaPosition = MoveSpeed * Time.captureDeltaTime
        }





    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void rotate() 
    {
        float rotationAmount = _input.MoveDirection * RotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

    }
}