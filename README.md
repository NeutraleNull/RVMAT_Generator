### It's still WIP but stable

### What it does?
 
 - Generate the rvmat config for your .co and .nopx files
 - scan folder for possible rvmat candidates
 - Has a arguments option
 
 ### It can't
  
  - Generate a custom config yet
  - It's only working for ground textures
  - If you do something stupid it will crash
  
### How to use it
First put the executable in the folder where you want to create your rvmats
### Console
Type 'scan' and follow the instructions on the screen
After the scan is succesfull you can type 'generate' to generate your rvmats

Type 'exit' or 'close' to exit the programm.

###Argument using

| Argument | What it does |
| ------   | ------       |
| -generate 'c:\your\folder\path' | generates rvmats for all matching files in your folder but does not overwrite existing rvmats |
| -overwrite | used together with -generate. It allows you to overwrite existing rvmats |
| -scan 'c:\your\folder\path' | scans your folder for possible matching rvmats canidates |

remove the ' from the path before using it...


