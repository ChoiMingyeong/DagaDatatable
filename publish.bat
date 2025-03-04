@echo off

if not exist "docs" (
	mkdir docs
)

if not exist "docs\.nojekyll" (    
	type nul > "docs\.nojekyll"
)

dotnet publish -c Release -o release
xcopy release\wwwroot docs /E /Y