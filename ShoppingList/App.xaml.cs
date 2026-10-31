namespace ShoppingList;
using System.IO;
using Microsoft.Maui.Storage;
using ShoppingList_Core.Services;
public partial class App : Application
{
    public static DatabaseService Database { get; set; }

    public App()
    {
        InitializeComponent();

        string path = Path.Combine(FileSystem.AppDataDirectory, "tasks.db");
        Database = new DatabaseService("Data Source=" + path);

        MainPage = new AppShell();
    }
}

