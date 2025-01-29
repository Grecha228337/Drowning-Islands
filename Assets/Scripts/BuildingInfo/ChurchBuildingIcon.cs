using UnityEngine.UI;

public class ChurchBuildingIcon : BuildingIcon
{
    public override bool CheckNeededAmount()
    {
        if(GameManager.current.churchCount == 3)
        {
            GetComponent<Button>().interactable = false;
            return false;
        }
        return base.CheckNeededAmount();
    }
}
