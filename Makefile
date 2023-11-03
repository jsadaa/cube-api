############
# Database #
############

db-migrate :
	dotnet ef database update

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

install-dotnet :
	sudo apt-get install dotnet-sdk-7.1

install-ef :
	dotnet tool install --global dotnet-ef

install-mariadb :
	sudo apt-get install mariadb-server

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

