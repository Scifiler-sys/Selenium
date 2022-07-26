from selenium import webdriver

def start_driver():
    print('starting chrome driver...')
    options = webdriver.ChromeOptions()
    options.headless = True

    driver = webdriver.Chrome(options=options, executable_path="./data/chromedriver")

    return driver

def open_google():
    driver = start_driver()
    driver.get("https://www.google.com/?client=safari")

    driver.save_screenshot("./data/screen.png")

    driver.quit()