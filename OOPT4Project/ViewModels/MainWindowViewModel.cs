using Prism.Mvvm;

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
