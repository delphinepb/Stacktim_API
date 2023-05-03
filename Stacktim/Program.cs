using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Stacktim.Model;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    option => option.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2022-01-01")
    })
    );

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder => {
        builder.WithOrigins("http://localhost:4200")

.AllowAnyMethod()
.AllowAnyHeader()
        ;
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build(); app.UseCors(MyAllowSpecificOrigins);

app.UseCors(builder => builder
.AllowAnyOrigin()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  -------------   CRUD CONNAISSANCE    -----------------------------
//read connaissance
app.MapGet("/GetConnaissancebyNom/{nom}", (string nom) =>
{
    var oConnaissance = new ConnaissanceRepo(builder.Configuration).GetConnaissancebyNom(nom);
    return oConnaissance;
}).WithTags("Connaissances");

app.MapGet("/GetConnaissanceById/{id:int}", (int id) =>
{
    var connaissance = new ConnaissanceRepo(builder.Configuration).GetConnaissanceById(id);
    return connaissance;
}).WithTags("Connaissances");

//read all connaissance
app.MapGet("/Getallconnaissance", () => {
    var oListe = new ConnaissanceRepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Connaissances");

//  update
app.MapPost("/Updateconnaissance", (ConnaissanceEntity connaissance) => {
    var ok = new ConnaissanceRepo(builder.Configuration).Update(connaissance);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
}).WithTags("Connaissances");

//delete
app.MapDelete("/Deleteconnaissance/{id:int}", (int id) => {
    var ok = new ConnaissanceRepo(builder.Configuration).Delete(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete n'a pas marché", Status = 500 });
}).WithTags("Connaissances");

//create
app.MapPut("/Createconnaissance", (ConnaissanceEntity connaissance) =>
{
    var ok = new ConnaissanceRepo(builder.Configuration).Insert(connaissance);
    return (ok != -1) ? Results.Created($"/{ok}", connaissance.idConnaissance = ok) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });
}).WithTags("Connaissances");

//  -------------   CRUD PROJET    -----------------------------
//read PROJET
app.MapGet("/GetProjetById/{id:int}", (int id) =>
{
    var projet = new ProjetRepo(builder.Configuration).GetProjetById(id);
    return projet;
}).WithTags("Projets");

//read all projet
app.MapGet("/GetallProjet", () => {
    var oListe = new ProjetRepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Projets");

//  update projet
app.MapPost("/UpdateProjet", (ProjetEntity projet) => {
    var ok = new ProjetRepo(builder.Configuration).Update(projet);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
}).WithTags("Projets");

//  delete projet
app.MapDelete("/DeleteProjet/{id:int}", (int id) => {
    var ok = new ProjetRepo(builder.Configuration).Delete(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete n'a pas marché", Status = 500 });
}).WithTags("Projets");

//  create projet
app.MapPut("/CreateProjet", (ProjetEntity projet) =>
{
    var ok = new ProjetRepo(builder.Configuration).Insert(projet);
    return (ok != -1) ? Results.Created($"/{ok}", projet.idProjet = ok) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });
}).WithTags("Projets");

//  -------------   CRUD RESSOURCES    -----------------------------
// read ressource
app.MapGet("/GetRessourcebyId/{id:int}", (int id) =>
{
    var ressource = new RessourceRepo(builder.Configuration).GetRessById(id);
    return ressource;
}).WithTags("Ressources");

//read all ressource
app.MapGet("/GetallRessource", () => {
    var oListe = new RessourceRepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Ressources");

//get ressources by idConnaissance
app.MapGet("/GetRessbyCo/{id:int}", (int id) => {
    var oListe = new RessourceRepo(builder.Configuration).GetRessByIdConnaissance(id);
    return oListe;
}).WithTags("Ressources");

//  update ressource
app.MapPost("/UpdateRessource", (RessourceEntity ressource) => {
    var ok = new RessourceRepo(builder.Configuration).Update(ressource);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
}).WithTags("Ressources");

//delete ressource
app.MapDelete("/DeleteRessource/{id:int}", (int id) =>
{
    var ok = new RessourceRepo(builder.Configuration).Delete(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete n'a pas marché", Status = 500 });
}).WithTags("Ressources");

//create ressource
app.MapPut("/CreateRessource", (RessourceEntity ressource) =>
{
    var ok = new RessourceRepo(builder.Configuration).Insert(ressource);
    return (ok != -1) ? Results.Created($"/{ok}", ressource.idRessource = ok) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });
}).WithTags("Ressources");


//  -------------   CRUD TYPE RESSOURCES    -----------------------------
//read typeressource
app.MapGet("/GettypeRById/{id:int}", (int id) =>
{
    var oTyper = new TypeRrepo(builder.Configuration).GetTypeRById(id);
    return oTyper;
}).WithTags("Type Ressources");

//read all typeRessource
app.MapGet("/GetallTypeR", () => {
    var oListe = new TypeRrepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Type Ressources");

//  update typeR
app.MapPost("/UpdateTypeR", (TypeREntity type) => {
    var ok = new TypeRrepo(builder.Configuration).Update(type);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
}).WithTags("Type Ressources");

//delete typeR
app.MapDelete("/DeleteTypeR/{id:int}", (int id) => {
    var ok = new TypeRrepo(builder.Configuration).Delete(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete n'a pas marché", Status = 500 });
}).WithTags("Type Ressources");

//create TypeR
app.MapPut("/CreateTypeR", (TypeREntity typeR) =>
{
    var ok = new TypeRrepo(builder.Configuration).Insert(typeR);
    return (ok != -1) ? Results.Created($"/{ok}", typeR.idTypeR = ok) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });
}).WithTags("Type Ressources");

//  -------------   CRUD CATEGORIE    -----------------------------
//read categorie
app.MapGet("/GetCategoriebyNom/{nom}", (string nom) =>
{
    var oCategorie = new CategorieRepo(builder.Configuration).GetCategorieByNom(nom);
    return oCategorie;
}).WithTags("Categorie");

//read all categorie
app.MapGet("/Getallcategorie", () => {
    var oListe = new CategorieRepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Categorie");
//  update
app.MapPost("/Updatecategorie", (CategorieEntity categorie) => {
    var ok = new CategorieRepo(builder.Configuration).Update(categorie);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "L'update n'a pas marché", Status = 500 });
}).WithTags("Categorie");

//delete
app.MapDelete("/Deletecategorie/{id:int}", (int id) => {
    var ok = new CategorieRepo(builder.Configuration).Delete(id);
    return ok ? Results.NoContent() : Results.Problem(new ProblemDetails { Detail = "Le delete n'a pas marché", Status = 500 });
}).WithTags("Categorie");

//create
app.MapPut("/Createcategorie", (CategorieEntity categorie) =>
{
    var ok = new CategorieRepo(builder.Configuration).Insert(categorie);
    return (ok != -1) ? Results.Created($"/{ok}", categorie.idCategorie = ok) : Results.Problem(new ProblemDetails { Detail = "L'insert n'a pas marché", Status = 500 });
}).WithTags("Categorie");

//  -------------   CRUD STATUTPROJET    -----------------------------
//read all
app.MapGet("/GetallStatut", () => {
    var oListe = new StatutProjetRepo(builder.Configuration).Getall();
    return oListe;
}).WithTags("Statuts");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

