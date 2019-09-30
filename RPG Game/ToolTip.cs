using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToolTip : MonoBehaviour {

    public Text tooltipTextPrice;
    public Text tooltipTextAttribute;
    public RectTransform bgTransform;

    [SerializeField]
    private Camera uiCamera;
    private static ToolTip instance;


    private void Awake() {
        bgTransform = gameObject.transform.Find("Background").GetComponent<RectTransform>();
        tooltipTextPrice = gameObject.transform.Find("Price").GetComponent<Text>();
        tooltipTextAttribute = gameObject.transform.Find("Attribute").GetComponent<Text>();

        instance = this;
    }

    private void Update() {
        //Input.mousePosition;
        Vector2 localPoint;
        //gameObject.transform.parent.GetComponent<RectTransform>()
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.transform.parent.GetComponent<RectTransform>(), Input.mousePosition ,uiCamera, out localPoint);
        //transform.localPosition = localPoint;
    }

    public void ShowToolTip(string price, string variable) {
        gameObject.SetActive(true);

        if(variable != "restock") {
            tooltipTextAttribute.text = variable;
        }
        tooltipTextPrice.text = price;
        //float textPaddingSize = 4f;
        //Vector2 backgroindSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        //bgTransform.sizeDelta = backgroindSize;
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string ttString) {
    }
    public static void HideTooltip_Static() {
        instance.HideToolTip();
    }

}

