// This is the clss that controlls the construction and upgrade of turrets

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buildManager : MonoBehaviour
{
    // data of the three kinds of turrets
    public turretData laserData;
    public turretData missleData;
    public turretData standardData;

    private turretData selectedTurret;

    // controller of upgrading tool
    private upgradeController controller; 

    private mapCube selectedMapCube;

    public Text moneyText;
    public int money = 500;

    public Animator moneyAnimator;

    public static buildManager instance;

    private void Start()
    {
        instance = this;
        controller = GetComponent<upgradeController>();
    }

    public void updateMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // when the left key is down
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // if pointer is not on the UI, do the following things
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    mapCube MapCube = hit.collider.GetComponent<mapCube>();
                    if (selectedTurret != null && MapCube.turretGo == null)
                    {
                        // if mouse hit a mapCube with no turret on it, then we build a turret
                        if (money >= selectedTurret.cost)
                        {
                            updateMoney(-selectedTurret.cost);
                            MapCube.BuildTurret(selectedTurret);
                        }
                        else
                        {
                            // if lack money, trigger money animation
                            moneyAnimator.SetTrigger("flick");
                        }
                    }
                    else if (MapCube.turretGo != null)
                    {
                        // if mouse hit a mapCube with turret, then we do upgrading
                        if (MapCube == selectedMapCube && controller.upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(controller.HideUpgradeUI());
                        }
                        else
                        {
                            controller.ShowUpgradeUI(MapCube, MapCube.isUpgraded);
                            selectedMapCube = MapCube;
                        }
                    }
                }
            }
        }
    }

    // methods for turret selections
    public void OnLaserSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = laserData;
        }
    }

    public void OnMissleSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = missleData;
        }
    }

    public void OnStandardSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = standardData;
        }
    }

    // methods for upgrade and destroy button
    public void OnUpgradeButtonDown()
    {
        int upgradeCost = selectedMapCube.TurretData.upgradeCost;
        if (money < upgradeCost)
        {
            moneyAnimator.SetTrigger("flick");
            controller.ShowUpgradeUI(selectedMapCube);
        }
        else
        {
            updateMoney(-upgradeCost);
            selectedMapCube.UpgradeTurret();
            StartCoroutine(controller.HideUpgradeUI());
        }
    }

    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(controller.HideUpgradeUI());
    }
}
