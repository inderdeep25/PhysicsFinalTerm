using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasHandler : MonoBehaviour 
{

    [SerializeField] private Text _gameResultText;
    [SerializeField] private Button _playAgainButton;

    public void SetResultText(string text)
    {
        _gameResultText.text = text;
    }

    void OnPlayAgainClicked()
    {
        SceneManager.LoadScene(0);
    }

    // Use this for initialization
    void Start () 
    {
        _playAgainButton.onClick.AddListener(OnPlayAgainClicked);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

}
