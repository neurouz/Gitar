# Gitar - Github User & Repository Viewer

Gitar is a simple web application for creating and reading users and repositories from GitHub.

To configure application, Github API key and data configuration is required. You can set these options in appsettings.json of ***Gitar.Web*** project.

## Installation:

### 1. Clone or download repository
### 2. Configure application settings in appsettings.json:
- GithubApi.BaseAddress - Base address of github, leave https://api.github.com/
- GithubApi.AccessToken - Github API access token, read more information on how to generate personal access token https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token
- DataSouce.AbsolutePath - Absolute path of JSON database file on system (ex. C:\Users\{user}\Desktop) or **leave empty to create file inside wwwroot folder of Gitar.WebApp**
- DataSource.FileName - File name of JSON database file
- DataSource.MinifyJson - false if you want to work with formatted JSON data, otherwise false (for minified JSON)
- DataSource.ClearOnStartup - Clear JSON database on each startup. You should keep this on false for normal usage

Simply you can normally start using application with setting only Github API key and leave other settings as they are.
On first startup, initial user will be created (neurouz). 
