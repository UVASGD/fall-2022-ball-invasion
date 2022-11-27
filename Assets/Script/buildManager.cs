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

    public propData freezingData;
    public propData bombData;

    private turretData selectedTurret;
    private propData selectedProp;

    // controller of upgrading tool
    private upgradeController controller; 

    private mapCube selectedMapCube;

    public Text moneyText;
    public int money = 500;

    public Animator moneyAnimator;

    public static buildManager instance;

    public GameObject buildCanvas;
    private Animator buildAnimator;

    private void Start()
    {
        instance = this;
        controller = GetComponent<upgradeController>();
        buildAnimator = buildCanvas.GetComponent<Animator>();
    }

    public void updateMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if(selectedTurret != null)
            {
                SpawnTurret();
            }
            if(selectedProp != null)
            {
                SpawnProp();
            }
            upgradeTurret();
        }    
    }

    public void SpawnTurret()
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
                if (MapCube == selectedMapCube && buildCanvas.activeInHierarchy)
                {
                    StartCoroutine(HideBuildUI());
                }
                else
                {
                    ShowBuildUI(MapCube);
                    selectedMapCube = MapCube;
                }
            }
        }
            
        
    }


    public void upgradeTurret()
    {
        // if pointer is not on the UI, do the following things
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
        if (isCollider)
        {
            mapCube MapCube = hit.collider.GetComponent<mapCube>();
            if (MapCube.turretGo != null)
            {
                // if mouse hit a mapCube with turret, then we do upgrading
                if (MapCube == selectedMapCube && controller.upgradeCanvas.activeInHierarchy)
                {
                    StartCoroutine(controller.HideUpgradeUI());
                }
                else
                {
                    StartCoroutine(HideBuildUI());
                    controller.ShowUpgradeUI(MapCube, MapCube.isUpgraded);
                    selectedMapCube = MapCube;
                }
            }
        }
    }


    public void SpawnProp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Road"));
        if (isCollider)
        {
            if(money >= selectedProp.cost)
            {
                updateMoney(-selectedProp.cost);
                Instantiate(selectedProp.propPrefab, hit.point, Quaternion.identity);
            }
            else
            {
                moneyAnimator.SetTrigger("flick");
            }
        }
    }

    // methods for turret and prop selections
    public void OnLaserSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = laserData;
            selectedProp = null;
        }
    }

    public void OnMissleSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = missleData;
            selectedProp = null;
        }
    }

    public void OnStandardSelected(bool IsOn)
    {
        if (IsOn)
        {
            selectedTurret = standardData;
            selectedProp = null;
        }
    }

    public void OnFreezingSelected(bool IsOn)
    {
        if(IsOn)
        {
            selectedProp = freezingData;
            selectedTurret = null;
        }
    }

    public void OnBombSelected(bool IsOn)
    {
        if(IsOn)
        {
            selectedProp = bombData;
            selectedTurret = null;
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

    public void OnBuildButtonDown()
    {
        int buildCost = selectedTurret.cost;
        if (money < buildCost)
        {
            moneyAnimator.SetTrigger("flick");
            ShowBuildUI(selectedMapCube);
        }
        else
        {
            updateMoney(-buildCost);
            selectedMapCube.BuildTurret(selectedTurret);
            StartCoroutine(HideBuildUI());
        }
    }

    public void ShowBuildUI(mapCube mc)
    {
        controller.upgradeCanvas.SetActive(false);
        buildCanvas.SetActive(false);
        buildCanvas.SetActive(true);
        buildCanvas.GetComponent<Transform>().position = mc.transform.position;
    }

    public IEnumerator HideBuildUI()
    {
        buildAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        buildCanvas.SetActive(false);
    }
}

