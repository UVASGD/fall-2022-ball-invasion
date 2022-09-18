using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mapCube : MonoBehaviour
{

    // (whether) there is a turret on the cube
    [HideInInspector]
    public GameObject turretGo;
    [HideInInspector]
    public turretData TurretData;

    // the prefab of construction effect
    public GameObject buildEffect;

    private new Renderer renderer;

    // whether the turret on the mapCube is upgraded
    [HideInInspector]
    public bool isUpgraded = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(turretData data)
    {
        turretGo = GameObject.Instantiate(data.turretPrefab, transform.position, Quaternion.identity);
        TurretData = data;
        UseBuildEffect();
    }

    public void UpgradeTurret()
    {
        if (isUpgraded) return;
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(TurretData.upgradedPrefab, transform.position, Quaternion.identity);
        isUpgraded = true;
        UseBuildEffect();
    }

    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        TurretData = null;
        turretGo = null;
        UseBuildEffect();
    }

    private void OnMouseEnter()
    {
        // if the mouse is on the mapCube
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // if there is not a turret on it and mouse is not on UI, then we use red color to mark it
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    private void UseBuildEffect()
    {
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
}
