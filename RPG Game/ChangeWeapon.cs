using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.EditorScripts;
using HeroEditor.Common;
using UnityEngine;
using Assets.HeroEditor.Common.ExampleScripts;


public class ChangeWeapon : MonoBehaviour
{
    public RuntimeSetup rts;

    public void Start() {
        rts = GameObject.FindWithTag("Character").GetComponent<RuntimeSetup>();
    }


    public void changeWeapon(string sname, string collection, string equipment)
    {
        switch (equipment.ToLower()) {

            case "sword":
                rts.EquipMeleeWeapon1H(sname, collection);
                break;
            case "armour":
                rts.EquipArmor(sname, collection);
                break;
            case "helm":
                rts.EquipHelmet(sname, collection);
                break;
            case "shield":
                rts.EquipShield(sname, collection);
                break;


            default:
                print("no equipment changed");
                break;

        }
    }

}
