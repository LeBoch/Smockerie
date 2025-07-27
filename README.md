# Smockerie API

Projet ASP.NET Core 8 pour la gestion de produits et de commandes. L'application utilise MySQL via Entity Framework Core et l'authentification JWT.

## Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- MySQL Server
- (optionnel) Outil global `dotnet-ef` pour les migrations

## Installation

```bash
git clone <votre-url-du-repo>
cd Smockerie
```

Copiez `appsettings.json` depuis `appsettings.Development.json` au besoin et modifiez la chaîne `DefaultConnection` pour correspondre à votre instance MySQL. Les paramètres de JWT peuvent également être ajustés.

### Restauration des dépendances

```bash
dotnet restore
```

### Création de la base de données

Installez l'outil `dotnet-ef` si nécessaire :

```bash
dotnet tool install --global dotnet-ef
```

Puis appliquez les migrations :

```bash
dotnet ef database update
```

### Lancement de l'API

```bash
dotnet run
```

L'API est alors accessible sur `https://localhost:7120` (voir `Properties/launchSettings.json`). L'interface Swagger est disponible sur `/swagger`.

## Développement

Pour bénéficier du rechargement à chaud :

```bash
dotnet watch run
```

## Structure du projet

- `Controllers` – Points d'entrée de l'API
- `Services` – Logique métier
- `Data` – `DbContext` Entity Framework
- `Models` – Modèles de données
- `DTO` – Objets de transfert
- `Migrations` – Historique des migrations
