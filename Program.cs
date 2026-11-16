using Microsoft.EntityFrameworkCore;
using EpicerieAlda.Data;
using EpicerieAlda.Dao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Accès à la base de données (EF Core + SQL Server)
builder.Services.AddDbContext<AldaDbContext>(options =>


options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProduitDao, ProduitDao>();
builder.Services.AddScoped<ICommandeDao, CommandeDao>();
builder.Services.AddScoped<IClientDao, ClientDao>();

// ajouter la memoire au car le http n'a pas de mémoire, donc on utilise
// la session pour garder les informations du panier
builder.Services.AddDistributedMemoryCache();   
builder.Services.AddSession();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapStaticAssets();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceuil}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
