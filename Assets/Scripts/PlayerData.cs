using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData : ScriptableObject
{
    public int HP = 3; // Default HP value
    public int Points= 0;

    public void DecreaseHP()
    {
        if (HP > 0)
        {
            HP -= 1;
            if (HP <= 0)
            {
                OnHPDepleted();
            }
        }
    }

    public void OnHPDepleted()
    {


        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}