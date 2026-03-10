using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoPlatform.Models;

namespace ToDoPlatform.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        #region Popular Perfis de usuarios
        List<IdentityRole> roles = new()
            {
                new()
                {
                    Id="2eaf96b7-9e52-47c6-a66b-4124f04f9855",
                    Name= "Administrador",
                    NormalizedName="ADMINISTRADOR"
                },
                 new()
                {
                    Id="5da8f6f4-eae6-4d24-ab1f-a3adaadf3f51",
                    Name= "Usuários",
                    NormalizedName="USUÁRIO"
                }
            };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Popular dados de Usuario
        List<AppUser> users = new()
        {
            new AppUser()
            {
                Id = "8aab3d25-3320-4d46-9407-cb03a9380a4b",
                Email = "estherlopes678@gmail.com",
                NormalizedEmail= "ESTHERLOPES678@GMAIL.COM",
                UserName = "estherlopes678@gmail.com",
                NormalizedUserName= "ESTHERLOPES678@GMAIL.COM",
                LockoutEnabled = false,
                EmailConfirmed= true,
                Name ="Esther Lopes",
                ProfilePicture = "/img/users/Id = 8aab3d25-3320-4d46-9407-cb03a9380a4b.png"
            },
             new AppUser()
            {
                Id = "a5047c93-a48e-47a7-98ed-559f65da4855",
                Email = "larissafantinatti24@gmail.com",
                NormalizedEmail= "LARISSAFANTINATTI24@GMAIL.COM",
                UserName = "larissafantinatti24@gmail.com",
                NormalizedUserName= "LARISSAFANTINATTI24@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed= true,
                Name ="Larissa Fantinatti",
                ProfilePicture = "/img/users/Id = a5047c93-a48e-47a7-98ed-559f65da4855.png"
            },
        };
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<AppUser>().HasData(users);
        #endregion

        #region Popular Dados de Perfil
        List<IdentityUserRole<string>> userRoles = new()
        {
            
            new IdentityUserRole<string>()
            {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>()
            {
                UserId= users[1].Id,
                RoleId = roles[1].Id
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
    #endregion
    
    #region Popular as Tarefas do usuário
    List<ToDo> toDos= new()
    {
        new ToDo()
        {
            Id= 1,
            Title = "seminario da meire",
            Description ="fazer os slides o resto",
            UserId =users[1].Id
        },
          new ToDo()
        {
            Id= 2,
            Title = "Tomar mais agua",
            Description ="precisamos tomar mais agua ",
            UserId =users[1].Id
        },
          new ToDo()
        {
            Id= 3,
            Title = "ir embora da escola",
            Description ="Temos que ir para a casinha",
            UserId =users[1].Id
        },
    };
    builder.Entity<ToDo>().HasData(toDos);
    #endregion
    }

}
