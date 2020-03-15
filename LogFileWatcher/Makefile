default:
	@echo 'Targets:'
	@echo '  drop  -- Copy test log from remote to drop'
	@echo '  purge -- Remove test logs from drop folder'
	@echo '  tree  -- View project in a tree view'

drop:
	@cp logfiles/remote/testlog-member.txt logfiles/drop/member

purge:
	@rm -rf logfiles/drop/member/*

tree:
	@clear
	@tree

