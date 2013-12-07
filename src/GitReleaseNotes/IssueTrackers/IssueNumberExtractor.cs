using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LibGit2Sharp;

namespace GitReleaseNotes.IssueTrackers
{
    public class IssueNumberExtractor : IIssueNumberExtractor
    {
        /// <param name="arguments"></param>
        /// <param name="releases"></param>
        /// <param name="issueNumberRegexPattern">Must have a named group called 'issueNumber'</param>
        public Dictionary<ReleaseInfo, List<string>> GetIssueNumbers(GitReleaseNotesArguments arguments, Dictionary<ReleaseInfo, List<Commit>> releases, string issueNumberRegexPattern)
        {
            var result = new Dictionary<ReleaseInfo, List<string>>();

            var issueRegex = new Regex(issueNumberRegexPattern, RegexOptions.Compiled);

            foreach (var release in releases)
            {
                var issueNumbersToScan = new List<string>();
                foreach (var commit in release.Value)
                {
                    var matches = issueRegex.Matches(commit.Message).Cast<Match>();

                    foreach (var match in matches)
                    {
                        var issueNumber = match.Groups["issueNumber"].Value;
                        if (arguments.Verbose)
                            Console.WriteLine("Found issues {0} in commit {1}", issueNumber, commit.Sha);
                        issueNumbersToScan.Add(issueNumber);
                    }
                }

                result.Add(release.Key, issueNumbersToScan);
            }
            
            return result;
        }
    }
}