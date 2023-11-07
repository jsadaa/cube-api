############
# Database #
############

db-migrate :
	dotnet ef database update

db-migrate-remove :
	dotnet ef migrations remove

db-migrate-rollback :
	dotnet ef database update 0
	
db-migrate-rollback-all :
	dotnet ef database update 0
	dotnet ef migrations remove

db-migrate-add :
	dotnet ef migrations add $(name)
	
db-drop :
	dotnet ef database drop
	
db-update :
	dotnet ef database update
	
db-reset :
	dotnet ef database drop
	dotnet ef database update

#########
# Infra #
#########

install-dotnet-on-debian :
	sudo apt-get install apt-transport-https
	sudo apt-get update
	sudo apt-get install dotnet-sdk-7.1

install-dotnet-on-mac :
	brew cask install dotnet

install-ef :
	dotnet tool install --global dotnet-ef

install-mariadb-on-debian :
	sudo apt-get install mariadb-server

install-mariadb-on-mac :
	brew install mariadb

###########
# Install #
###########
	
install-app :
	dotnet restore
	dotnet build
	dotnet publish -c Release -o out

run :
	dotnet run

build :
	dotnet build

install-dependencies :
	dotnet restore

