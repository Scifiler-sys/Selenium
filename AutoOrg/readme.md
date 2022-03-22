# Project description
* Are you like me? Tired of adding people in your organization especially when you have more that 20 ppl to add?
* Well here comes the bot! It will add all the username you specified in an excel sheet for you
* Just provide your credentials!n

# Setup process
1. You need to edit the excel sheet in Data folder to add in the github usernames you need to add
2. Create an organization in github
3. You need to create credentials.json inside of Data folder
```json
{
    "username": "Add github username here",
    "password": "Add github password here",
    "organizationName" : "Add your organization name here"
}
```
4. CLI command process (make sure you are in AutoOrg folder)
```C#
npm install //To get the dependencies back
npm start //To run the bot
```

# Potential things that will fail the automation
* Github username is incorrect
    * Fix the offending github username in excel
* Github username is already invited and accepted
    * Fix by removing added user on excel

# Future implementation
* Checking which users already exist in organization
* Ignore incorrect usernames and move on to the next one

# Troubleshoot
* Ubunto/Linux CLI won't work properly, you need extra configuration to setup chromium right
    * Just use windows powershell instead