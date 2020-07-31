using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attributes;
using System;
using UnityEngine.Audio;
[Serializable]
[CreateAssetMenu(fileName = "New BulletData", menuName = "BulletData")]
public class BulletData : ScriptableObject
{
    [Header("Bullet Graphics")]
    public Sprite Image;

    [ColorUsage(true, true)] public Color color;
    public GameObject AdditionalEffect;
    [Header("Bullet Parameters")]
    public float RangeInSeconds;
    public float Damage;
    public float Speed;
    public float FireRate = 1;
    public float Cost = 0;

    public float Inaccuracity = 0;
    public float BulletCount = 1;
    public float Knockback = 0;
    public float Recoil = 0;
    public float Size;
    [Header("Pierce")]
    public bool IsPiercing;
    [ConditionalField("IsPiercing")] public int PierceCount = 1;
    [Header("Homing")]



    public bool IsHoming;
    public bool IsAutoTargeting;

    public float HomingSpeed;
    public float HomingRotation;
    [Header("Explosion")]
    public bool IsExplosive;
    [ConditionalField("IsExplosive")] public float ExplosionRange = 0;
    [ConditionalField("IsExplosive")] public GameObject ExplosionEffect;
    [Header("Laser - NOT WORKING")]
    public bool Laser;
    [ConditionalField("Laser")] public float placeholder;
    [Header("Sound")]
    public bool IfBulletSound;
    [ConditionalField("IfBulletSound")] public AudioClip BulletSound;
    public bool IfFireSound;
    [ConditionalField("IfFireSound")] public AudioClip FireSound;



}
