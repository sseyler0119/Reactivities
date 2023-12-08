using API.Extensions;
using API.Middleware;
using API.SignalR;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (things we use inside of our application logic)

builder.Services.AddControllers(opt => /* this will require every single controller endpoint to require authentication*/
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline. 
// aka Middleware: i.e. things that can do something with the http request on its way in or out
app.UseMiddleware<ExceptionMiddleware>();

app.UseXContentTypeOptions(); // prevents mine-sniffing of content type
app.UseReferrerPolicy(opt => opt.NoReferrer());
app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
app.UseXfo(opt => opt.Deny()); // prevent application from being used inside iframe to prevent click-jacking
app.UseCsp(opt => opt
    .BlockAllMixedContent()
    .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com"))
    .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com", "data:"))
    .FormActions(s => s.Self())
    .FrameAncestors(s => s.Self())
    .ImageSources(s => s.Self().CustomSources("blob:", "https://res.cloudinary.com"))
    .ScriptSources(s => s.Self())
); // main defense against cross-site scripting attacks by white-listing which content is allowed to be served


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
    app.Use(async (context, next) => 
    {
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
        await next.Invoke();
    });
}

app.UseCors("CorsPolicy");

app.UseAuthentication(); // useAuthentication() must come before UseAuthorization()
app.UseAuthorization();

app.UseDefaultFiles(); // look in wwwroot files for index.html file
app.UseStaticFiles(); // look in wwwrooot file for static content

app.MapControllers();
app.MapHub<ChatHub>("/chat"); 
app.MapFallbackToController("Index", "Fallback"); // use Index method in Fallback controller to serve up Index.html page

/* using will automatically clean up the memory allocated for the variable when we're done using it*/
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    // apply any pending migrations for the context to the db, will create the db if it does not already exist
    await context.Database.MigrateAsync(); // change this to await and use MigrateAsync instead of Migrate
    await Seed.SeedData(context, userManager); // SeedData is an async function, so it requires await
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
