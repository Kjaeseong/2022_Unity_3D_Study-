using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//특성 : 클래스, 메서드, 변수 등에다 추가 정보를 줄 수 있다.
[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
public AudioClip ShotClip;
public AudioClip ReloadClip;

public float Damage = 25f;

public int InitialAmmoCount = 100;
public int MagazineCapacity = 24;

public float FireCooltime = 0.12f;
public float ReloadTime = 1.8f;
}
