using System.Collections;
using UnityEngine;
using System;

// 총을 구현한다
public class Gun : MonoBehaviour 
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State 
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State CurrentState { get; private set; } // 현재 총의 상태

    public Transform FireTransform; // 총알이 발사될 위치

    public ParticleSystem MuzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem ShellEjectEffect; // 탄피 배출 효과

    private LineRenderer _bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    public GunData Data;
    private AudioSource _audioSource; // 총 소리 재생기
    private float _fireDistance = 50f; // 사정거리

    private int _remainAmmo = 100; // 남은 전체 탄약
    private int _ammoInMagazine; // 현재 탄창에 남아있는 탄약

    private float _lastFireTime; // 총을 마지막으로 발사한 시점


    private void Awake()         // 사용할 컴포넌트들의 참조를 가져오기
    {
        _bulletLineRenderer = GetComponent<LineRenderer>();
        _bulletLineRenderer.positionCount = 2;
        _bulletLineRenderer.enabled = false;

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //총 상태 초기화
        _remainAmmo = Data.InitialAmmoCount;
        _ammoInMagazine = Data.MagazineCapacity;
        CurrentState = State.Ready;
        _lastFireTime = 0f;
    }

    public void Fire()     // 발사 시도
    {
        //발사 가능 : 상태가 레디일 때, 쿨타임 다 찼을 때
        if(CurrentState != State.Ready || Time.time < _lastFireTime + Data.FireCooltime)
        {
            return;
        }

        _lastFireTime = Time.time;
        Shot();
    }

    private void Shot()     // 실제 발사 처리
    {
        Vector3 hitPosition;

        RaycastHit hit;
        if(Physics.Raycast(FireTransform.position, transform.forward, out hit, _fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if(target != null)
            {
                target.OnDamage(Data.Damage, hit.point, hit.normal);
            }

            hitPosition = hit.point;
        }
        else
        {
            Debug.Log("아무일도 없었다.");
            hitPosition = FireTransform.position + transform.forward * _fireDistance;
            //이걸 애초에 초기화할때 사용하는 방법도..
        }

        StartCoroutine(ShotEffect(hitPosition));

        --_ammoInMagazine;
        if(_ammoInMagazine <= 0)
        {
            CurrentState = State.Empty;
        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) 
    {
        //발사 이펙트 재생
        MuzzleFlashEffect.Play();
        ShellEjectEffect.Play();

        //점 세팅
        _bulletLineRenderer.SetPosition(0, FireTransform.position);
        _bulletLineRenderer.SetPosition(0, hitPosition);
        _bulletLineRenderer.enabled = true;
        
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        
        _bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        _bulletLineRenderer.enabled = false;
    }

    public bool TryReload()     // 재장전 시도
    {
        //1. 리로드 상태가 아니면 되고
        //2. 탄창이 가득 차있지 않아야 됨
        //3. 장전할 총알이 없음.
        if(CurrentState != State.Reloading || _remainAmmo <= 0 || _remainAmmo == Data.MagazineCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());

        return true;
    }

    private IEnumerator ReloadRoutine()     // 실제 재장전 처리를 진행

    {
        // 현재 상태를 재장전 중 상태로 전환
        CurrentState = State.Reloading;
        
        //재장전 소리 재생
        _audioSource.PlayOneShot(Data.ReloadClip);
        
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(Data.ReloadTime);

        //총알을 채워줘야함
        int ammoToFill = Mathf.Min(Data.MagazineCapacity - _ammoInMagazine, _remainAmmo);

        //총알이 충분하다면
        //if(ammoToFill)
        _ammoInMagazine += ammoToFill;
        _remainAmmo -= ammoToFill;


        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}