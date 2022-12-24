using ExpenseTracker.Data;
using ExpenseTracker.Repositories;
using FastCrud.Persistence.EFCore.Data;
using FastFinance.Data;
using FastFinance.FastCrud.EFCore.Repositories;
using FastFinance.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ETDbContext>(options =>
{
    options.UseInMemoryDatabase("ET");
});

builder.Services
    .AddScoped<BaseDbContext, ETDbContext>()
    .AddScoped<IAccountRepository, AccountRepository>()
    .AddScoped<TransactionRepository>()
    .AddScoped<IAccountRepositoryFactory, AccountRepositoryFactory>()
    .AddScoped<ITransactionRepositoryFactory, TransactionRepositoryFactory>()
    .AddScoped<IChartOfAccounts, ChartOfAccounts>()
    .AddScoped<FastFinance.Data.Journal, ExpenseTracker.Data.Journal>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
