﻿using GitTools.IssueTrackers;

namespace GitReleaseNotes
{
    public class JiraContext : IssueTrackerContext
    {
        public string Jql { get; set; }
    }
}