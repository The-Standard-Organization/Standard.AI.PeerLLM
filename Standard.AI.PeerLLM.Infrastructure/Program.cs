// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using Standard.AI.PeerLLM.Infrastructure.Services;

namespace Standard.AI.PeerLLM.Infrastructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();

            scriptGenerationService.GenerateBuildScript(
                branchName: "main",
                projectName: "Standard.AI.PeerLLM",
                dotNetVersion: "9.0.100");

            scriptGenerationService.GeneratePrLintScript("main");
        }
    }
}
