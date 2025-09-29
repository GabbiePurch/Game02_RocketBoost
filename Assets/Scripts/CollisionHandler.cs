using UnityEngine;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameText;
    int fuel = 0;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything Looks Good");
                break;
            case "Finish":
                Debug.Log("You're all done, welcome to our country");
                break;
            case "Fuel":
                fuel = fuel + 5;
                gameText.text = "Fuel: " + fuel;
                Destroy(other.gameObject);
                break;
            default:
                Debug.Log("You crashed dummy!");
                break;
        }
    }
}
