// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV5s;

namespace NHSISL.LibPostalClient.Infrastructure.Services
{
    internal class ScriptGenerationService
    {
        private readonly ADotNetClient adotNetClient;

        public ScriptGenerationService() =>
            this.adotNetClient = new ADotNetClient();

        public void GenerateBuildScript()
        {
            string branchName = "main";

            var githubPipeline = new GithubPipeline
            {
                Name = "Build",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { branchName }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Types = new string[] { "opened", "synchronize", "reopened", "closed" }
                    }
                },

                EnvironmentVariables = new Dictionary<string, string>
                {
                    { "IS_RELEASE_CANDIDATE", EnvironmentVariables.IsGitHubReleaseCandidate() }
                },

                Jobs = new Dictionary<string, Job>
                {
                    {
                        "Build",
                        new Job
                        {
                            RunsOn = BuildMachines.UbuntuLatest,

                            Steps = new List<GithubTask>
                            {
                                new CheckoutTaskV5
                                {
                                    Name = "Check Out"
                                },

                                new SetupDotNetTaskV5
                                {
                                    Name = "Setup Dot Net Version",

                                    With = new TargetDotNetVersionV5
                                    {
                                        DotNetVersion = "10.x"
                                    }
                                },

                                new RestoreTask
                                {
                                    Name = "Restore"
                                },

                                new DotNetBuildTask
                                {
                                    Name = "Build"
                                },

                                new TestTask
                                {
                                    Name = "Test"
                                }
                            }
                        }
                    },
                    {
                        "add_tag",
                        new TagJobV2(
                            runsOn: BuildMachines.UbuntuLatest,
                            dependsOn: "build",
                            projectRelativePath: "NHSISL.LibPostalClient/NHSISL.LibPostalClient.csproj",
                            githubToken: "${{ secrets.PAT_FOR_TAGGING }}",
                            branchName: branchName)
                    },
                    {
                        "publish",
                        new PublishJobV4(
                            runsOn: BuildMachines.UbuntuLatest,
                            dependsOn: "add_tag",
                            nugetApiKey: "${{ secrets.NUGET_ACCESS }}",
                            dotNetVersion: "10.0.100")
                    }
                }
            };

            string buildScriptPath = "../../../../.github/workflows/build.yml";
            string directoryPath = Path.GetDirectoryName(buildScriptPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: buildScriptPath);
        }

        public void GeneratePrLinterScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "PR Linter",

                OnEvents = new Events
                {
                    PullRequest = new PullRequestEvent
                    {
                        Types = new string[] { "opened", "synchronize", "reopened", "edited", "closed" }
                    }
                },

                Jobs = new Dictionary<string, Job>
                {
                    {
                        "require_issue_or_task",
                        new RequireIssueOrTaskJobV2()
                    },
                    {
                        "set_author_as_pr_assignee",
                        new SetAuthorAsPrAssigneeJobV2(BuildMachines.UbuntuLatest)
                    }
                }
            };

            string prLinterScriptPath = "../../../../.github/workflows/prLinter.yml";
            string directoryPath = Path.GetDirectoryName(prLinterScriptPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: prLinterScriptPath);
        }
    }
}
