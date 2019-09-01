#addin "Cake.Docker"

var target = Argument("target", "Run");

Task("Build-Services")
    .Does(() =>
{
	var settings = new DockerComposeBuildSettings
	{
		Files = new []{ "docker-compose.yml", "docker-compose.override.yml" }
	};
	DockerComposeBuild("transavia.web.mvc", "transavia.api");
});

Task("Build-DBSeeder")
    .Does(() =>
{
	DotNetCoreBuild("./src/Transavia.DatabaseSeeder/Transavia.DatabaseSeeder.csproj");
});

Task("Run-Services")
	.IsDependentOn("Build-Services")
    .Does(() =>
{
	var settings = new DockerComposeUpSettings 
	{
		ForceRecreate = true,
		Files = new []{ "docker-compose.yml", "docker-compose.override.yml" },
		DetachedMode = true 
	};
	DockerComposeUp(settings);
	
});

Task("Run-DBSeeder")
	.IsDependentOn("Build-DBSeeder")
	.IsDependentOn("Run-Services")
    .Does(() =>
{
	DotNetCoreRun("./src/Transavia.DatabaseSeeder/Transavia.DatabaseSeeder.csproj", "-c 'Data Source=.,5433;Initial Catalog=TransaviaDB;User ID=sa;Password=123asdQ!' -f https://raw.githubusercontent.com/jbrooksuk/JSON-Airports/master/airports.json");	
});

Task("Build-PerformanceTests")	
    .Does(() =>
{
	DotNetCoreBuild("./tests/Performance/Transavia.API.PerformanceTests/Transavia.API.PerformanceTests.csproj");
});

Task("PerformanceTests")
	.IsDependentOn("Run-Services")
	.IsDependentOn("Run-DBSeeder")
	.IsDependentOn("Build-PerformanceTests")
    .Does(() =>
{
	DotNetCoreTest("./tests/Performance/Transavia.API.PerformanceTests/Transavia.API.PerformanceTests.csproj");
});

RunTarget(target);