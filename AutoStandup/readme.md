# Project Details
* Using Selenium instead to automate standup and use pre-existing user credentials in Google Chrome

# Project Setup
1. Add appsettings.json and add this scaffold here
```json
{
    "DateStartedBatch" : "DATE HERE",
    "AssociateNumber": 0,
    "SaleForceMatch": false, 
    "SaleForceReasons": [
        "NAME - REASON",
        "NAME - REASON"
    ],
    "Warning": 0,
    "GeneralNote" : "GENERAL DESC.",
    "Initiatives": [
        "INITIATIVES 1",
        "INITIATIVES 2"
    ]
}
```
2. Create/Change the chrome shortcut link to match your directory
    * Example of filepath to my chrome: "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
    * Add --remote-debugging-port=9222 on the "Target" input box when you right-click and click properties of the shortcut icon (it should be there already but it might not be so double check)
    * Remove --profile-directory="Profile 1" (That one is specific to me)

# Future Implementation
* Go to caliber to dynamically change QC overall feedback for that week