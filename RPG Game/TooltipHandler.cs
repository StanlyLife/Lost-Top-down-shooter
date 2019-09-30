using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    public ToolTip childtt;
    public string price;
    public string attack;


    private void OnMouseOver() {
        childtt.ShowToolTip(price,attack);
    }
    private void OnMouseExit() {
        childtt.HideToolTip();
    }
    private void OnMouseUp() {
        
        StartCoroutine(WaitForPurchase());
    }

    IEnumerator WaitForPurchase() {
        yield return new WaitForSeconds(.01f);
        if (attack.ToLower() != "restock") {
            gameObject.SetActive(false);
        }
    }
}
