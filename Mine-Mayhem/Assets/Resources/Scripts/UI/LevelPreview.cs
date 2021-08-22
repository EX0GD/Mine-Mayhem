using UnityEngine;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{
    public Sprite LevelImage { get { return GetComponent<Image>().sprite; } set => GetComponent<Image>().sprite = value; }
    public GameObject LevelPreviewLock;
}
