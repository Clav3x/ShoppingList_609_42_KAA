using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows.Input;
using ShoppingList_Core.Models;
using ShoppingList_Core.Services;

namespace ShoppingList_Core.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private DatabaseService _db;

        public ObservableCollection<TaskItemViewModel> Tasks { get; set; }
        public List<Category> CategoryList { get; set; }

        private string _newTaskTitle;
        private Category _selectedCategory;
        public ICommand SelectProductsCommand { get; set; }
        public ICommand SelectHomeCommand { get; set; }
        public ICommand SelectOtherCommand { get; set; }

        public string NewTaskTitle
        {
            get { return _newTaskTitle; }
            set
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTaskCommand { get; set; }

        public MainViewModel(DatabaseService db)
        {
            _db = db;

            Tasks = new ObservableCollection<TaskItemViewModel>();
            CategoryList = Categories.All;
            SelectedCategory = CategoryList[0];
            AddTaskCommand = new Command(AddTask);
            ReloadTasks();
            SelectProductsCommand = new Command(SelectProducts);
            SelectHomeCommand = new Command(SelectHome);
            SelectOtherCommand = new Command(SelectOther);
        }

        private void AddTask()
        {
            if (NewTaskTitle != null && NewTaskTitle != "")
            {
                int categoryId = 3;
                if (SelectedCategory != null)
                {
                    categoryId = SelectedCategory.Id;
                }

                _db.AddTask(NewTaskTitle, categoryId);
                NewTaskTitle = "";
                ReloadTasks();
            }
        }

        public void ReloadTasks()
        {
            Tasks.Clear();

            List<TaskItem> tasks = _db.GetTasks();

            foreach (TaskItem task in tasks)
            {
                TaskItemViewModel viewModel = new TaskItemViewModel(task, _db, this);
                Tasks.Add(viewModel);
            }
        }
        private void SelectProducts()
        {
            SelectedCategory = CategoryList[0];
        }

        private void SelectHome()
        {
            SelectedCategory = CategoryList[1];
        }

        private void SelectOther()
        {
            SelectedCategory = CategoryList[2];
        }
    }
}
