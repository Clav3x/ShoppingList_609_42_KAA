using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingList_Core.Models;
using ShoppingList_Core.Services;
using System.Collections.Generic;

namespace ShoppingList_Tests
{
    [TestClass]
    public class DatabaseServiceTests
    {
        [TestMethod]
        public void Список_ДолженСодержатьОднуЗадачу_ПослеДобавления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Молоко", 1);

            List<TaskItem> tasks = db.GetTasks();

            Assert.AreEqual(1, tasks.Count);
        }
        [TestMethod]
        public void Заголовок_ДолженСохраниться_ПослеДобавления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Хлеб", 2);

            List<TaskItem> tasks = db.GetTasks();

            Assert.AreEqual("Хлеб", tasks[0].Title);
        }
        [TestMethod]
        public void Список_ДолженСодержатьДвеЗадачи_ПослеДобавления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Молоко", 1);
            db.AddTask("Хлеб", 2);

            List<TaskItem> tasks = db.GetTasks();

            Assert.AreEqual(2, tasks.Count);
        }
        [TestMethod]
        public void Список_ДолженСодержатьНольЗадач_ПослеУдаления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Молоко", 1);
            

            List<TaskItem> tasks = db.GetTasks();
            db.DeleteTask(tasks[0].Id);
            tasks = db.GetTasks();
            Assert.AreEqual(0, tasks.Count);
        }
        [TestMethod]
        public void Список_ДолженСодержатьОднуПодзадачу_ПослеДобавления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Молоко", 1);

            List<TaskItem> tasks = db.GetTasks();
            tasks[0].IsDone = true;
            db.AddSubtask(tasks[0].Id, "Медвежье");

            tasks = db.GetTasks();
            Assert.AreEqual(1, tasks[0].Subtasks.Count);
        }
        [TestMethod]
        public void Задача_ДолженаСтатьВыполненной_ПослеОбновления()
        {
            DatabaseService db = new DatabaseService("Data Source=:memory:");
            db.AddTask("Молоко", 1);

            List<TaskItem> tasks = db.GetTasks();
            tasks[0].IsDone = true;
            db.UpdateTask(tasks[0]);

            tasks = db.GetTasks();
            Assert.AreEqual(true, tasks[0].IsDone);
        }
    }
}
