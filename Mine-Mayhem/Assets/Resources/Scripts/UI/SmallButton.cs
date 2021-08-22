using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallButton : MonoBehaviour
{
    public Image ButtonOutline { get { return transform.GetChild(0).GetComponent<Image>(); } }
    public Image ButtonLockIcon { get { return transform.GetChild(2).GetComponent<Image>(); } }
}
