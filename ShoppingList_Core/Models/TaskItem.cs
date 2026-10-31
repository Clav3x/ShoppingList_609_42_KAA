using System.Collections.Generic;
namespace ShoppingList_Core.Models
{
	public class TaskItem
	{
		public int Id { get; set;}
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int CategoryId { get; set; }
        public List<Subtask> Subtasks { get; set; }

		public TaskItem()
		{
			Title = "";
			Subtasks = new List<Subtask>();
		}
	}
}

