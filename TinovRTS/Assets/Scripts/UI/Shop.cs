using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public BuidlingBluepring mainBase;
    public BuidlingBluepring circleTurret;
    public BuidlingBluepring cubicTurret;

    public BuidlingBluepring cubicStructure;
    public BuidlingBluepring circleStructure;
    public BuidlingBluepring piramidStructure;

    BuildingSystem buildingSystem;

    private void Start()
    {
        buildingSystem = BuildingSystem.instanceOfBuildsingSystem;
    }
    void DeactivateThisUI()
    {
        gameObject.SetActive(false);
    }

    public void SelectMainBase()
    {
        buildingSystem.SetBuilding(mainBase);
        DeactivateThisUI();
    }
    public void SelectCircleTurret()
    {
        buildingSystem.SetBuilding(circleTurret);
        DeactivateThisUI();
    }
    public void SelectCubicTurret()
    {
        buildingSystem.SetBuilding(cubicTurret);
        DeactivateThisUI();
    }

    public void SelectCubicStructure()
    {
        buildingSystem.SetBuilding(cubicStructure);
        DeactivateThisUI();
    }

    public void SelectCirclSetructure()
    {
        buildingSystem.SetBuilding(circleStructure);
        DeactivateThisUI();
    }

    public void SelectPiramidStructure()
    {
        buildingSystem.SetBuilding(piramidStructure);
        DeactivateThisUI();
    }
}
