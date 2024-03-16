using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Save Data", menuName = "Save System/Character Data")]
public class CharacterSaveData : ScriptableObject
{
    [Header("Character Name")]
    public string characterName = "Character";

    [Header("Character Stats")]
    public int characterLevel;

    [Header("Equipment")]
    public int currentRightHandWeaponID;
    public int currentLeftHandWeaponID;

    public int currentHeadGearItemID;

    [Header("World Coordinates")]
    public Vector3 currentPosition;

    public void ResetData()
    {
        currentPosition = Vector3.zero;
        characterName = "Character";
        characterLevel = 10;
        currentRightHandWeaponID = 0000;
        currentLeftHandWeaponID = 0000;
        currentHeadGearItemID = 0000;
    }
}
