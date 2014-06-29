using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character
{
  public class Indian : MonoBehaviour
  {

    public IndianData IndianData { get; private set; }

    private float currentStamina;
    private float maxStamina = 1.0f; // sekunden zum wiederaufbau

    public delegate void StaminaChangedDelegate(float newCurrentStamina);
    public event StaminaChangedDelegate OnStaminaChanged;

    public static Indian AddAsComponent(GameObject gameObjectFromPreafab, IndianData indianData)
    {
      Indian indian = gameObjectFromPreafab.AddComponent<Indian>();
      indian.IndianData = indianData;
      indian.currentStamina = indian.maxStamina;
      return indian;
    }

    void Update()
    {
      RebuildStamina();
    }

    private void RebuildStamina()
    {
      if (currentStamina < maxStamina)
      {
        if (currentStamina < 0.5){
          currentStamina += Time.deltaTime / 6;
        } else {
          currentStamina += Time.deltaTime / 3;
        }
        if (OnStaminaChanged != null)
        {
          OnStaminaChanged(currentStamina);
        }
      }
      else if (currentStamina > maxStamina)
      {
        currentStamina = maxStamina;
        if (OnStaminaChanged != null)
        {
          OnStaminaChanged(currentStamina);
        }
      }
    }

    public bool ShotRequest()
    {
      //Debug.Log("Stamina: " + currentStamina);
      if (1.0f / IndianData.Stamina <= currentStamina)
      {
        currentStamina -= 1.0f / IndianData.Stamina;
        return true;
      }
      else
      {
        currentStamina = 0;
        return false;
      }
    }


  }

}
