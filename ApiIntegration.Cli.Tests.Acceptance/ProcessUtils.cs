using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace ApiIntegration.Cli.Tests.Acceptance {
    [Pure]
    public static class ProcessUtils {
        public static Process ExecuteDotnetCommand(
            string command,
            Dictionary<string, string>? envVariables = null
        ) {
            var startInfo = new ProcessStartInfo {
                FileName = "dotnet.exe",
                Arguments = command,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };

            if (envVariables is not null) {
                foreach (var (key, value) in envVariables) {
                    startInfo.Environment[key] = value;
                }
            }

            var process = Process.Start(startInfo);

            Debug.Assert(process is not null, nameof(process) + " is not null");

            return process;
        }
    }
}