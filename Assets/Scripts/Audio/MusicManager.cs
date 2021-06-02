using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

	public AudioClip mainTheme;
	public AudioClip menuTheme;

	string Scene_Name = null;

	void Start()
	{
		//씬 이름 저장
		Scene_Name = SceneManager.GetActiveScene().name;

		AudioManager.instance.PlayMusic(menuTheme, 2);
	}

	void Update()
	{
		//씬 이동이 이루어 진 경우
		if (SceneManager.GetActiveScene().name != Scene_Name)
		{
			Scene_Name = SceneManager.GetActiveScene().name;

			if(Scene_Name == "GameScene")
				AudioManager.instance.PlayMusic(mainTheme, 2);
		}
	}
}
