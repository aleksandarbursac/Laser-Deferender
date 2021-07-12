using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        scoreTextUI = GetComponent<TMPro.TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextUI.text = "HP: " + player.ReturnHealth().ToString();
    }
}
