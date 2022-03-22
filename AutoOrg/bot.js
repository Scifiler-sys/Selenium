const puppeteer = require('puppeteer');
const creds = require('./Data/credentials.json');

//Depending on your internet speed, you might want to change this delay
const timer = 2;

(async () => {
    const browser = await puppeteer.launch({headless: false});
    const page = await browser.newPage();
    await page.goto("https://github.com/login");

    //Dom Selecting
    const userInput = "#login_field";
    const passInput = "#password";
    const loginButton = "#login > div.auth-form-body.mt-3 > form > div > input.btn.btn-primary.btn-block.js-sign-in-button";

    //Logging in operation
    await page.click(userInput);
    await page.keyboard.type(creds.username);

    await page.click(passInput);
    await page.keyboard.type(creds.password);

    await Promise.all([
        await page.click(loginButton),
        await page.waitForNavigation()
    ]);
    
    // //Creating Organization
    // await page.goto("https://github.com/account/organizations/new?coupon=&plan=team_free");
    
    // const orgNameInput = "#organization_profile_name";
    // await page.click(orgNameInput);
    // await page.keyboard.type(creds.organizationName);
    // alert("Unfortunately due to that robot checking, you have to do this one manually. You have 5 minutes to finish this part and the bot will take care of the rest");

    // //Unfortunately due to that robot checking, you have to do this one manually
    // //You have 5 minute to finish it as well
    // await page.waitForNavigation({timeout: 300000});

    
    //Adding people in Organization automatically
    await ConvertingToJson();
    const usernames = require('./Data/ConvertedJson.json');
    usernames.Sheet1.shift();
    await page.goto("https://github.com/orgs/"+creds.organizationName+"/people");

    await page.click("#js-pjax-container > div > div.container-xl.px-3 > div > div.Layout-main > div > div > div.subnav.org-toolbar.org-toolbar-next > div > details.details-reset.details-overlay.details-overlay-dark > summary");
    
    for(const user of usernames.Sheet1)
    {
        await page.waitForSelector("#org-invite-complete-input").then(async () => {
            console.log(user);
            await page.click("#org-invite-complete-input");
            await page.keyboard.type(user.username);
            await page.screenshot({path:"test.png"});
    
            await page.waitForSelector("#org-invite-complete-results-option-0").then(async () => {
                await page.click("#org-invite-complete-results-option-0");
                await page.click("#js-pjax-container > div > div.container-xl.px-3 > div > div.Layout-main > div > div > div.subnav.org-toolbar.org-toolbar-next > div > details.details-reset.details-overlay.details-overlay-dark > details-dialog > form > div > div > button");
            });
    
            await page.waitForSelector("#js-pjax-container > div.add-member-wrapper.settings-next > form > div > div > button").then(async () => {
                await page.click("#js-pjax-container > div.add-member-wrapper.settings-next > form > div > div > button");
            });
    
            await page.waitForSelector("#js-pjax-container > div > div.container-xl.px-3 > div > div.Layout-main > div > div > div.subnav.org-toolbar.org-toolbar-next > div > details > summary").then(async () => {
                await page.click("#js-pjax-container > div > div.container-xl.px-3 > div > div.Layout-main > div > div > div.subnav.org-toolbar.org-toolbar-next > div > details > summary");
            })
        });
        
    }


    await page.screenshot({path:"test.png"}); 

    await browser.close();
})();

//Will create a json version of an excel sheet
async function ConvertingToJson() {
    'use strict';
    const excelToJson = require('convert-excel-to-json');
    const fs = require('fs');

    const result = excelToJson({
        source: fs.readFileSync('Data/GithubUsernames.xlsx'),
        columnToKey: {
            A: 'username'
        }
    });

    await fs.writeFileSync('Data/ConvertedJson.json', JSON.stringify(result,null,2), function(err) {
        if (err) {
            console.log(err);
        }
    });
}