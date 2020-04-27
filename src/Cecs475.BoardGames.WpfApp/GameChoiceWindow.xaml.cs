﻿using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.WpfApp {
	/// <summary>
	/// Interaction logic for GameChoiceWindow.xaml
	/// </summary>
	public partial class GameChoiceWindow : Window {
		public GameChoiceWindow() {
			InitializeComponent();

			Type iWpfGameFactory = typeof(IWpfGameFactory);

			Assembly current = Assembly.GetExecutingAssembly();
			
			var gamesPath = "../../../../src/Cecs475.BoardGames.WpfApp/bin/Debug/games";

			DirectoryInfo d = new DirectoryInfo(gamesPath);
			foreach(var file in d.GetFiles("*.dll")) {
				//is this how you load every file?
				Assembly tttAssembly = Assembly.LoadFrom(gamesPath + "/"+ file.Name);
			}

			/*var gameTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => iWpfGameFactory.IsAssignableFrom(t) && t.IsClass);*/

			//GetTypes gives an error from above so create an alternate
			foreach(var i in AppDomain.CurrentDomain.GetAssemblies())
			{
				var check = i.GetTypes();
			}


			//is this how to do it?
			//create list, invoke the gameTypes and then add to a list of objects
			/*List<object> gamesList = new List<object>();
			foreach (var games in gameTypes)
			{
				var type = games.GetType();
				var con = type.GetConstructor(Type.EmptyTypes);
				//how to invoke? 
				var invoke = con.Invoke(new object[0]);
				gamesList.Add(invoke);
			}
			IEnumerable<object> IEGamesList = gamesList;
			this.Resources.Add("GameTypes", IEGamesList);

*/


		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			// Retrieve the game type bound to the button
			IWpfGameFactory gameType = b.DataContext as IWpfGameFactory;
			// Construct a GameWindow to play the game.
			var gameWindow = new GameWindow(gameType) {
				Title = gameType.GameName
			};
			// When the GameWindow closes, we want to show this window again.
			gameWindow.Closed += GameWindow_Closed;

			// Show the GameWindow, hide the Choice window.
			gameWindow.Show();
			this.Hide();
		}

		private void GameWindow_Closed(object sender, EventArgs e) {
			this.Show();
		}
	}
}
