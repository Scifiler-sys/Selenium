# Project Details
* So far specific to my machine only so I'm not sure how it will react to yours
* It will automatically login to saleforce website and grab the emails from each associate and send them a welcome email

# Project Setup
1. Create a Data folder with a credentials.json file inside of it
```json
{
    "username": "PUT SALEFORCE USERNAME HERE",
    "password": "PUT SALEFORCE PASSWORD HERE",
    "reportLink": "PUT URL LINK TO YOUR TRAINING REPORT PAGE OF SALEFORCE"
}
```
2. Create/Change the chrome shortcut link to match your directory
    * Example of filepath to my chrome: "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
    * Add --remote-debugging-port=9222 on the "Target" input box when you right-click and click properties of the shortcut icon (it should be there already but it might not be so double check)
    * Remove --profile-directory="Profile 1" (That one is specific to me)