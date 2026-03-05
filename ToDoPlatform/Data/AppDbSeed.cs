namespace ToDoPlatform.Data;

    public class AppDbSeed
    {
        public AppDbSeed(ModelBuilder builder)
        {
            #region Popular Perfis de usuarios
            List<IdentityRole> roles= new()
            {
                new()
                {
                    Id="2eaf96b7-9e52-47c6-a66b-4124f04f9855",
                    Name= "Administrador",
                    NormalizedName="ADMINISTRADOR"
                }
                 new()
                {
                    Id="5da8f6f4-eae6-4d24-ab1f-a3adaadf3f51",
                    Name= "Usuários",
                    NormalizedName="USUÁRIO"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
             #endregion

        }
    }
