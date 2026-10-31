using System.Collections.Generic;
namespace ShoppingList_Core.Models
{
	public class Subtask
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int TaskId { get; set; }

        public Subtask()
		{
            Title = "";
        }
	}
}

