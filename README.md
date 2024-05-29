# Cube API : Stock Management

## Description

This API was created to manage the wine stock of a fictive company during a school project. 

It is consumed by :

- A Symfony web application for the e-commerce part
- A .NET WPF desktop application for the management of the stock

## Installation

### Pre-requisites

- Dotnet SDK >= 7.0
- Entity Framework Core CLI
- MariaDB
- Docker
- Make

### Installation

#### Make

##### Windows

- Download and install [Chocolatey](https://chocolatey.org/install)
- Open a terminal as administrator
- Run `choco install make` in the terminal
- Restart the terminal
- Check the installation with the command `make --version`

##### MacOS

- Open a terminal
- Run `xcode-select --install` 
- Download and install [Homebrew](https://brew.sh/index_fr)
- Run `brew install make`
- Check the installation with the command `make --version`

#### Dotnet SDK

##### Linux (Debian)

```bash
make install-dotnet-on-debian
```

##### MacOS

```bash
make install-dotnet-on-mac
```

#### Entity Framework Core CLI

```bash
make install-ef
```

#### MariaDB (Sans Docker)

##### Linux (Debian)

```bash
make install-mariadb-on-debian
```

##### MacOS

```bash
make install-mariadb-on-mac
```

#### MariaDB (Dev: utilisation du docker-compose.yml)

```bash
docker compose up -d
```

#### Docker

##### Linux (Debian)

```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
```

Check official documentation : [Docker](https://docs.docker.com/engine/install/debian/)

##### MacOS

```bash
brew install docker
```

Or check official documentation [Docker](https://docs.docker.com/docker-for-mac/install/)

### API installation

#### Clone the repository

```bash
git clone git@github.com:LCE-CESI/ApiCube.git
```

#### Create the database

```bash
make db-create
```

#### Run the migrations

```bash
make db-migrate
```

#### Run the API

```bash
make run
```

#### Update the database

```bash
make db-update
```

#### Reset the database

```bash
make db-reset
```

#### Drop the database

```bash
make db-drop
```

## Development

### Domain Enums

Enums used in the API for status and roles.

```csharp
public enum Role
{
    Admin,
    Employe,
    Manager,
    Client
}
```

```csharp
public enum StatutCommande
{
    EnCours,
    Livree,
    Annulee,
    Autre
}
```

```csharp
public enum StatutFacture
{
    EnCours,
    Payee,
    Annulee,
    Autre
}
```

```csharp
public enum StatutStock
{
    EnStock,
    EnRuptureDeStock,
    Indisponible,
    QuasimentEpuise,
    EnCommande,
    EnCoursDeLivraison,
    Livre,
    Perime,
    Retourne,
    Vendu,
    Perdu,
    Vole,
    Casse,
    Supprime,
    Autre
}
```

```csharp
public enum TypeTransactionStock
{
    Achat,
    Approvisionnement,
    AjoutInterne,
    Vente,
    Retour,
    Perte,
    Vol,
    Peremption,
    ModificationInterne,
    Suppression
}
```

### Fixtures

The API use seeders [Bogus](https://github.com/bchavez/Bogus) to generate fake data for the database.
They are loaded at the start of the API (see Persistence/Seeders).

#### Clients accounts

```csharp
var client = new ClientModel
{
    Nom = "Gagnant",
    Prenom = "Michel",
    Email = "michel@mail.fr",
    Adresse = "12, rue des fleurs",
    CodePostal = "75000",
    Ville = "Paris",
    Pays = "France",
    Telephone = "0123456789",
    DateNaissance = new DateTime(1980, 1, 1),
    DateInscription = DateTime.Now,
    ApplicationUserId = userId1
};

var passwordClient = "Doudou58!";
```

#### Employes accounts

```csharp
var employe1 = new EmployeModel
{
    Nom = "Admin",
    Prenom = "Admin",
    Email = "admin@gmail.com",
    DateEmbauche = DateTime.Now,
    Poste = "Responsable",
    ApplicationUserId = userId1
};

var employe2 = new EmployeModel
{
    Nom = "Employe",
    Prenom = "Employe",
    Email = "employe@gmail.com",
    DateEmbauche = DateTime.Now,
    Poste = "Saisonnier",
    ApplicationUserId = userId2
};

var passwordAdmin = "Admin123!";
var passwordSaisonnier = "Client123!";
```

### API Endpoints

In development environment, the API uses SwaggerUI to test the endpoints.
To access SwaggerUI, launch the API and go to the following URL: http://localhost:5273/swagger/index.html (
adapt the port if necessary) or launch the API runner.

## Authors and contributors

Léo Paillard