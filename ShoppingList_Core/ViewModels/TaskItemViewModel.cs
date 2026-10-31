using System.Collections.Generic;
using System.Windows.Input;
using ShoppingList_Core.Models;
using ShoppingList_Core.Services;

namespace ShoppingList_Core.ViewModels
{
    public class TaskItemViewModel : ObservableObject
    {
        private TaskItem _task;
        private DatabaseService _db;
        private MainViewModel _main;

        public ICommand DeleteTaskCommand { get; set; }
        public ICommand AddSubtaskCommand { get; set; }

        public List<SubtaskViewModel> Subtasks { get; set; }

        public TaskItemViewModel(TaskItem task, DatabaseService db, MainViewModel main)
        {
            _task = task;
            _db = db;
            _main = main;

            NewSubtaskTitle = "";
            DeleteTaskCommand = new Command(DeleteTask);
            AddSubtaskCommand = new Command(AddSubtask);

            Subtasks = new List<SubtaskViewModel>();
            foreach (Subtask subtask in task.Subtasks)
            {
                SubtaskViewModel viewModel = new SubtaskViewModel(subtask, db, main);
                Subtasks.Add(viewModel);
            }
        }

        public int Id
        {
            get { return _task.Id; }
        }

        public string Title
        {
            get { return _task.Title; }
        }

        public bool IsDone
        {
            get { return _task.IsDone; }
            set
            {
                _task.IsDone = value;
                OnPropertyChanged();
                _db.UpdateTask(_task);
            }
        }

        public string Color
        {
            get
            {
                foreach (Category category in Categories.All)
                {
                    if (category.Id == _task.CategoryId)
                    {
                        return category.Color;
                    }
                }
                return "#9E9E9E";
            }
        }

        public string CategoryName
        {
            get
            {
                foreach (Category category in Categories.All)
                {
                    if (category.Id == _task.CategoryId)
                    {
                        return category.Name;
                    }
                }
                return "Другое";
            }
        }

        private string _newSubtaskTitle;
        public string NewSubtaskTitle
        {
            get { return _newSubtaskTitle; }
            set
            {
                _newSubtaskTitle = value;
                OnPropertyChanged();
            }
        }

        private void DeleteTask()
        {
            _db.DeleteTask(_task.Id);
            _main.ReloadTasks();
        }

        private void AddSubtask()
        {
            if (NewSubtaskTitle != null && NewSubtaskTitle != "")
            {
                _db.AddSubtask(_task.Id, NewSubtaskTitle);
                NewSubtaskTitle = "";
                _main.ReloadTasks();
            }
        }
    }
}