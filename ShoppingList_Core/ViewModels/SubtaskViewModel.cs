using System.Windows.Input;
using ShoppingList_Core.Models;
using ShoppingList_Core.Services;

namespace ShoppingList_Core.ViewModels
{
    public class SubtaskViewModel : ObservableObject
    {
        private Subtask _subtask;
        private DatabaseService _db;
        private MainViewModel _main;

        public ICommand DeleteSubtaskCommand { get; set; }

        public SubtaskViewModel(Subtask subtask, DatabaseService db, MainViewModel main)
        {
            _subtask = subtask;
            _db = db;
            _main = main;
            DeleteSubtaskCommand = new Command(DeleteSubtask);
        }

        public int Id
        {
            get { return _subtask.Id; }
        }

        public string Title
        {
            get { return _subtask.Title; }
        }

        public bool IsDone
        {
            get { return _subtask.IsDone; }
            set
            {
                _subtask.IsDone = value;
                OnPropertyChanged();
                _db.UpdateSubtask(_subtask);
            }
        }

        private void DeleteSubtask()
        {
            _db.DeleteSubtask(_subtask.Id);
            _main.ReloadTasks();
        }
    }
}