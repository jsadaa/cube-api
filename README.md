# API - LCE

## Description

Cette API permet de gérer les données de la société LCE.

Elle est consommée par :

- Une application web pour la vente de produits
- Une application desktop pour la gestion des stocks

## Installation

### Prérequis

- Dotnet SDK >= 7.0
- Entity Framework Core CLI
- MariaDB (Docker ou non)
- Docker
- Make

### Installation des prérequis

#### Make

##### Windows

- Télécharger et installer [Chocolatey](https://chocolatey.org/install)
- Ouvrir un terminal en tant qu'administrateur
- Exécuter la commande `choco install make`
- Redémarrer le terminal
- Vérifier l'installation avec la commande `make --version`

##### MacOS

- Ouvrir un terminal
- Exécuter la commande `xcode-select --install`
- Installer [Homebrew](https://brew.sh/index_fr)
- Exécuter la commande `brew install make`
- Vérifier l'installation avec la commande `make --version`

#### Dotnet SDK

##### Linux (Debian)

```bash
Make install-dotnet-on-debian
```

##### MacOS

```bash
Make install-dotnet-on-mac
```

#### Entity Framework Core CLI

```bash
Make install-ef
```

#### MariaDB (Sans Docker)

##### Linux (Debian)

```bash
Make install-mariadb-on-debian
```

##### MacOS

```bash
Make install-mariadb-on-mac
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

Se référer à la documentation officielle de [Docker](https://docs.docker.com/engine/install/debian/)

##### MacOS

```bash
brew install docker
```

Ou suivre les instructions sur le site officiel de [Docker](https://docs.docker.com/docker-for-mac/install/)

### Installation de l'API

#### Cloner le projet

```bash
git clone git@github.com:LCE-CESI/ApiCube.git
```

#### Créer la base de données

```bash
make db-create
```

#### Créer les tables

```bash
make db-migrate
```

#### Lancer l'API

```bash
make run
```

#### Mettre à jour la base de données

```bash
make db-update
```

#### Réinitialiser la base de données

```bash
make db-reset
```

#### Supprimer la base de données

```bash
make db-drop
```

## Développement

### Fixtures

L'API utilise des seeders Bogus [Bogus](https://github.com/bchavez/Bogus) pour générer des données aléatoires.
Elles sont chargées au démarrage de l'API (voir Persistence/Seeders).

### Tests

En environnement de développement, l'api utilise SwaggerUI pour tester les endpoints.
Pour accéder à SwaggerUI, lancer l'API et se rendre sur l'URL suivante : http://localhost:5273/swagger/index.html (adapter le port si besoin) ou lancer le runner de l'API.