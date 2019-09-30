using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.Data;
using Assets.HeroEditor.Common.Enums;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using UnityEngine;

public class ChangeProjectile : Character
{
    public GameObject projectile;
    public void OnEnable()
    {
        //UpdateAnimation();
    }

    public void EquipMeleeWeapon(Sprite sprite, Sprite trail, bool twoHanded = false)
    {
        projectile.GetComponent<SpriteRenderer>().sprite = sprite;

        //PrimaryMeleeWeaponTrailRenderer.sprite = trail;
        WeaponType = twoHanded ? WeaponType.Melee2H : WeaponType.Melee1H;
        Initialize();
    }
}