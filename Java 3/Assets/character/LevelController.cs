using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;
    public int maxLevel = 6;
    private Enemy[] _enemies;
    private bool _gameEnded = false;
    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameEnded) return;

        foreach (Enemy enemy in _enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }

        // Check if we've reached the maximum level
        if (_nextLevelIndex < maxLevel)
        {
            _nextLevelIndex++;
            string nextLevelName = "Level" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.Log("Congrats you reached the final level!");
            _gameEnded = true;
        }
    }
}