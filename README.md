# Debug Panel for Unity3D

A debug panel for Unity3D to always show the current value of variables

![Debug Panel for Unity3D](https://i.imgur.com/63zOXly.png)

## Installation

Import the [package](https://github.com/nicoplv/debug-panel/raw/master/DebugPanel.unitypackage) on your project and that's all ... you have access easily to all the tags and layers on your code.

## Usage
To log a variable awesomeVariable with the label "My Awesome Variable" on the group "My Awesome Group":
```C#
DebugPanel.Log("My Awesome Variable", awesomeVariable, "My Awesome Group");
```

A more concrete example to log the current time in any group:
```C#
DebugPanel.Log("Time", Time.time);
```