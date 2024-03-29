# VEEAM-TTASK QA for Internal Development in QA (SDET) Team

## Instructions:

1. **Pull the project**: Clone or download the project repository (there's only one branch so it's pretty straightforward).
   
2. **Build the Release version**: Build the project in Release mode.

3. **Navigate to the "\bin\Release\net8.0" folder**: Once the project is built successfully, navigate to the `/bin/Release/net8.0` folder in the project directory.

4. **Use CMD to test the software**: Open Command Prompt (CMD) and execute the software with the appropriate parameters.

## Example Command:

```cmd
VEEAM-TESTTASK.exe "source" "replica" "log.txt" 30
```
"source", "replica", "log.txt" will be created on the root folder of the project. 

Note: The software is currently designed to only work within the project solution.

I extend my gratitude to VEEAM Software for this challenge. Despite the solution not being optimal, it has provided a valuable learning experience.
