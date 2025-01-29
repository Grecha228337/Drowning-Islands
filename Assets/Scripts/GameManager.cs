using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager current;
    private void Awake()
    {
        current = this;
    }
    [Header("Tools")]
    public Tool axe;
    public Tool pickaxe;
    public HammerAttackArea hammer;

    [Header("Drop")]
    public Vector2Int woodDrop = new Vector2Int(1, 3);
    public Vector2Int rockDrop = new Vector2Int(1, 3);

    public Dictionary<ResourceType, int> amountOfResources = new Dictionary<ResourceType, int>();

    [Header("Spawners/Churches")]
    [SerializeField]private RandomResourseSpawner[] spawners;
    public int churchCount = 0;
    public TextMeshProUGUI churchCountText;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rocksText;
    public TextMeshProUGUI planksText;
    public TextMeshProUGUI nailsText;
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI pokeballText;
    public GameObject pokeball;
    [SerializeField] private BuildingIcon[] buildingIcons;
    [SerializeField] private UpgradeIcon[] upgradeIcons;

    public bool isPickaxeUpgraded; // Апгрейд, который позволяет с небольшим шансом выбить руду

    [Header("Statistic")]
    public Dictionary<ResourceType, int> resourceCollected = new Dictionary<ResourceType, int>();
    public int buildingCreated;

    public void Start()
    {

        for (int i = 0; i < 7; i++)
        {
            amountOfResources.Add((ResourceType)i, 0);
            resourceCollected.Add((ResourceType)i, 0);
        }
        /*
        amountOfResources.Add(ResourceType.Wood, 20);
        amountOfResources.Add(ResourceType.Money, 0);
        amountOfResources.Add(ResourceType.Planks, 0);
        amountOfResources.Add(ResourceType.Rock, 0);
        amountOfResources.Add(ResourceType.Pokeball, 0);
        amountOfResources.Add(ResourceType.Nails, 0);
        amountOfResources.Add(ResourceType.Ore, 0);
        */
        isPickaxeUpgraded = false;
        churchCount = 0;
    }
    public void UpgradeTool(ToolType toolType, UpgradeType upgradeType)
    {
        switch (toolType)
        {
            case ToolType.Axe:
                switch (upgradeType)
                {
                    case UpgradeType.Attack:
                        axe.UpgadeAttack(2f);
                        break;
                    case UpgradeType.Speed:
                        axe.UpgadeSpeed();
                        break;
                    case UpgradeType.AmountIncrease:
                        woodDrop = new Vector2Int(2, 4);
                        break;
                }
                break;
            case ToolType.Pickaxe:
                switch (upgradeType)
                {
                    case UpgradeType.Attack:
                        pickaxe.UpgadeAttack(2f);
                        break;
                    case UpgradeType.Speed:
                        pickaxe.UpgadeSpeed();
                        break;
                    case UpgradeType.AmountIncrease:
                        isPickaxeUpgraded = true;
                        break;
                }
                break;
            case ToolType.Hammer:
                if(upgradeType == UpgradeType.ReturnResource) {
                    hammer.isUpgraded = true;
                }
                break;
        }
    }
    public void ChangeResource(int amount, ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood:
                amountOfResources[ResourceType.Wood] += amount;
                woodText.text = amountOfResources[ResourceType.Wood].ToString();
                break;
            case ResourceType.Rock:
                amountOfResources[ResourceType.Rock] += amount;
                rocksText.text = amountOfResources[ResourceType.Rock].ToString();
                break;
            case ResourceType.Money:
                amountOfResources[ResourceType.Money] += amount;
                moneyText.text = amountOfResources[ResourceType.Money].ToString();
                break;
            case ResourceType.Planks:
                amountOfResources[ResourceType.Planks] += amount;
                planksText.text = amountOfResources[ResourceType.Planks].ToString();
                break;
            case ResourceType.Nails:
                amountOfResources[ResourceType.Nails] += amount;
                nailsText.text = amountOfResources[ResourceType.Nails].ToString();
                break;
            case ResourceType.Pokeball:
                amountOfResources[ResourceType.Pokeball] += amount;
                if (amountOfResources[ResourceType.Pokeball] == 0)
                {
                    pokeballText.text = "";
                    pokeball.SetActive(false);
                }
                else
                {
                    pokeball.SetActive(true);
                    pokeballText.text = amountOfResources[ResourceType.Pokeball].ToString();
                }
                break;
            case ResourceType.Ore:
                amountOfResources[ResourceType.Ore] += amount;
                oreText.text = amountOfResources[ResourceType.Ore].ToString();
                break;
        }
        if (amount > 0)
            resourceCollected[type] += amount;
    }
    public bool CheckAmount(int neededAmount, ResourceType type)
    {
        if(neededAmount <= amountOfResources[type])
            return true;
        return false;
    }
    //Проверить можно ли активировать кнопки для постройки здания в режиме строителя
    public void CheckForEveryBuildingIcons()
    {
        foreach (var buildingIcon in buildingIcons)
        {
            buildingIcon.CheckNeededAmount();
        }
    }
    public void CheckForEveryUpgradeIcons()
    {
        foreach (var upgradeIcon in upgradeIcons)
        {
            upgradeIcon.CheckAmount();
        }
    }

    public void ChangeSpawnersResAmount(int amount, int churchesIncrease)
    {
        foreach (var spawner in spawners)
        {
            spawner.resSpawns += amount;
        }
        churchCount += churchesIncrease;
        churchCountText.text = churchCount + "/3";
        CheckForEveryBuildingIcons();
    }

    public DeathScreen win;
    public NarrowManager narrowManager;
    public GameObject player;
    public ToolSelectSystem toolSelectSystem;

    public void Win()
    {
        win.Appear();
        //Страшно. Не смотрите. Мне уже лень думоть
        win.SetInfo(narrowManager.secPassed, resourceCollected[ResourceType.Wood],
            resourceCollected[ResourceType.Rock], resourceCollected[ResourceType.Planks],
           resourceCollected[ResourceType.Money], resourceCollected[ResourceType.Ore],
            resourceCollected[ResourceType.Nails], buildingCreated);
        player.SetActive(false);
        toolSelectSystem.SelectTool(ToolType.None);
    }

}
public enum ResourceType
{
    Wood,
    Rock,
    Money,
    Planks,
    Nails,
    Ore,
    Pokeball
}