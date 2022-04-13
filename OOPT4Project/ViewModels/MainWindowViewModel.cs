using Prism.Commands;
using Prism.Mvvm;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace OOPT4Project.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		private string _title = "";

		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}

		public MainWindowViewModel()
		{
			Title = "Hexalife";
		}
	}
}
