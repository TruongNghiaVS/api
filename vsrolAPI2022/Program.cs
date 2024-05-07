using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;
using VS.core.API.job;
using VS.Core.Business.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterBusiness();
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("UpdateTrackingCall");
    q.AddJob<UpdateTrackingCall>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("UpdateTrackingCall-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule(" 0/1 * * * * ? *")
    );
});


builder.Services.AddQuartz(q =>
{

    var jobkey = new JobKey("updatedatagroupdb");
    q.AddJob<UpdateDataGroupDB>(opts => opts.WithIdentity(jobkey));

    q.AddTrigger(opts => opts
        .ForJob(jobkey)
        .WithIdentity("updatedatagroupdb-trigger")
        .WithCronSchedule(" 0/15 * * * * ? *")
    );
});



builder.Services.AddQuartz(q =>
{

    var jobkey = new JobKey("clearlogcalljob");
    q.AddJob<ClearLogCallJob>(opts => opts.WithIdentity(jobkey));

    q.AddTrigger(opts => opts
        .ForJob(jobkey)
        .WithIdentity("clearlogcalljob-trigger")
        .WithCronSchedule("0 0 20 ? * * *")
    );
});




builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

//var issuer =  builder.Configuration["Jwt:Issuer"];
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
