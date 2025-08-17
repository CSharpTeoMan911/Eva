<img align=center src="https://github.com/user-attachments/assets/ff57c28b-235f-4a01-905a-23ca973abaf3"/>


<br>
<br>
<br>
<br>

## 🔄 Changelog
📌 Latest Version: v7.4.0

* 🆕 Implemented the ChatGPT chat history
* 🆕 ChatGPT can now render markdown inside the chat
* 🆕 Re-designed the instruction manual window
* 🐞 Fixed UI related bugs such as window snapping

<img width="724" height="681" alt="Settings" src="https://github.com/user-attachments/assets/a6879123-9362-45fe-9975-7faa034dac57" />


<br>
<br>

## 📥 Download
* ➡️ Download Eva from SourceForge: https://sourceforge.net/projects/eva-ai/
* ➡️ Download Eva from Github: https://github.com/CSharpTeoMan911/Eva/releases/tag/Release

<br>
<br>

## 🛠️ Developer support
* For instructions about how to set up the environment for developent, how to add the resources needed by the application, and how to add the necessary libraries and SDK's : https://github.com/CSharpTeoMan911/Eva/wiki/Visual-Studio-configuration-and-operational-prerequisites

<br>
<br>

## About ❓
* Eva is an A.I. assistant that has the purpose of helping users multi-task. It also has the use of helping people with mental and phisical disabilities. All the commands to be executed are given to Eva through voice commmands.

https://github.com/user-attachments/assets/cb57aae9-8f52-4323-95ef-aac1812aa80e

<br> 

## 💻 Technologies
* The Eva's core technologies are the **Vosk** In-proc speech recognition engine, the **Microsoft online speech recognition engine**, the **.NET framework**, **Windows Presentation Foundation** (**WPF**), and the **Universal Windows Platforms** (**UWP**). 

* 🖥️🎙️ [Vosk Speech Recognition Engine](https://github.com/alphacep/vosk-api) 

* 🤖🧠 [Speech recognition model](https://alphacephei.com/vosk/models)

* 🖥️🎙️ [UWP Speech Recognition Engine](https://learn.microsoft.com/en-us/uwp/api/windows.media.speechrecognition.speechrecognizer?view=winrt-22621)

<br>
<br>

### 🗣️💻⚙️🌐📡 Speech recognition infrastructure
___________________________________________________

![Speech system flowchart](https://user-images.githubusercontent.com/87245086/234708319-0ad38208-afe3-460c-9066-224704151b20.png)


<br>

* The Vosk speech recogniser is listening permanently, if the listening function is activated. Once it recognises the word "Listen" or "Hey listen", the online speech recognition engine is activated. The online speech recognition engine has the role of extracting commands and their content.

<br>
<br>

### 🧠💬📖⚙️ Natural Language Understanding (NLU) in Eva
____________________________________________________________

Eva utilizes Natural Language Understanding (NLU) in two key ways: speech recognition and contextual command/content extraction. These two components are powered by Vosk and Windows Online Speech Recognition engines for speech recognition, and a custom-built command and content extraction engine that I developed.

1) **Speech Recognition with NLU:**
* Both Vosk and Windows Online Speech Recognition use NLU to convert spoken words from any audio medium into text. These engines are responsible for interpreting and transcribing the user's voice.

3) **Contextual Command & Content Extraction:**
* Eva understands commands and extracts relevant content based on user input. This is done by processing commands through the following steps:

#### The NLU Process in Eva:
1) **Tokenization: Understanding the Command Type:**

* The first step in processing a user command is tokenization. This process breaks down the sentence into individual components to identify the command's action and any additional parameters that need to be extracted.
* Example:
"open chrome" – The first word ("open") signals that the command is to open an application (in this case, "chrome").
"search leopard 1 tank blueprint on google" – The first word ("search") indicates that the action is a web search.
* If the command doesn’t match any known patterns, no action is taken.

2) **Secondary Tokenization: Extracting Parameters:**

* Once the command type (action) is identified, a secondary tokenization occurs to extract any secondary parameters.
* Example:
"search black shoes on amazon" – The system identifies that the content to search for is "black shoes" and the web application is "amazon".
* If the second tokenization fails to identify valid parameters, no action is taken.
 
3) **Third Tokenization: Variable Extraction and Validation:**

* A third round of tokenization is used to extract variables (like search content or website names) based on the previous steps.
* These variables are then validated against a predefined list of valid values (e.g., valid websites or search keywords).
* Once validated, the relevant processes are executed based on the user's command.
If no valid variables are found, no process is triggered.

#### How This Works:
* Tokenization helps Eva break down the input into smaller parts, identifying the core action (open, search, etc.) and the necessary details (content, application, etc.).
* If each step of the tokenization process identifies and validates the necessary components, Eva proceeds to execute the desired action (e.g., opening an app or performing a web search).
* If any tokenization step fails (e.g., if the required content or application is missing), no action is taken.

#### Why This is Important:
* Precision and Accuracy: By using multiple tokenization steps, Eva ensures that the correct processes are executed based on the user's exact intent.
* Flexibility: Eva can handle various types of commands (e.g., opening apps, searching the web) by adjusting to different formats of user input.
* User Experience: This process ensures that even with a wide variety of potential commands, Eva can respond accurately and intuitively.


![302436808-cbed89ab-0788-4fcf-976d-bb694cdb888f](https://github.com/CSharpTeoMan911/Eva/assets/87245086/bf68529e-295f-4300-9b8f-15959da55643)

<br>


#### ⏱️📊 Natural Language Understanding Engine Time Complexities
__________________________________________________________

<br>

![Time complexity](https://github.com/user-attachments/assets/996de477-159c-400c-adc5-3dc7d7b86f77)

<br>

The time complexity of the natural language understanding engine is in ***O(n)*** in the worst case and average case scenarios, and the time complexity ***O(n - (ci + 1))*** is in the best case scenario, where "ci" stands for current index where the engine could not match the input with any patern related to any process. Because the natural language understanding engine has 3 stages of tokenisation that are verifying if certain criterias are met within the given sentence, the natural language understanding engine will stop processing the information at the index where the sentence did not fulfil the specified criterias, depending at which tokenisation stage the natural language understanding engine the criterias are not satisfied, and thus the ***RAM*** memory resources and the ***CPU*** processing power are not wasted unnecessary.

#### Note 📝

The previously mentioned formula results in the fact that the resulting best case is **O(1)**, and it was used like in other algorithms **(e.g. Boyer Moore algorithm, best case: **O(n/m)**)** to signify the underlying process through which the time complexity was achieved as well as highlighting the variations of the result **(e.g. O(1), O(2), O(3), etc.)**, which can be part of the same class **(e.g. O(1))**, but at a discreete level are different **(O(2) has two steps, whereas O(3) has three steps, but Big-O notation concludes that they are both part of the O(1) class)**.

<br>
<br>
<br>

# ∇🔺📉 Gradient fluctuation formula
<br>

![Gradient fluctuation](https://user-images.githubusercontent.com/87245086/234971684-804bca17-440c-44d4-84e2-dd3a73a240d8.png)

<br>

<br>

![Linear representation of the gradient fluctuation](https://github.com/CSharpTeoMan911/Eva/assets/87245086/e3fcd77c-046d-49ba-97d0-dc8c22eac702)

<br>

Eva uses a unique algorithm developed by me to create stunning graphical user interface effects. This algorithm controls the gradient fluctuation used to produce smooth, dynamic animations. The effect is achieved by manipulating the gradient value incrementally, creating fluid transitions between different states of the GUI.

## How the Algorithm Works:
The gradient fluctuation formula functions by increasing the offset of the gradient via an incremental sum. This incrementally increases the gradient value until a certain threshold is reached, at which point it decreases the gradient back to its original value, creating a smooth oscillation.

This fluctuation can be represented as two linear functions:

1) Increasing the Gradient (Smooth Animation Start):
When the animation begins, the formula y = x + v is used, where x is the current gradient value, y is the resulting gradient value, and v is the value added to the gradient to increment it. This continues until the threshold for the desired animation effect is reached.

2) Decreasing the Gradient (Smooth Animation End):
After reaching the threshold, the formula y = x - v is applied. The gradient value is decremented by v back to its original value, creating a smooth, flowing animation effect. This ensures that the animation ends as smoothly as it started.

## Formula Breakdown:
* **y = x + v (Increasing the gradient value for a smooth animation start)**
* **y = x - v (Decreasing the gradient value for a smooth animation finish)**

#### Where:
* **x = Current gradient value**
* **y = Resulting gradient value**
* **v = Increment/Decrement value**
* **All values x, y, and v are greater than zero.**
  
This algorithm allows Eva to create smooth, beautiful animations with fluid transitions, adding a polished feel to the application’s graphical interface.
<br>
<br>
<br>


# 📋 Usage

To give commands to Eva, simply say the word "Listen" or "Hey listen" followed by your desired command. For example:

1) Say "Hey listen" or "Listen" to activate Eva.
2) Then say "Search robots are cool on Google" to initiate a web search.

<br/>

You can find detailed instructions on command patterns and troubleshooting in the Eva instruction manual.

<br>

![Eva_Capture1225954986](https://github.com/user-attachments/assets/28e1d5ac-942d-4ca2-aef2-0bc695f47f64)

![Eva_Capture1445270440](https://github.com/user-attachments/assets/1ad5b108-db95-47c9-a134-e8e28f71f8c0)

<br>

# ⚙️ Command Customization

<img width="1128" height="683" alt="Command Customisation" src="https://github.com/user-attachments/assets/9582eece-f325-4c2e-899a-4cbcf8bbd21e" />


* Add, remove, and modify commands as you desire. For detailed instruction regarding command customisation, go to the Wiki page: **https://github.com/CSharpTeoMan911/Eva/wiki/Commands-customisation** 

<br>
<br>
<br>

# 🤖 What Eva Can Do?

<br>

## 🗣️ Give Commands to ChatGPT
* Eva can communicate with ChatGPT and process your requests for intelligent, conversational replies.

<br>

<img width="820" height="681" alt="Api key setup" src="https://github.com/user-attachments/assets/d5f470f4-31d1-4561-baf3-58d893cbe839" />

![Eva_Capture1350166031](https://github.com/user-attachments/assets/a328a720-dca8-43db-99c1-576ee0e4fb4f)

![Eva_Capture296921737](https://github.com/user-attachments/assets/81550a89-2ae2-414e-9cf5-7e8f05c355d6)

<br>

## ⏲️ Set a Timer
* Eva can set timers for you, whether it’s for cooking, work breaks, or other tasks!
<br>

<img width="278" height="336" alt="Timer off" src="https://github.com/user-attachments/assets/277bb2f3-800d-4913-8dfb-515bfaa7cbee" />

<img width="277" height="337" alt="Timer on" src="https://github.com/user-attachments/assets/47e76af6-1320-4875-af47-f91c951b590c" />

<br>

## 📸 Take Screenshots
* Need to capture your screen? Eva can take screenshots on demand. The main window has an auto-hide feature when the screenshot function is initiated, thus making this process as seamless possible.

<br>

![Eva_Capture1101378818](https://github.com/user-attachments/assets/9c490e12-76a2-4dce-94cb-9937f753a52a)

<br>

## 🌐 Search on Google
* Eva can search Google for anything!

<br>

![Eva_Capture2116847400](https://user-images.githubusercontent.com/87245086/201438605-8b1d54d3-4208-4a8d-88fa-23c1ab413477.jpg)

<br>

## 📰 Search on Google News
* Stay up to date with the latest news using Google News search.
  
<br>

![Eva_Capture1332318160](https://user-images.githubusercontent.com/87245086/201438689-1bfeb4fa-5d98-4953-a5ac-351152ec5724.jpg)

<br>

## 🖼️ Search on Google Images
* Eva can search for images across the web on Google Images.

<br>

![Eva_Capture1895286925](https://user-images.githubusercontent.com/87245086/201438735-185cc24d-ec54-4bc3-adfa-6fcf71063999.jpg)

<br>

## 🛒 Search on eBay
* Want to find something on eBay? Eva has you covered.

<br>

![Eva_Capture1343975081](https://user-images.githubusercontent.com/87245086/201438775-0ff79a5b-dcd4-41c4-b972-294836c99b29.jpg)

<br>

## 🛍️ Search on Amazon
* Shop for anything on Amazon with Eva.
  
<br>

![Eva_Capture759505394](https://user-images.githubusercontent.com/87245086/201438812-776be9a4-1aea-45f1-b27d-b6dc1150f32d.jpg)

<br>

## 📚 Search on Wikipedia
* Need information? Eva can search Wikipedia for you.

<br>

![Eva_Capture903369397](https://user-images.githubusercontent.com/87245086/201438847-d34a1894-46f6-41be-a848-c9d3f6b05e3a.jpg)

<br>

## 📺 Search on Netflix
* Eva can also search Netflix for shows or movies.
  
<br>

![Eva_Capture2144580495](https://user-images.githubusercontent.com/87245086/201479457-2f1c0609-9c41-4481-8142-30f6db61528a.jpg)

<br>

## 🔴 Search on Reddit
* Eva can find discussions, threads, and more on Reddit.

<br>

![Eva_Capture1857061334](https://user-images.githubusercontent.com/87245086/229218589-cbac7e37-3c14-4541-a6f5-660dc78e69ff.jpg)

<br>

## 📘 Search on Facebook
* Find posts and content on Facebook through Eva.

<br>

![Eva_Capture719492630](https://user-images.githubusercontent.com/87245086/229218831-b215de4b-aa78-4b70-be10-d08f6ddba158.jpg)

<br>

## 📸 Search on Instagram
* Search Instagram for images, posts, and more.

<br>

![Eva_Capture934103573](https://user-images.githubusercontent.com/87245086/229219394-d3dc45a2-4da9-48a8-b609-87ed6c41cdfa.jpg)

<br>

## 📧 Search on Gmail
* Need to check your Gmail? Eva can search through it for you.

<br>

![Eva_Capture2104650543](https://user-images.githubusercontent.com/87245086/229219627-59f734c2-4deb-44a7-8fc5-1d86eb4ef7db.jpg)

<br>

## 🐦 Search on Twitter
* Eva can search through Twitter for tweets and hashtags.

<br>

![Eva_Capture144044807](https://user-images.githubusercontent.com/87245086/229219803-dcf7c33c-5280-4fcb-aac9-9407d5b82be0.jpg)

<br>

## 📌 Search on Pinterest
* Find pins and boards on Pinterest with Eva.

<br>

![Eva_Capture571896073](https://user-images.githubusercontent.com/87245086/229220011-8f3bf0f7-2832-44bc-99a2-870d770b01ef.jpg)

<br>

## 💼 Search on LinkedIn
* Eva can search for professional content on LinkedIn.

<br>

![Eva_Capture1342419452](https://user-images.githubusercontent.com/87245086/229220221-41099e9e-c27c-41d1-ba91-42376061a9c8.jpg)

<br>

## 🧑‍💻 Search on Github
* Developers rejoice! Eva can search GitHub repositories and issues.

<br>

![Eva_Capture1030181943](https://user-images.githubusercontent.com/87245086/229220365-81212deb-2b29-448a-a748-59dc7e7b6f27.jpg)

<br>

## 📸 Search on Unsplash
* Looking for high-quality images? Eva can find them on Unsplash.

<br>

![Eva_Capture1898758186](https://user-images.githubusercontent.com/87245086/229220460-a68e8f64-e6fa-4b87-be51-b0eef14efc01.jpg)

<br>

## 💻 Search on Stack Overflow
* Get answers to coding questions from Stack Overflow.

<br>

![Eva_Capture1340621546](https://user-images.githubusercontent.com/87245086/229220632-ef4ddd23-b567-4179-96e6-ace092304d60.jpg)

<br>

## ⚙️ Open and Close System Settings
* Eva can manage your system settings for you.

<br>

![Eva_Capture1994252519](https://user-images.githubusercontent.com/87245086/201438947-8bac14cd-794b-42b2-b443-930479ef2cbf.jpg)

<br>

## 💻 Open and Close Applications
* Eva can open and close multiple applications, such as Chrome, Firefox, Notepad, Visual Studio Code, and more.

<br>

![Eva_Capture204556584](https://user-images.githubusercontent.com/87245086/201439146-1a57e248-f545-4c3e-9145-dae08eca19aa.jpg)

![Eva_Capture24348866](https://user-images.githubusercontent.com/87245086/201439151-fb4192a6-6f61-4aba-9b8a-08fc398fbf94.jpg)

<br>
<br>
<br>

# Extra information

<br>

Eva is open source and free to use. You can find extra informations on the [**Wiki**](https://github.com/CSharpTeoMan911/Eva/wiki) section of the repository. 

<br>

![Eva_Capture2116139740](https://user-images.githubusercontent.com/87245086/201439583-33776568-07bb-4217-842b-1506c87b6428.jpg)

![Eva_Capture657687740](https://github.com/user-attachments/assets/1f21763a-8d11-4d50-b8b5-9620c161262b)


<br>
<br>
</html>



