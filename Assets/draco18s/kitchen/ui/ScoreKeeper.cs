using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.draco18s.kitchen.ui
{
	public class ScoreKeeper : MonoBehaviour
	{
		public enum GameState
		{
			Menu,InGame,GameOver
		}
		public static ScoreKeeper instance;
		[FormerlySerializedAs("text")]
		public TextMeshProUGUI scoreText;
		public float GameDuration = 3 * 60;
		public Canvas mainMenu;
		public Canvas scoreScreen;
		public TextMeshProUGUI finalScoreText;
		public Image timerImg;
		public Transform player;

		private int score;
		private float gameTime = 0;
		public GameState state { get; protected set; }

		[UsedImplicitly]
		void Start()
		{
			instance = this;
			state = GameState.Menu;
			ShowScreen();
		}

		public void BeginGame()
		{
			score = 0;
			gameTime = 0;
			state = GameState.InGame;
			player.GetComponent<Player>().Reset();
			ShowScreen();
		}

		private void ShowScreen()
		{
			switch (state)
			{
				case GameState.Menu:
					mainMenu.enabled = true;
					scoreScreen.enabled = false;
					break;
				case GameState.GameOver:
					finalScoreText.text = scoreText.text;
					mainMenu.enabled = false;
					scoreScreen.enabled = true;
					break;
				case GameState.InGame:
					mainMenu.enabled = false;
					scoreScreen.enabled = false;
					break;
			}
		}

		public void UpdateScore(int pts)
		{
			score += pts;
			scoreText.text = score.ToString();
		}

		[UsedImplicitly]
		void Update()
		{
			if (state != GameState.InGame) return;
			gameTime += Time.deltaTime;
			timerImg.fillAmount = gameTime / GameDuration;
			if (gameTime >= GameDuration)
			{
				state = GameState.GameOver;
				ShowScreen();
			}
		}
	}
}
