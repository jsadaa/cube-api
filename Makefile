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