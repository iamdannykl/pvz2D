using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class startADVT : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Graphic graphic;
    public AudioSource tap;

    public void OnPointerDown(PointerEventData eventData)
    {
        graphic.color = new Color(0.6f, 0.6f, 0.6f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tap.Play();
        graphic.color = new Color(1f, 1f, 1f);
        startSc.Instance.startGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        graphic = GetComponent<Graphic>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
