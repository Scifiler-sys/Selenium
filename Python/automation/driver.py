from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
from selenium import webdriver

def start_driver():
    print('starting chrome driver...')

    service = Service(executable_path="./data/chromedriver")
    options = Options()
    options.headless = True
    driver = webdriver.Chrome(service=service)

    return driver

def open_google():
    driver = start_driver()
    driver.get("")