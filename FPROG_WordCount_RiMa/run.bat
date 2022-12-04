@echo off 

title FPROG WordCount test script!

echo first testrun from moodle sample

cmd /k "FPROG_WordCount.exe .\testfiles\sampletestMoodle .txt"

echo testrun from base project directory

cmd /k "FPROG_WordCount.exe . .txt"

echo testrun over multiple directorys 

cmd /k "FPROG_WordCount.exe .\testfiles .txt"

echo testrun with .java files

cmd /k "FPROG_WordCount.exe .\testfiles .java"

pause

