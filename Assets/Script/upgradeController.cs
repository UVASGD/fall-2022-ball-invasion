using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeController : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public Button upgradeButton;
    public Button destroyButton;

    // the animator for showing upgrade canvas
    private Animator upgradeCanvasAnimator;

    public Text upgradeText;

    private void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    public void ShowUpgradeUI(mapCube selectedMapCube, bool isDisableUpgrade = false)
    {
        upgradeCanvas.SetActive(false);
        if (!isDisableUpgrade)
        {
            upgradeText.text = "Upgrade\n($" + selectedMapCube.TurretData.upgradeCost + ")";
        }
        else
        {
            upgradeText.text = "Upgrade";
        }
        upgradeCanvas.SetActive(true);
        upgradeCanvas.GetComponent<Transform>().position = selectedMapCube.transform.position;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    public IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeCanvas.SetActive(false);
    }
}
