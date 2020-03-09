default:
	@echo 'Targets:'
	@echo '  drop  -- Copy test log from remote to drop'
	@echo '  purge -- Remove test logs from drop folder'

drop:
	@cp logfiles/remote/testlog1.txt logfiles/drop

purge:
	@rm -rf logfiles/drop/*
	@mkdir logfiles/drop/formatted
