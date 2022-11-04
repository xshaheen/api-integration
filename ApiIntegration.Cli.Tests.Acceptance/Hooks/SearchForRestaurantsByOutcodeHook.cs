using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiIntegration.Cli.Tests.Acceptance.Models;
using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;
using WireMock.Server;
using WireMock.Settings;

namespace ApiIntegration.Cli.Tests.Acceptance.Hooks {
    [Binding]
    public class SearchForRestaurantsByOutcodeHook {
        private const string CliProjectDirectoryName = "ApiIntegration.Cli";
        private const string CliDllName = "ApiIntegration.Cli.dll";

        private WireMockServer _wireMockServer = default!;
        private ProjectPaths _projectPaths = default!;

        private readonly IObjectContainer _objectContainer;

        public SearchForRestaurantsByOutcodeHook(IObjectContainer objectContainer) {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public async Task SetupApiMock() {
            SetupFileLocations();
            await BuildProject();

            _wireMockServer = WireMockServer.Start(new WireMockServerSettings {
                StartAdminInterface = true,
            });

            _objectContainer.RegisterInstanceAs(_wireMockServer);
            _objectContainer.RegisterInstanceAs(_projectPaths);
        }

        [AfterScenario]
        public void TeardownApiMock() {
            _wireMockServer.Stop();
        }

        private void SetupFileLocations() {
            var slnPath = GetSolutionDirectoryInfo();

            Debug.Assert(slnPath is not null, "slnPath is not null");

            var buildExecuteDir = $@"{CliProjectDirectoryName}{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}Debug";
            var refDir = $@"{Path.DirectorySeparatorChar}ref";

            _projectPaths = new ProjectPaths {
                PathToBuild = Directory
                    .EnumerateDirectories(slnPath.FullName, "*", SearchOption.AllDirectories)
                    .First(x => x.EndsWith(CliProjectDirectoryName)),
                PathToExecute = Directory
                    .EnumerateFiles(slnPath.FullName, "*.dll", SearchOption.AllDirectories)
                    .First(x => x.Contains(buildExecuteDir) && x.EndsWith(CliDllName) && !x.Contains(refDir)),
            };
        }

        private async Task BuildProject() {
            var buildCommand = $"build {_projectPaths.PathToBuild}";
            var process = ProcessUtils.ExecuteDotnetCommand(buildCommand);
            await process.WaitForExitAsync();
            process.ExitCode.Should().Be(0);
        }

        public static DirectoryInfo? GetSolutionDirectoryInfo(string? currentPath = null) {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any()) {
                directory = directory.Parent;
            }

            return directory;
        }
    }
}