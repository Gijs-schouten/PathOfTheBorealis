using UnityEngine;

public class CamPostProcessing : MonoBehaviour
{
    public static CamPostProcessing Instance;
    private PlayerStatemachine Player;

    private void Awake()
    {
        Instance = this;

        Player = transform.root.GetComponent<PlayerStatemachine>();
    }


    public void EnterWater()
    {
        
    }
    public void ExitWater()
    {
        
    }
}
