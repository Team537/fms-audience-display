# Commander Commands
Enter these commands in the command line interface to connect and control the display.  Used mainly for testing.

## Managing the connection to the display
```
connect [ipaddress] - connect to display
disconnect - disconnect from display
```

## Controlling the match
```
match [number] - set the match number
[r|b][1-3] [number] - set red/blue team numbers
[r|b]s [number] - set the red/blue score
time [number] - set the remaining time in seconds - warning sounds will automatically play at 10 and 30 seconds left
total-time [number] - set the total duration for the match in seconds - used to make the progress bar
```
## Playing Match Sounds
```
auto-start
auto-end
tele-start
tele-end
abort-match
```