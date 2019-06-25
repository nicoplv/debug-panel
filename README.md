# Debug Panel for Unity3D

A debug panel for Unity3D to always show the current value of variables

![Debug Panel for Unity3D](https://i.imgur.com/iJV65Iz.png)

## Installation

Import the [last package](https://github.com/nicoplv/debug-panel/releases) on your project and that's all!

## Usage
To log a variable awesomeVariable with the label "My Awesome Variable" on the group "My Awesome Group":
```C#
DebugPanel.Log("My Awesome Variable", awesomeVariable, "My Awesome Group");
```

A more concrete example to log the current time in any group:
```C#
DebugPanel.Log("Time", Time.time);
```