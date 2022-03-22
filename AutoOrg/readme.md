# Projects
* Automatically adding people to your organization

# Setup process
* You need to edit the excel sheet in Data folder to add in the github usernames you need to add
* You need to create credentials.json inside of Data folder
```json
{
    "username": "Add github username here",
    "password": "Add github password here",
    "organizationName" : "Add your organization name here"
}
```
* CLI command process (make sure you are in AutoOrg folder)
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
