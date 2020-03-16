create table definitions (logfilekey TEXT PRIMARY KEY, splunkoutput BIT, databaseoutput BIT);
create table archive (sourcefile TEXT, dropdate TEXT, fileentry TEXT);
