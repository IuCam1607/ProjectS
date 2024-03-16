using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopUp : MonoBehaviour
{
    public GameObject itemPopUp;

    private void Awake()
    {
        itemPopUp.SetActive(false);
    }
}
