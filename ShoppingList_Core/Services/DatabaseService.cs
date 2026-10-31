using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using ShoppingList_Core.Models;

namespace ShoppingList_Core.Services
{
    public class DatabaseService
    {
        private SqliteConnection _connection;

        public DatabaseService(string connectionString)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            CreateTables();
        }

        private void CreateTables()
        {
            _connection.Execute(
                "CREATE TABLE IF NOT EXISTS Tasks (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Title TEXT NOT NULL, " +
                "IsDone INTEGER NOT NULL DEFAULT 0, " +
                "CategoryId INTEGER NOT NULL DEFAULT 3)");

            _connection.Execute(
                "CREATE TABLE IF NOT EXISTS Subtasks (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "TaskId INTEGER NOT NULL, " +
                "Title TEXT NOT NULL, " +
                "IsDone INTEGER NOT NULL DEFAULT 0)");
        }

        public void AddTask(string title, int categoryId)
        {
            _connection.Execute(
                "INSERT INTO Tasks (Title, CategoryId) " +
                "VALUES (@title, @categoryId)",
                new { title = title, categoryId = categoryId });
        }
        public void AddSubtask(int taskId, string title)
        {
            _connection.Execute(
                "INSERT INTO Subtasks (TaskId, Title)" +
                "VALUES (@taskid, @title)",
                new { taskid = taskId, title = title }
                );
        }
        public List<TaskItem> GetTasks()
        {
            List<TaskItem> tasks = _connection.Query<TaskItem>(
                "SELECT Id, Title, IsDone, CategoryId FROM Tasks").ToList();

            List<Subtask> subtasks = _connection.Query<Subtask>(
                "SELECT Id, TaskId, Title, IsDone FROM Subtasks").ToList();

            foreach (Subtask subtask in subtasks)
            {
                foreach (TaskItem task in tasks)
                {
                    if (task.Id == subtask.TaskId)
                    {
                        task.Subtasks.Add(subtask);
                    }
                }
            }

            return tasks;
        }
        public void DeleteTask(int taskId)
        {
            _connection.Execute(
                "DELETE FROM Subtasks WHERE TaskId = @taskId",
                new { taskId = taskId });

            _connection.Execute(
                "DELETE FROM Tasks WHERE Id = @taskId",
                new { taskId = taskId });
        }
        public void UpdateTask(TaskItem task)
        {
            _connection.Execute(
                "UPDATE Tasks SET Title = @title, IsDone = @isDone, CategoryId = @categoryId " +
                "WHERE Id = @id",
                new { title = task.Title, isDone = task.IsDone, categoryId = task.CategoryId, id = task.Id });
        }
        public void DeleteSubtask(int subtaskId)
        {
            _connection.Execute(
                "DELETE FROM Subtasks WHERE Id = @subtaskId",
                new { subtaskId = subtaskId });
        }
        public void UpdateSubtask(Subtask subtask)
        {
            _connection.Execute(
                "UPDATE Subtasks SET Title = @title, IsDone = @isDone " +
                "WHERE Id = @id",
                new { title = subtask.Title, isDone = subtask.IsDone, id = subtask.Id });
        }
    }

}
