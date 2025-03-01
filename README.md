README - Packages des APIs

Introduction

Ce fichier liste les packages actuellement utilisés dans les deux APIs (principale et secondaire). Cette liste peut être mise à jour en fonction des besoins futurs.

API Principale

Packages installés

- AspNetCoreRateLimit (5.0.0)
- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
- Hangfire (1.8.0)
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.2)
- Microsoft.AspNetCore.Cors (2.3.0)
- Microsoft.AspNetCore.OpenApi (8.0.13)
- Microsoft.AspNetCore.SignalR (1.2.0)
- Microsoft.AspNetCore.SignalR.Client (9.0.2)
- Microsoft.EntityFrameworkCore (9.0.2)
- Microsoft.EntityFrameworkCore.Design (9.0.2)
- Microsoft.EntityFrameworkCore.InMemory (9.0.2)
- Microsoft.EntityFrameworkCore.Proxies (9.0.2)
- Microsoft.EntityFrameworkCore.Tools (9.0.2)
- Microsoft.IdentityModel.Tokens (8.6.0)
- Newtonsoft.Json (13.0.3)
- Serilog.AspNetCore (7.0.0)
- Swashbuckle.AspNetCore (6.6.2)

API Secondaire

Packages installés

- AspNetCoreRateLimit (5.0.0)
- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
- Hangfire (1.8.0)
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.2)
- Microsoft.AspNetCore.Cors (2.3.0)
- Microsoft.AspNetCore.OpenApi (8.0.13)
- Microsoft.AspNetCore.SignalR (1.2.0)
- Microsoft.AspNetCore.SignalR.Client (9.0.2)
- Microsoft.EntityFrameworkCore (9.0.2)
- Microsoft.EntityFrameworkCore.Design (9.0.2)
- Microsoft.EntityFrameworkCore.InMemory (9.0.2)
- Microsoft.EntityFrameworkCore.Proxies (9.0.2)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.2)
- Microsoft.EntityFrameworkCore.Tools (9.0.2)
- Microsoft.IdentityModel.Tokens (8.6.0)
- Microsoft.ML (4.0.2)
- Newtonsoft.Json (13.0.3)
- Serilog.AspNetCore (7.0.0)
- Swashbuckle.AspNetCore (6.6.2)

Packages potentiels à ajouter dans le futur

Ces packages ne sont pas encore installés mais pourraient être nécessaires selon les évolutions des APIs :

- FluentValidation.AspNetCore
- MassTransit
- Polly
- Ocelot
- Dapper

Instructions d'installation

Pour installer tous les packages nécessaires, exécutez la commande suivante dans le terminal du projet :

# Pour l'API principale
dotnet add package <nom_du_package>

# Pour l'API secondaire
cd API_Secondaire
 dotnet add package <nom_du_package>

Remplacez <nom_du_package> par le nom du package souhaité.

Mise à jour des packages

Pour mettre à jour tous les packages vers leur dernière version compatible, exécutez :

# Mise à jour de tous les packages
 dotnet outdated
 dotnet add package <nom_du_package> --version <dernière_version>

Contact

Pour toute suggestion ou mise à jour, envoyez moi sur discord ou insta
