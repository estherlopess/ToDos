// ViewModels/HomeVM.cs
using ToDoPlatform.Models;
namespace ToDoPlatform.ViewModel;
public class HomeVM
{
public int TotalTasks { get; set; }
public int OpenTasks { get; set; }
public int EndedTasks { get; set; }
public List<ToDo> ToDos { get; set; }
}