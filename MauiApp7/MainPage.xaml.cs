using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp7
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private bool _isCompleted;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class TaskViewModel : INotifyPropertyChanged
    {
        private string _taskTitle = string.Empty;
        private int _taskCount;

        public string TaskTitle
        {
            get => _taskTitle;
            set { _taskTitle = value; OnPropertyChanged(); }
        }

        public int TaskCount
        {
            get => _taskCount;
            set { _taskCount = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public ICommand AddTaskCommand { get; }

        public TaskViewModel()
        {
            AddTaskCommand = new Command(() =>
            {
                if (!string.IsNullOrWhiteSpace(TaskTitle))
                {
                    Tasks.Add(new TaskItem { Title = TaskTitle });
                    TaskTitle = string.Empty;
                    TaskCount = Tasks.Count;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new TaskViewModel();
        }
    }
}