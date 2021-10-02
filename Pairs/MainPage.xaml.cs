using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using Pairs.ViewModels;

namespace Pairs
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		void MainPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(MainPageViewModel.State) &&
			    ((MainPageViewModel)BindingContext).State == Models.LevelState.Complete)
			{
				//TrophyAnimation.PlayAnimation();
			}
		}
	}
}
