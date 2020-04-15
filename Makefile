BROWSER = firefox

default:
	@echo 'Targets:'
	@echo '  gen-docs  -- Generate HTML documentation'
	@echo '  view-docs -- View HTML documentation'

gen-docs:
	doxygen

view-docs:
	$(BROWSER) docs/index.html &
