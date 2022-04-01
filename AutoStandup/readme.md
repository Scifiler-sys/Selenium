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

# Future Implementation
* Go to caliber to dynamically change QC overall feedback for that week