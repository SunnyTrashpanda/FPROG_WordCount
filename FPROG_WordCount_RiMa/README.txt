# FPROG_WordCount

### Team Members
- Marlies Rieder
- Lucienne Angeli Maramara

### Need to know
- .bat executes programm with provided testfile
- programm is written in C# on Visual Studio --> Windows environment

## HOW TO compile (Windows)
- dotnet 6 sdk has to be installed
- use the cmd commands in the terminal in the source_code directory: 
dotnet restore
dotnet build

## HOW TO run the Unit Tests
- dotnet 6 sdk has to be installed
- use the cmd commands in the terminal in the source_code directory: 
dotnet test

### Disclaimer
Since this subject is about functional programming, the project was developed in a way that all inputs are correct:
- path should exist and be written correct
- there should always be 2 input parameters (path and fileending)

## How to use the programm:
### Step one
double click FPROG_WordCount.bat (to ensures that the project works)
- open a commandline window in the project directory

### Step two
- write "FPROG_WordCount.exe" followed by a path and a fileendling 
eg. "FPROG_WordCount.exe .\testfiles .txt" --> same as in the .bat file

### Step three
- press "enter" and review the printed results