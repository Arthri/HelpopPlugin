# Usage

## In Terraria

### Raising an Issue
1. `/helpop <Message>`

The message will be broadcasted so all staff in the network can see it.

## Through the API

### Raising an Issue
```cs
using HelpopPlugin;

var issuer = new IssueUser(
    name: "Plugin",
    ip: "localhost",
    uuid: "N/A",
    account: null
);
var issue = new Issue(
    message: "Test Message",
    issuer: issuer
);

Events.OnInvokeIssue(issue);
```

Everyone with the permission to see reports in the current server will be alerted, and if Redis is enabled, it will be broadcasted to other servers.

### Handling an Issue
```cs
using HelpopPlugin;

Helpop.OnIssue += (args) =>
{
    /* Do stuff */
};
```

Alternatively,
```cs
using HelpopPlugin;

Events.OnIssue += (args) =>
{
    /* Do stuff */
};
```

But what's the different between the two? It is recommended to always use the former. It is always invoked on the Main Terraria Thread as opposed to a background thread. This is to ensure no state corruption occurs. Most methods used in handling issues aren't thread safe, they were never designed to be. However, if you know for sure your code won't corrupt state(e.g. async code), feel free to use the latter one.
