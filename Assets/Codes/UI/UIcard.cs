using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIcard : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public GameObject uicard;
    public Transform zuocao;
    public bool isXuan;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ZuoKaCao.Instance.currentCardNum < ZuoKaCao.Instance.cardsNum && !isXuan)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            GameObject ka = Instantiate(uicard, Vector3.zero, Quaternion.identity, zuocao);
            ka.GetComponent<UIPlantCards>().uicard = this;
            ZuoKaCao.Instance.currentCardNum++;
            isXuan = true;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
