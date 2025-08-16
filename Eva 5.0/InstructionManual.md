# About Eva  
Eva is an A.I. assistant that has the purpose of helping users multi-task.  
Eva is achieving this by executing voice commands given by the user.  

Eva is an open-source project and you can find the project [here](https://github.com/CSharpTeoMan911/Eva).  

---

# User Instructions  

In order for Eva to execute commands, the user must do the following steps:  

1. Press the 🎙️ button.  
2. When the button is displaying 🎙️, Eva is actively listening.  
3. Tell Eva the wake word **`Listen`** or **`Hey listen`**, followed by a command.  
   - You must wait one second before giving it a command.  
   - When the circle is changing its colour to bright blue, and the ⏱️ is initiated, Eva is actively listening.  
   - Because the online speech recognition engine has a timeout, when the ⏱️ reaches `0`, Eva will stop listening.  
   - Eva has a delay mechanism that prevents it from taking commands more often than once every **3 seconds**. When the circle turns red, wait until it turns blue again to give a command.  
   - If the user wants to stop Eva from listening, the user must say **`Stop listening`**.  

---

# Commands  

## Opening Applications  
To open applications, use one of the following formats:  
- **`open [Application]`**  
- **`open [Application] now`**  
- **`open [Application] please`**  
- **`please open [Application]`**


### Examples:  
- Command format = **`open [Application]`**  
- Application = `chrome`  
- Command = **`open chrome`**  


### List of applications Eva can open:
- chrome  
- edge  
- firefox  
- opera  
- calculator  
- paint  
- file explorer  
- gmail  
- skype  
- notepad  
- task manager  
- command prompt  
- powershell  
- windows terminal  
- visual studio 2019  
- visual studio 2022  
- visual studio code  
- calendar  
- weather  
- snip and sketch  
- word  
- powerpoint  
- excel  
- onedrive  
- settings  
- disk cleanup  
- control panel  
- temporary files  
- recycle bin cleanup  

Eva can also open **Settings pages or subsections**:  
- Example: **`open devices settings`**  
- Example: **`open mouse settings`**  

---

## Setting Timers  

To set a timer:  
- **`set a [Time interval] timer`**  
- **`please set a [Time interval] timer`**  
- **`set a [Time interval] timer please`**  

### Example:
- Command format = **`set a [Time interval] timer`**  
- Time interval = `1 hour, 45 minutes and 10 seconds`  
- Command = **`set a 1 hour, 45 minutes and 10 seconds timer`**  

When a timer is set, the ⏱️ button changes from red to blue.  

### Example:  
- Open the ⏱️ window by pressing ⏱️  
- Cancel by pressing ❌ or by command:  
  - **`close timer`**  
  - **`close timer please`**  
  - **`close timer now`**  
  - **`please close timer`**  

---

## Closing Applications  

To close applications:  
- **`close [Application]`**  
- **`close [Application] now`**  
- **`close [Application] please`**  
- **`please close [Application]`**  

### List of applications Eva can close:
- chrome  
- edge  
- firefox  
- opera  
- calculator  
- paint  
- skype  
- notepad  
- calendar  
- weather  
- snip and sketch  
- settings  
- task manager  
- command prompt  
- powershell  
- windows terminal  
- visual studio 2019  
- visual studio 2022  
- visual studio code  
- disk cleanup  
- timer  

---

## Searching Content  

To search content on web apps:  
- **`search [Content] on [Web Application]`**  
- **`search on [Web Application] [Content]`**  
- **`please search on [Web Application] [Content]`**  
- **`please search [Content] on [Web Application]`**  

### List of web apps Eva can search on:
- google  
- google news  
- google images  
- ebay  
- netflix  
- wikipedia  
- amazon  
- reddit  
- facebook  
- instagram  
- gmail  
- twitter  
- pinterest  
- linkedin  
- github  
- unsplash  
- stackoverflow  

### Example:
- Command format = **`search [Content] on [Web Application]`**  
- Web Application = `youtube`  
- Content = `robots`  
- Command = **`search robots on youtube`**  

---

## Screenshots  

To take a screenshot:  
- **`screenshot`**  
- **`take screenshot`**  
- **`take a screenshot`**  
- **`take a screenshot please`**  
- **`please take a screenshot`**  

---

## ChatGPT Integration  

Steps:  
1. Open the settings menu and insert your OpenAI API key.  
2. If valid, you can query ChatGPT:  
   - **`Gpt [Query]`**  
   - Example: **`Gpt what is the fastest bird on the planet?`**  

You can also enable **chatbot mode**:  
- **`Activate chatbot mode`**  
- **`Enable chatbot mode`**  

When chatbot mode is on:  
- The indicator turns **🟢 green**.  
- You can continuously query ChatGPT using wake words **`Listen`** / **`Hey listen`** followed by your query.  

To disable chatbot mode:  
- **`Deactivate chatbot mode`**  
- **`Disable chatbot mode`**  

---

## Visibility Toggle  

To make the Eva window **visible/invisible**:  
1. Say **`Listen`** and wait until the circle turns blue or the app beeps.  
2. Say either **`Invisible`** or **`Visible`**.  

---

# Extra Features  

- To prevent accidental activation of Eva without pressing 🎙️, press the ➖ button.  
- Restore the window when you want to give Eva commands.  

To customize commands, see the [detailed tutorial](https://github.com/CSharpTeoMan911/Eva/wiki/Commands-customisation).  

---

# Troubleshooting  

- If Eva cannot understand you:  
  - Choose an English version suitable for your accent in [Settings → Speech Language].  

- If Eva cannot hear you:  
  - Go to [Settings → Time & Language → Speech → Microphone].  
  - Under Microphone, press **Get Started** and follow the instructions.  

- If Eva has accuracy issues:  
  - Go to [Settings → Time & Language → Speech → Speech language].  
  - Check the option **Recognise non-native accents**.  

- If Eva still cannot hear you or has accuracy/behavior issues:  
  - Open **Command Prompt (Admin)** and run:  

```
Dism /Online /Cleanup-Image /ScanHealth

Dism /Online /Cleanup-Image /CheckHealth

sfc /scannow
```  

These commands fix corrupt system files. Since Eva depends on Windows OS, corrupt system files may cause serious issues.  
