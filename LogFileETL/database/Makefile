DATABASENAME = logetl.db
SQLBATCHCMD = sqlite3 -batch $(DATABASENAME)

default:
	@echo 'Targets:'
	@echo '  create -- Create new database and populate it.'
	@echo '  drop   -- Drop existing database.'

create:
	@-rm -f $(DATABASENAME)
	@$(SQLBATCHCMD) < schema.sql
	@$(SQLBATCHCMD) < insert.sql

drop:
	@-rm -f $(DATABASENAME)
