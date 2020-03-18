# Log File ETL

ETL processing for log/response files.

## Requirements

All instructions assume you're working in one of the following environments:

* Linux; or
* Windows with Cygwin installed.

You'll also need [SQLite](https://www.sqlite.org).

If you're in Windows without Cygwin, you'll have to adjust the instructions accordingly.

## Testing

Open a terminal in the project root directory.

Go to the database directory:

```bash
cd LogFileETL/database
```

Generate a new database:

```bash
make create
```

Change to the LogFileETL root folder, then run the project:

```bash
cd ..

dotnet run
```

Open another terminal in the project root directory.

Go to the LogFileWatcher directory, purge existing target log files, and run the project:

```bash
cd LogFileWatcher

make purge

dotnet run
```

Open another terminal in the project root directory.

Go to the LogFileWatcher directory, and drop a test file:

```bash
cd LogFileWatcher

make drop
```

A new Splunk-friendly log file will be generated in the logfiles/drop/member/formatted folder; and

The project_root/LogFileETL/database/logetl.db database will have new entries added to its archive table:

```bash
sqlite3 logetl.db
```

Then:

```
.header on
.mode column
select * from archive;
```
