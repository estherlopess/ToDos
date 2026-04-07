using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Data;
using ToDoPlatform.Models;
using ToDoPlatform.Services;
using ToDoPlatform.ViewModels;

namespace ToDoPlatform.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IUserService _userService;

    public HomeController(AppDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetLoggedUser();

        if (user == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var todos = await _dbContext.ToDos
            .AsNoTracking()
            .Where(t => t.UserId == user.Id)
            .ToListAsync();

        var openTodos = todos
            .Where(t => !t.Done)
            .OrderByDescending(t => t.CreatedAt)
            .ThenBy(t => t.Title)
            .ToList();

        HomeVM homeVM = new()
        {
            TotalTasks = todos.Count,
            OpenTasks = openTodos.Count,
            EndedTasks = todos.Count(t => t.Done),
            ToDos = openTodos
        };

        return View(homeVM);
    }

    [HttpPost]
    public async Task<IActionResult> EndTask(int? id)
    {
        if (!id.HasValue)
        {
            return Json(new
            {
                success = false,
                message = "Problemas ao carregar a tarefa!"
            });
        }

        var user = await _userService.GetLoggedUser();

        if (user == null)
        {
            return Json(new
            {
                success = false,
                message = "Sua sessão expirou!",
                redirect = true
            });
        }

        var todo = await _dbContext.ToDos
            .Where(t => t.Id == id && t.UserId == user.Id)
            .SingleOrDefaultAsync();

        if (todo == null)
        {
            return Json(new
            {
                success = false,
                message = "Tarefa não encontrada ou sem permissão!"
            });
        }

        try
        {
            todo.Done = true;
            await _dbContext.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Tarefa finalizada com sucesso!"
            });
        }
        catch
        {
            return Json(new
            {
                success = false,
                message = "Erro ao finalizar tarefa."
            });
        }  
    }
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   
     public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
};