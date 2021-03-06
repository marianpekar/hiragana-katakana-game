using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private const int GameSceneIndex = 1;
    private const int MenuSceneIndex = 0;

    public static void LoadGame() {
        SceneManager.LoadScene(GameSceneIndex);
    }

    public static void LoadMenu() {
        SceneManager.LoadScene(MenuSceneIndex);
    }

}
