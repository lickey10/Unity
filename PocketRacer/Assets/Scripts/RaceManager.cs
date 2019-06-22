using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public int LapsPerRace = 3;
    public Text[] PlacesText;
    public Car[] allCars;
    public Car[] carOrder;

    public void Start()
    {
        // set up the car objects
        carOrder = new Car[allCars.Length];
        InvokeRepeating("ManualUpdate", 0.5f, 0.5f);
    }

    // this gets called every frame
    public void ManualUpdate()
    {
        foreach (Car car in allCars)
        {
            if(car.enabled)
                carOrder[car.GetCarPosition(allCars) - 1] = car;
        }

        //display results
        for(int x = 0; x < carOrder.Length; x++)
        {
            if (PlacesText[x] != null && carOrder[x] != null)
            {
                if(carOrder[x].isActiveAndEnabled)
                    PlacesText[x].text = (x + 1) + ": " + carOrder[x].name;
                else
                    PlacesText[x].text = "DNF: " + carOrder[x].name;
            }
        }

        //stop car if LapsPerRace completed
        try
        {
            var cars = carOrder.Where(x => x != null && !x.Stopped && x.currentLap > LapsPerRace).ToList();

            if (cars.Count > 0)
                cars.ForEach(y => y.StopCar());

            //check if race is over
            cars = carOrder.Where(x => x != null && !x.Stopped && x.isActiveAndEnabled).ToList();

            if(cars.Count == 0)//the race is over
            {
                Debug.Log("Race is over");
            }
        }
        catch (System.Exception ex)
        {

            throw;
        }
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        StartCoroutine(PauseGameWithDelay(0));
    }

    public void PauseGame(float PauseDelay)
    {
        StartCoroutine(PauseGameWithDelay(PauseDelay));
    }

    private IEnumerator PauseGameWithDelay(float PauseDelay)
    {
        yield return new WaitForSeconds(PauseDelay);

        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }
}
