Recommended Book to Read where this project was compiled from.
Pro ASP.NET Core Identity By Adam Freeman

# Portal
 .Net Core Identity
dotnet --list-sdks
https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.201-windows-x86-installer
PS C:\Users\Steph> cd source
cd repos
dotnet new globaljson --sdk-version 7.0.201 --output Portal
dotnet new webapp --auth Individual --use-local-db true --output Portal --framework net7.0
dotnet new sln -o Portal
dotnet sln Portal add Portal --force
cd Portal
dotnet build


Nuget package
dotnet tool install --global dotnet-ef

dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef --version 7.0.3
dotnet ef migrations add AddPortal
dotnet ef database drop -- force
dotnet ef database update
dotnet tool uninstall --global Microsoft.Web.LibraryManager.Cli
dotnet tool install --global Microsoft.Web.LibraryManager.Cli --version 2.1.175
libman init -p cdjs
libman install twitter-bootstrap@4.6.2 -d wwwroot/lib/twitter-bootstrap
dotnet add package Microsoft.EntityFrameWorkCore.Design --version 7.0.3
dotnet add package Microsoft.EntityFrameWorkCore.SqlServer --version 7.0.3
 dotnet ef migrations add Intitial
dotnet ef database drop --force
dotnet ef database update
 dotnet run
dotnet dev-certs https --clean
dotnet dev-certs https --trust
dotnet add package Microsoft.AspNetCore.Identity.UI --version 7.0.3
dotnet ef migrations add IdentityInitial --context IdentityDbContext
dotnet ef database drop --force --context IdentityDbContext
dotnet ef database update --context IdentityDbContext
libman install qrcodejs@1.0.0 -d wwwroot/lib/qrcode
 dotnet ef database drop -- force --context ProductDbContext
dotnet ef database update --context ProducyDbContext
dontnet tool uninstall --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-aspnet-codegenerator --version 7.0.4
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 7.0.4
dotnet aspnet-codegenerator identity --listFiles
libman install font-awesome@6.3.0 -d wwwroot/lib/font-awesome
dotnet aspnet-codegenerator identity --dbContext Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext --files Account.Login
Move-Item -Path ManageNavPages.cs -Destination ManageNavPages.cs.safe
Move-Item -Path ManageNav.cshtml -Destination ManageNav.cshtml.safe
dotnet aspnet-codegenerator identity --dbContext Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext --files Account.Manage.EnableAuthenticator --no-build --force
dotnet aspnet-codegenerator identity --dbContext Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext --files Account.ForgotPassword
dotnet ef database drop --force --context ProductDbContext
dotnet ef database drop --force --context IdentityDbContext
dotnet ef database update --context ProductDbContext
dotnet ef database update --context IdentityDbContext
